using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics
{
    [AddComponentMenu("LevelMechanics/ApplyPowerUpOnSectionEnemySpawned")]
    public class ApplyPowerUpOnSectionEnemySpawned : GameLogic
    {
        public List<Prefab> PowerUpPrefabs;

        public int MaxApplierTime;
        private int _initialMaxApplierTime;
        private int _appliedCounter;

        [Attributes.GameScriptEvent(GameScriptEvent.OnSectionEnemySpawned)]
        public void OnSectionEnemySpawned(GameObject enemy)
        {
            if (_appliedCounter > MaxApplierTime)
            {
                return;
            }
            _appliedCounter++;

            Prefab powerupPrefab = PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Count)];
            PrefabManager.Instance.SpawnPrefab(powerupPrefab, o => o.TriggerGameScriptEvent(GameScriptEvent.ApplyPowerUp, enemy, powerupPrefab));
        }

        [Attributes.GameEvent(GameEvent.SurvivalDifficultyIncreased)]
        public void UpdateMaxApplierTime(int difficulty)
        {
            MaxApplierTime += (int)(0.35f * difficulty);
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _initialMaxApplierTime = MaxApplierTime;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _appliedCounter = 0;
            MaxApplierTime = _initialMaxApplierTime;
        }

        protected override void Deinitialize()
        {

        }
    }
}
