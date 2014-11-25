using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/SpawnPrefabOnSpriteGameViewRandomPosition")]
    public class SpawnPrefabOnSpriteGameViewRandomPosition : TargetEffectApplier
    {
        public Prefab Prefab;
        public PrefabSpawner PrefabSpawner;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.SpawnPrefabOnSpriteGameViewOnRandomPosition, Prefab);
        }
    }
}
