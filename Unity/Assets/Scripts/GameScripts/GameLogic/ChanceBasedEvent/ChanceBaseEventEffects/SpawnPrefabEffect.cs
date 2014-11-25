using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ChanceBasedEvent.ChanceBaseEventEffects
{
    [AddComponentMenu("ChanceBaseEventEffect/SpawnPrefabEffect")]
    [RequireComponent(typeof(PrefabSpawner))]
    public class SpawnPrefabEffect : ChanceBaseEventEffect
    {
        [Range(0f, 100f)]
        public float SpawnRadius = 0f;

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

        public override void Activate()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            const float blockRadius = 0.2f;

            if (!PrefabSpawner.CanSpawn())
            {
                yield break;
            }
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
            while (!UtilityFunctions.LocationPathFindingReachable(transform.position, spawnPosition) ||
                Physics2D.OverlapCircle(spawnPosition, blockRadius, LayerConstants.LayerMask.Obstacle) != null)
            {
                spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            PrefabSpawner.SpawnPrefab(spawnPosition);
        }
    }
}
