using System.Collections.Generic;
using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Health.Health))]
    [AddComponentMenu("LevelMechanics/Section/EnemyTeleporter")]
    public class EnemyTeleporter : SectionLogic
    {
        public List<SectionEnemySpawnPoint> SpawnPoints;

        private Health.Health _health;
        private ParticleSystem _onEnemySpawnedSystem;

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHasNoHitPoint)]
        public void StopSpawn()
        {
            SpawnPoints.ForEach(p => p.Activated = false);
        }

        [GameEvent(Constants.GameEvent.OnSectionEnemySpawned)]
        public void PlayParticleSystem(int sectionID)
        {
            if (sectionID == SectionId)
            {
                if(_onEnemySpawnedSystem && _health.HitPoint.Value > 0)
                    _onEnemySpawnedSystem.Play();
            }
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _health.Invincible = false;
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                _health.Invincible = true;
            }
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _health = GetComponent<Health.Health>();
            _onEnemySpawnedSystem = GetComponentInChildren<ParticleSystem>();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _health.Invincible = true;
        }

        protected override void Deinitialize()
        {
        }
    }
}
