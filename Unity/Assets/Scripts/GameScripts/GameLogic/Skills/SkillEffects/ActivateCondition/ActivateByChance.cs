using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    [AddComponentMenu("Skill/SkillEffect/SkillEffectActivateCondition/ActivateByChance")]
    public class ActivateByChance : SkillEffectActivateCondition 
    {
        [Range(0f, 1f)]
        public float Chance;

        public override bool CanActivate()
        {
            return UtilityFunctions.RollChance(Chance);
        }
    }
}
