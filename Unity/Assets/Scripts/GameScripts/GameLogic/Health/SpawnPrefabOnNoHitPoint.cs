using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("Health/SpawnPrefabOnNoHitPoint")]
    public class SpawnPrefabOnNoHitPoint : GameLogic 
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

        [GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void OnObjectHasNoHitPoint()
        {
            PrefabSpawner.SpawnPrefab(transform.position);
        }
    }
}
