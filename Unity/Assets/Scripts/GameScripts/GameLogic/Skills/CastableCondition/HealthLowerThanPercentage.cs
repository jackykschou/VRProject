using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/HealthLowerThanPercentage")]
    public class HealthLowerThanPercentage : SkillCastableCondition
    {
        [Range(0f, 1.0f)]
        public float LowerThanValuePercentage;
        public Health.Health Health;

        protected override void Deinitialize()
        {
            Health = null;
        }

        public override bool CanCast()
        {
            if (Health == null)
            {
                Health = Skill.Caster.gameObject.GetComponent<Health.Health>();
            }

            return Health.HitPoint.Percentage <= LowerThanValuePercentage;
        }
    }
}
