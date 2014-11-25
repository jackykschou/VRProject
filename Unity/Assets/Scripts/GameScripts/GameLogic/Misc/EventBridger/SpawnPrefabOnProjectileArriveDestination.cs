using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("Misc/SpawnPrefabOnProjectileArriveDestination")]
    public class SpawnPrefabOnProjectileArriveDestination : GameLogic
    {
        public PrefabSpawner PrefabSpawner;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnProjectileArriveDestination)]
        public void OnProjectileArriveDestination(Vector2 destination)
        {
            PrefabSpawner.SpawnPrefab(transform.position);
        }
    }
}
