using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [RequireComponent(typeof(TargetNotNull))]
    [AddComponentMenu("Skill/CastableCondition/TargetWithinDistance")]
    public class TargetWithinDistance : SkillCastableCondition
    {
        [Range(0, float.MaxValue)] 
        public float Distance;

        protected override void Deinitialize()
        {
        }

        public override bool CanCast()
        {
            if (Skill.Caster.Target == null)
            {
                return false;
            }

            return Vector2.Distance(Skill.Caster.Target.position, Skill.Caster.GameView.CenterPosition) <= Distance;
        }
    }
}
