using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/TargetNotNull")]
    public class TargetNotNull : SkillCastableCondition 
    {

        protected override void Deinitialize()
        {
        }

        public override bool CanCast()
        {
            return Skill.Caster.Target != null && !Skill.Caster.Target.gameObject.HitPointAtZero();
        }
    }
}
