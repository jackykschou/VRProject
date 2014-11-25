using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlayAnimation")]
    public class PlayAnimation : SkillEffect
    {
        public string BoolParameterName;

        [SerializeField] 
        private float _animationDuration;

        [SerializeField]
        private List<string> _animationEventMessages;

        [SerializeField]
        private List<float> _animationEventMessagesSendTime;

        [SerializeField] 
        private List<SkillEffect> _animationSkillEffects;

        [SerializeField]
        private List<float> _animationSkillEffectsActivateTime;

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            if (_animationEventMessages != null)
            {
                if (_animationEventMessages.Count != _animationEventMessagesSendTime.Count)
                {
                    _animationEventMessagesSendTime.Resize(_animationEventMessages.Count);
                }
            }

            if (_animationSkillEffects != null)
            {
                if (_animationSkillEffects.Count != _animationSkillEffectsActivateTime.Count)
                {
                    _animationSkillEffectsActivateTime.Resize(_animationSkillEffects.Count);
                }
            }
        }

        public override void Activate()
        {
            base.Activate();
            TriggerCasterGameScriptEvent(GameScriptEvent.SetAnimatorBoolState, BoolParameterName);
            StartCoroutine(WaitForAnimationFinish());
        }

        IEnumerator WaitForAnimationFinish()
        {
            List<bool> eventMessagesSent = new List<bool>();
            eventMessagesSent.AddRange(Enumerable.Repeat(false, _animationEventMessages.Count));
            List<bool> skillEffectActivated = new List<bool>();
            skillEffectActivated.AddRange(Enumerable.Repeat(false, _animationSkillEffects.Count));
            for (int i = 0; i < _animationSkillEffects.Count; ++i)
            {
                skillEffectActivated.Add(false);
            }
            float timer = 0f;
            while ((timer < _animationDuration || eventMessagesSent.Any(b => !b)) && !Skill.Caster.gameObject.IsInterrupted() && !Skill.Caster.gameObject.HitPointAtZero())
            {
                if (timer >= _animationDuration)
                {
                    Activated = false;
                }
                yield return new WaitForSeconds(Time.deltaTime);
                timer += Time.deltaTime;
                for (int i = 0; i < _animationEventMessages.Count; ++i)
                {
                    if (!eventMessagesSent[i] && (timer >= _animationEventMessagesSendTime[i]))
                    {
                        eventMessagesSent[i] = true;
                        Skill.Caster.gameObject.BroadcastMessage(_animationEventMessages[i], SendMessageOptions.DontRequireReceiver);
                    }
                }
                for (int i = 0; i < _animationSkillEffects.Count; ++i)
                {
                    if (!skillEffectActivated[i] && (timer >= _animationSkillEffectsActivateTime[i]))
                    {
                        skillEffectActivated[i] = true;
                        if (_animationSkillEffects[i].CanActivate())
                        {
                            _animationSkillEffects[i].Activate();
                        }
                    }
                }
            }
            Activated = false;
        }

    }
}
