using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/ApplyPowerUp")]
    public class ApplyPowerUp : TargetEffectApplier
    {
        public Prefab PowerUpPrefab;

        protected override void ApplyEffect(GameObject target)
        {
            PrefabManager.Instance.SpawnPrefab(PowerUpPrefab, o => o.TriggerGameScriptEvent(GameScriptEvent.ApplyPowerUp, target, PowerUpPrefab));
        }
    }
}
