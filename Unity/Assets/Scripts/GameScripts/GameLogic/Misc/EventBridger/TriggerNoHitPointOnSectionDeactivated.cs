using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [AddComponentMenu("Misc/TriggerNoHitPointOnSectionDeactivated")]
    public class TriggerNoHitPointOnSectionDeactivated : GameLogic
    {
        public int SectionId;

        private bool _triggered;

        [GameEvent(Constants.GameEvent.OnSectionDeactivated)]
        public void OnSectionDeactivated(int sectionId)
        {
            if (sectionId == SectionId && !_triggered)
            {
                TriggerGameScriptEvent(Constants.GameScriptEvent.OnObjectHasNoHitPoint);
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DecrementSectionEnemy()
        {
            _triggered = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (LevelManager.Instance != null)
            {
                SectionId = LevelManager.Instance.CurrentSectionId;
            }
            _triggered = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
