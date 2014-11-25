using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjective/EnemyKillCountGoal")]
    public class EnemyKillCountGoal : SectionObjective
    {
        [Range(0, 1000)]
        public int GoalCount;
        private int _killCount;

        protected override void Initialize()
        {
            base.Initialize();
            _killCount = 0;
        }

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _killCount = 0;
            }
        }

        public override bool ObjectiveCompleted()
        {
            return _killCount >= GoalCount;
        }

        [GameEventAttribute(GameEvent.OnSectionEnemyDespawned)]
        public void OnSectionEnemyDespawned(int sectionId)
        {
            if (sectionId == SectionId)
            {
                _killCount++;
            }
        }
    }
}
