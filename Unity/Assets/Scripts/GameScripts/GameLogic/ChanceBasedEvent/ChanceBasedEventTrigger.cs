using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameScripts.GameLogic.ChanceBasedEvent.ChanceBasedEventTriggerConditions;
using Assets.Scripts.GameScripts.GameLogic.ChanceBasedEvent.ChanceBaseEventEffects;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ChanceBasedEvent
{
    [AddComponentMenu("ChanceBasedEventTrigger/ChanceBasedEventTrigger")]
    public class ChanceBasedEventTrigger : GameLogic
    {
        public Managers.ChanceBasedEvent Event;

        public float AdditionalChanceChangeAmount;

        private List<ChanceBaseEventEffect> _chanceBaseEventEffects;
        private List<ChanceBasedEventTriggerCondition> _chanceBasedEventTriggerConditions;

        protected override void Initialize()
        {
            base.Initialize();
            _chanceBaseEventEffects = GetComponents<ChanceBaseEventEffect>().ToList();
            _chanceBasedEventTriggerConditions = GetComponents<ChanceBasedEventTriggerCondition>().ToList();
        }

        protected override void Deinitialize()
        {
        }

        public void Trigger()
        {
            if (_chanceBasedEventTriggerConditions.Any(c => !c.CanTrigger()))
            {
                return;
            }

            if (ChanceBasedEventManager.Instance.RollEventChance(Event, AdditionalChanceChangeAmount))
            {
                _chanceBaseEventEffects.ForEach(e => e.Activate());
            }
        }
    }
}
