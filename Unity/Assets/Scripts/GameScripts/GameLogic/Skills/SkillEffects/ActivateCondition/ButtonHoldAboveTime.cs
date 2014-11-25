using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    [AddComponentMenu("Skill/SkillEffect/SkillEffectActivateCondition/ButtonHoldAboveTime")]
    public class ButtonHoldAboveTime :  SkillEffectActivateCondition
    {
        [Range(0f, 10f)] 
        public float TimeAbove;

        private float _holdTime;

        public override bool CanActivate()
        {
            return _holdTime >= TimeAbove;
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        public void UpdateSkillButtonHoldEffectTime(float time)
        {
            _holdTime = time;
        }
    }
}
