using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjective/GameEventReceive")]
    public class GameEventReceive : SectionObjective
    {
        public GameEvent Event;
        [Range(0, 100)]
        public int ReceiveGoal = 1;

        private int _receiveCount;

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _receiveCount = 0;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _receiveCount = 0;
        }

        protected override void Deinitialize()
        {
        }

        public override bool ObjectiveCompleted()
        {
            return _receiveCount >= ReceiveGoal;
        }

        [GameEventAttribute(GameEvent.OnGameEventSent)]
        public void OnGameEventSent(GameEvent gameEventName)
        {
            if (gameEventName == Event && SectionActivated)
            {
                _receiveCount++;
            }
        }

    }
}
