using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjectiveTracker")]
    public class SectionObjectiveTracker : SectionLogic
    {
        public List<SectionObjective> Objectives;
        private const float StartTrackObjectivesDelay = 1.5f;
        private const float TrackObjectivesInterval = 1.0f;

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            TriggerGameEvent(GameEvent.OnSectionObjectivesCompleted, SectionId);
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                InvokeRepeating("TrackObjective", StartTrackObjectivesDelay, TrackObjectivesInterval);
            }
        }

        public void TrackObjective()
        {
            if (GameManager.Instance.PlayerMainCharacter.HitPointAtZero())
            {
                CancelInvoke();
                return;
            }
            if (Objectives.All(o => o.ObjectiveCompleted()) && !GameScriptEventManager.Destroyed)
            {
                CancelInvoke();
                TriggerGameEvent(GameEvent.OnSectionObjectivesCompleted, SectionId);
            }
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Objectives = GetComponents<SectionObjective>().ToList();
        }

        protected override void Deinitialize()
        {
        }
    }
}
