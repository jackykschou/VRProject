﻿using System.Collections.Generic;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [AddComponentMenu("LevelMechanics/Section/SectionObjective/EnemySpawnPointsInactive")]
    public class EnemySpawnPointsInactive : SectionObjective
    {
        private List<GameObject> ActiveSpawnPoints;

        [Attributes.GameEvent(GameEvent.OnSectionEnemySpawnPointActivated)]
        public void OnSectionEnemySpawnerActivated(GameObject spawnPoint, int sectionId)
        {
            if (sectionId == SectionId)
            {
                ActiveSpawnPoints.Add(spawnPoint);
            }
        }

        [Attributes.GameEvent(GameEvent.OnSectionEnemySpawnPointDeactivated)]
        public void OnSectionEnemySpawnPointDeactivated(GameObject spawnPoint, int sectionId)
        {
            if (sectionId == SectionId)
            {
                ActiveSpawnPoints.Remove(spawnPoint);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            ActiveSpawnPoints = new List<GameObject>();
        }

        protected override void Deinitialize()
        {
        }

        public override bool ObjectiveCompleted()
        {
            return ActiveSpawnPoints.Count == 0;
        }
    }
}
