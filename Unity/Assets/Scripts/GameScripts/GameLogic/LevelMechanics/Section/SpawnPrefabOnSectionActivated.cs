using Assets.Scripts.GameScripts.GameLogic.Spawner;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(PrefabSpawner))]
    [AddComponentMenu("LevelMechanics/Section/SpawnPrefabOnSectionActivated")]
    public class SpawnPrefabOnSectionActivated : SectionLogic 
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

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                PrefabSpawner.SpawnPrefab(transform.position);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
