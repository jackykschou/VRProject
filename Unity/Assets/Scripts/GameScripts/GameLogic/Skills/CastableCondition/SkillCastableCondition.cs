using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [RequireComponent(typeof(Skill))]
    public abstract class SkillCastableCondition : GameLogic
    {
        public Skill Skill;

        public abstract bool CanCast();

        protected override void Initialize()
        {
            base.Initialize();
            Skill = GetComponent<Skill>();
        }
    }
}
