using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ButtonHoldLoop")]
    public class ButtonHoldLoop : SkillEffect
    {
        [SerializeField] private ButtonOnHold _button;

        [SerializeField] private List<string> _onHoldFinishEventMessages;

        [SerializeField]
        private List<string> _loopEventMessages;

        [SerializeField]
        private List<float> _loopEventMessagesSendTime;

        [SerializeField]
        private List<SkillEffect> _loopSkillEffects;

        [SerializeField]
        private List<float> _loopSkillEffectsActivateTime;

        [Range(0f, 10)] 
        public float TimePerLoop;

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (_loopEventMessages != null)
            {
                if (_loopEventMessages.Count != _loopEventMessagesSendTime.Count)
                {
                    _loopEventMessagesSendTime.Resize(_loopEventMessages.Count);
                }
            }

            if (_loopSkillEffects != null)
            {
                if (_loopSkillEffects.Count != _loopSkillEffectsActivateTime.Count)
                {
                    _loopSkillEffectsActivateTime.Resize(_loopSkillEffects.Count);
                }
            }
        }

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(StartHolding());
        }

        private IEnumerator StartHolding()
        {
            float holdTimer = 0f;
            float loopTimer = 0f;

            List<bool> loopMessagesSent = new List<bool>();
            loopMessagesSent.AddRange(Enumerable.Repeat(false, _loopEventMessages.Count));
            List<bool> skillEffectActivated = new List<bool>();
            skillEffectActivated.AddRange(Enumerable.Repeat(false, _loopSkillEffects.Count));

            TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, 0f);

            while (_button.Detect() && !Skill.Caster.gameObject.IsInterrupted() && !Skill.Caster.gameObject.HitPointAtZero())
            {
                for (int i = 0; i < _loopEventMessages.Count; ++i)
                {
                    if (!loopMessagesSent[i] && (loopTimer >= _loopEventMessagesSendTime[i]))
                    {
                        loopMessagesSent[i] = true;
                        Skill.Caster.gameObject.BroadcastMessage(_loopEventMessages[i], SendMessageOptions.DontRequireReceiver);
                    }
                }
                for (int i = 0; i < _loopSkillEffects.Count; ++i)
                {
                    if (!skillEffectActivated[i] && (loopTimer >= _loopSkillEffectsActivateTime[i]))
                    {
                        skillEffectActivated[i] = true;
                        if (_loopSkillEffects[i].CanActivate())
                        {
                            _loopSkillEffects[i].Activate();
                        }
                    }
                }

                loopTimer += Time.deltaTime;
                holdTimer += Time.deltaTime;

                if (loopTimer >= TimePerLoop)
                {
                    loopTimer = 0f;
                    for (int i = 0; i < loopMessagesSent.Count; ++i)
                    {
                        loopMessagesSent[i] = false;
                    }
                    for (int i = 0; i < skillEffectActivated.Count; ++i)
                    {
                        skillEffectActivated[i] = false;
                    }
                }
                yield return new WaitForSeconds(Time.deltaTime);
                TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, holdTimer);
            }
            _onHoldFinishEventMessages.ForEach(s => gameObject.BroadcastMessage(s, SendMessageOptions.DontRequireReceiver));
            TriggerGameScriptEvent(GameScriptEvent.UpdateSkillButtonHoldEffectTime, _button.LastHoldTime);
            Activated = false;
        }
    }
}
