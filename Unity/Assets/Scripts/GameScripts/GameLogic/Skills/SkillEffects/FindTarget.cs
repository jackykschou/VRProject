using Assets.Scripts.GameScripts.GameLogic.TargetFinders;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/FindTarget")]
    public class FindTarget : SkillEffect
    {
        public TargetFinder TargetFinder;

        public override void Activate()
        {
            base.Activate();
            TargetFinder.FindAndApply();
            Activated = false;
        }
    }
}