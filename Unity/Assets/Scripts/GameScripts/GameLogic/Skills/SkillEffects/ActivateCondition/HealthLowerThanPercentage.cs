using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    [AddComponentMenu("Skill/SkillEffect/SkillEffectActivateCondition/HealthLowerThanPercentage")]
    public class HealthLowerThanPercentage : SkillEffectActivateCondition
    {
        [Range(0f, 1.0f)]
        public float LowerThanValuePercentage;
        public Health.Health Health;

        protected override void Deinitialize()
        {
        }

        public override bool CanActivate()
        {
            return Health.HitPoint.Percentage <= LowerThanValuePercentage;
        }
    }
}
