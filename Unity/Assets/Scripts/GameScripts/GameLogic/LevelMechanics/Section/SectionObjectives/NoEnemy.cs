using Assets.Scripts.Attributes;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjective/NoEnemy")]
    public class NoEnemy : SectionObjective
    {
        private int _enemyCount;

        protected override void Deinitialize()
        {
        }

        public override bool ObjectiveCompleted()
        {
            return _enemyCount <= 0;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _enemyCount = 0;
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _enemyCount = 0;
            }
        }

        [GameEvent(GameEvent.OnSectionEnemySpawned)]
        public void OnSectionEnemySpawned(int sectionId)
        {
            if (sectionId == SectionId)
            {
                _enemyCount++;
            }
        }

        [GameEvent(GameEvent.OnSectionEnemyDespawned)]
        public void OnSectionEnemyDespawned(int sectionId)
        {
            if (sectionId == SectionId)
            {
                _enemyCount--;
            }
        }
    }
}
