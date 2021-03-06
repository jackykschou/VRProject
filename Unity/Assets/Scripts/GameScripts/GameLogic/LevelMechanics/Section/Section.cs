﻿using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/Section")]
    public class Section : SectionLogic
    {
        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                LevelManager.Instance.CurrentSectionId = SectionId;
            }
        }

        [GameEvent(GameEvent.OnSectionObjectivesCompleted)]
        public void OnSectionObjectivesCompleted(int sectionId)
        {
            if (sectionId == SectionId)
            {
                TriggerGameEvent(GameEvent.OnSectionDeactivated, SectionId);
            }
        }

        [GameScriptEvent(GameScriptEvent.SurvivalAreaSpawned)]
        [GameEvent(GameEvent.OnLevelStarted)]
        public void UpdateSectionId()
        {
            TriggerGameScriptEvent(GameScriptEvent.UpdateSectionId, SectionId);
        }
        

        protected override void Deinitialize()
        {
        }
    }
}
