using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Collider2D))]
    public class SectionGateBlock : SectionLogic
    {
        public Collider2D BlockCollider;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            base.Initialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.StaticObstacle);
            BlockCollider = GetComponent<Collider2D>();
            BlockCollider.isTrigger = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            LockGate();
        }

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            LockGate();
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            UnLockGate();
        }

        public void LockGate()
        {
            BlockCollider.enabled = true;
            TriggerGameScriptEvent(GameScriptEvent.GateActivated);
        }

        public void UnLockGate()
        {
            BlockCollider.enabled = false;
            TriggerGameScriptEvent(GameScriptEvent.GateDeactivated);
        }
    }
}
