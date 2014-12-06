using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Utility;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.Managers
{
    public class LevelManager : GameLogic
    {
        public bool IsPlayLevel;

        public int CurrentSectionId;

        public static LevelManager Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<LevelManager>()); }
        }
        private static LevelManager _instance;

        public LoopName BackGroundMusicLoop;

        public static bool LevelStarted 
        {
            get { return Instance != null && Instance._levelStarted; }
        }

        public Transform CameraInitialFollowTransform;

        private bool _levelStarted;

        protected override void Initialize()
        {
            base.Initialize();
            _levelStarted = false;

            if (!GameManager.Instance.LoadLevelOnStart)
            {
                GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelFinishedLoading);
                GameEventManager.Instance.TriggerGameEvent(GameEvent.OnLevelStarted);
            }
        }

        protected override void Deinitialize()
        {
            _instance = null;
        }

        [GameEvent(GameEvent.OnLevelEnded)]
        public void OnLevelEnded()
        {
            AudioManager.Instance.StopLevelLoop(BackGroundMusicLoop);
        }

        [GameEvent(GameEvent.OnLevelStarted)]
        public void OnLevelStarted()
        {
            GameManager.Instance.HUD.SetActive(IsPlayLevel);
            if (IsPlayLevel)
            {
                GameEventManager.Instance.TriggerGameEvent(GameEvent.EnablePlayerCharacter);
            }
            _levelStarted = true;
            AudioManager.Instance.PlayLevelLoop(BackGroundMusicLoop);
        }

        [GameEvent(GameEvent.OnLevelFinishedLoading)]
        public void OnLevelFinishedLoading()
        {
            if (CameraInitialFollowTransform == null)
            {
                GameManager.Instance.MainCamera.TriggerGameScriptEvent(GameScriptEvent.CameraFollowTarget, GameManager.Instance.PlayerMainCharacter.transform);
            }
            else
            {
                GameManager.Instance.MainCamera.TriggerGameScriptEvent(GameScriptEvent.CameraFollowTarget, CameraInitialFollowTransform);
            }
            if (IsPlayLevel)
            {
                GameManager.Instance.PlayerMainCharacter.TriggerGameScriptEvent(GameScriptEvent.SetAnimatorBoolState, AnimatorControllerConstants.AnimatorParameterName.Idle);
                if (GameManager.Instance.PlayerMainCharacter.HitPointAtZero())
                {
                    GameManager.Instance.PlayerMainCharacter.TriggerGameScriptEvent(GameScriptEvent.ResetHealth);
                }
                GameManager.Instance.PlayerMainCharacter.renderer.enabled = true;
            }
            else
            {
                GameManager.Instance.PlayerMainCharacter.renderer.enabled = false;
            }
        }
    }
}
