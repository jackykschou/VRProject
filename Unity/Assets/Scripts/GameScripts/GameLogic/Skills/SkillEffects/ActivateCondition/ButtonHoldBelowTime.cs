using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    [AddComponentMenu("Skill/SkillEffect/SkillEffectActivateCondition/ButtonHoldBelowTime")]
    public class ButtonHoldBelowTime : SkillEffectActivateCondition 
    {

        [Range(0f, 10f)]
        public float TimeBelow;

        private float _holdTime;

        public override bool CanActivate()
        {
            return _holdTime < TimeBelow;
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        public void UpdateSkillButtonHoldEffectTime(float time)
        {
            _holdTime = time;
        }
    }
}
