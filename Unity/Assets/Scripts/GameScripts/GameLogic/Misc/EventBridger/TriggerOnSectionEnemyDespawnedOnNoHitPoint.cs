using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [AddComponentMenu("Misc/TriggerOnSectionEnemyDespawnedOnNoHitPoint")]
    public class TriggerOnSectionEnemyDespawnedOnNoHitPoint : GameLogic
    {
        public int SectionId;

        protected override void Initialize()
        {
            base.Initialize();
            if (LevelManager.Instance != null)
            {
                SectionId = LevelManager.Instance.CurrentSectionId;
            }
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DecrementSectionEnemy()
        {
            TriggerGameEvent(GameEvent.OnSectionEnemyDespawned, SectionId);
        }

        protected override void Deinitialize()
        {
        }
    }
}
