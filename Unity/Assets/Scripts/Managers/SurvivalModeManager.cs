using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;
using Assets.Scripts.GameScripts.GameLogic;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.Managers
{
    public class SurvivalModeManager : GameLogic
    {
        public int EasyDifficultyCap;
        public int MediumDifficultyCap;

        public int CurrentDifficulty;
        public List<Prefab> EasySurvivalAreaPrefabs;
        public List<Prefab> MediumSurvivalAreaPrefabs;
        public List<Prefab> HardSurvivalAreaPrefabs;
        public List<Transform> AreaSpawnPoints;

        private int _nextSpawnAreaIndex;
        private Prefab _currentAreaPrefab;
        private GameObject _currentArea;

        public static SurvivalModeManager Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<SurvivalModeManager>()); }
        }
        private static SurvivalModeManager _instance;
        
        protected override void Initialize()
        {
            base.Initialize();
            _currentAreaPrefab = Prefab.None;
            _currentArea = null;
            CurrentDifficulty = 0;
        }

        private Vector3 NextSpawnPosition()
        {
            Vector3 nextPoint = AreaSpawnPoints[_nextSpawnAreaIndex].position;
            _nextSpawnAreaIndex++;
            _nextSpawnAreaIndex %= AreaSpawnPoints.Count;
            return nextPoint;
        }

        private Prefab GetNextArea()
        {
            Prefab nextArea;
            if (CurrentDifficulty < EasyDifficultyCap)
            {
                nextArea = EasySurvivalAreaPrefabs[Random.Range(0, EasySurvivalAreaPrefabs.Count)];
                while (nextArea == _currentAreaPrefab)
                {
                    nextArea = EasySurvivalAreaPrefabs[Random.Range(0, EasySurvivalAreaPrefabs.Count)];
                }
            }
            else if (CurrentDifficulty < MediumDifficultyCap)
            {
                nextArea = MediumSurvivalAreaPrefabs[Random.Range(0, MediumSurvivalAreaPrefabs.Count)];
                while (nextArea == _currentAreaPrefab)
                {
                    nextArea = MediumSurvivalAreaPrefabs[Random.Range(0, MediumSurvivalAreaPrefabs.Count)];
                }
            }
            else
            {
                nextArea = HardSurvivalAreaPrefabs[Random.Range(0, HardSurvivalAreaPrefabs.Count)];
                while (nextArea == _currentAreaPrefab)
                {
                    nextArea = HardSurvivalAreaPrefabs[Random.Range(0, HardSurvivalAreaPrefabs.Count)];
                } 
            }
            return nextArea;
        }

        [GameEventAttribute(GameEvent.SurvivalSectionEnded)]
        public void SpawnNextSection()
        {
            TriggerGameEvent(GameEvent.DisablePlayerCharacter);
            if (_currentArea != null)
            {
                PrefabManager.Instance.DespawnPrefab(_currentArea);
            }

            _currentAreaPrefab = GetNextArea();

            PrefabManager.Instance.SpawnPrefabImmediate(Prefab.SpawnParticleSystem, GameManager.Instance.PlayerMainCharacter.transform.position, o =>
            {
                o.transform.parent = GameManager.Instance.PlayerMainCharacter.transform;
                o.GetComponent<ParticleSystem>().Play();
            });
            PrefabManager.Instance.SpawnPrefabImmediate(_currentAreaPrefab, NextSpawnPosition(), o =>
            {
                _currentArea = o;
            });
            _currentArea.TriggerGameScriptEvent(GameScriptEvent.SurvivalAreaSpawned);
            TriggerGameEvent(GameEvent.SurvivalDifficultyIncreased, CurrentDifficulty++);
            GameManager.Instance.Difficulity = CurrentDifficulty;
            TriggerGameEvent(GameEvent.SurvivalSectionStarted);
            TriggerGameEvent(GameEvent.WaveCountIncreased,CurrentDifficulty-1);
            if (AstarPath.active != null)
            {
                AstarPath.active.Scan();
            }
            Vector3 playerSpawnPoint = _currentArea.GetComponentInChildren<PlayerSpawnPoint>().transform.position;
            GameManager.Instance.PlayerMainCharacter.transform.position = new Vector3(playerSpawnPoint.x,
                                                                                      playerSpawnPoint.y,
                                                                                      GameManager.Instance.PlayerMainCharacter.transform.position.z);
            GameManager.Instance.MainCamera.transform.position = new Vector3(playerSpawnPoint.x,
                                                                             playerSpawnPoint.y,
                                                                             GameManager.Instance.MainCamera.transform.position.z);
            TriggerGameEvent(GameEvent.EnablePlayerCharacter);
        }

        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void OnLevelStarted()
        {
            TriggerGameEvent(GameEvent.OnPlayerReset);
            CurrentDifficulty = 0;
            _currentAreaPrefab = Prefab.None;
            _currentArea = null;
            GameManager.Instance.Difficulity = CurrentDifficulty;
            SpawnNextSection();
        }

        protected override void Deinitialize()
        {
        }
    }
}