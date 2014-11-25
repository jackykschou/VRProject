using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(Collider2D))]
    [AddComponentMenu("Misc/UpdateAStarGraphWithColliderBoundOnInitialize")]
    public class UpdateAStarGraphWithColliderBoundOnInitialize : GameLogic
    {
        public Collider2D Collider2D;

        protected override void Deinitialize()
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            Collider2D = GetComponent<Collider2D>();
        }

        [Attributes.GameEvent(Constants.GameEvent.OnLevelStarted)]
        [Attributes.GameEvent(Constants.GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            AstarPath.active.UpdateGraphs(new Bounds(Collider2D.bounds.center, new Vector3(10, 10, 10)));
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.GateActivated)]
        public void GateActivated()
        {
            Collider2D.enabled = true;
            AstarPath.active.UpdateGraphs(new Bounds(Collider2D.bounds.center, new Vector3(10, 10, 10)));
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.GateDeactivated)]
        [Attributes.GameEvent(Constants.GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            Collider2D.enabled = false;
            AstarPath.active.UpdateGraphs(new Bounds(Collider2D.bounds.center, new Vector3(10, 10, 10)));
        }
        
    }
}
