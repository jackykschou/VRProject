using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/ManualAssignFinder")]
    public class ManualAssignFinder : TargetFinder
    {
        public GameObject Target;

        protected override void Initialize()
        {
            base.Initialize();
            FindTargets();
        }

        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
            ClearTargets();
            Targets.Add(Target);
        }
    }
}
