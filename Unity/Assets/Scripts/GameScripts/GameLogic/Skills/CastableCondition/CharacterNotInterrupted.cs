using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/CharacterNotInterrupted")]
    public class CharacterNotInterrupted : SkillCastableCondition 
    {
        protected override void Deinitialize()
        {
        }

        public override bool CanCast()
        {
            return !gameObject.IsInterrupted();
        }
    }
}
