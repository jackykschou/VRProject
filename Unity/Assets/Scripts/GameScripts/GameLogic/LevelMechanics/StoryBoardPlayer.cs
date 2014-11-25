using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics
{
    [AddComponentMenu("LevelMechanics/StoryBoardPlayer")]
    public class StoryBoardPlayer : GameLogic
    {
        public List<Sprite> FlipBook;
        public float FlipDelay = 3.0f;
        public Prefab NextLevelPrefab;
        public bool Skippable = true;

        private UnityEngine.UI.Image _holder;
        private float _timeAlive;
        private int _curFlipBookImageIndex = 0;
        private bool _canTrigger = true;

        [SerializeField]
        private ButtonOnPressed SkipButton;

        protected override void Initialize()
        {
            base.Initialize();
            _holder = GetComponent<UnityEngine.UI.Image>();
            _holder.sprite = FlipBook[_curFlipBookImageIndex];
            _timeAlive = 0;
            _canTrigger = true;
            _curFlipBookImageIndex = 0;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_timeAlive > FlipDelay)
            {
                if (_curFlipBookImageIndex == FlipBook.Count - 1)
                {
                    GameManager.Instance.ChangeLevel(NextLevelPrefab);
                    return;
                }
                _holder.sprite = FlipBook[++_curFlipBookImageIndex];
                _timeAlive = 0;
            }
            if (SkipButton.Detect() && _canTrigger && Skippable)
            {
                _curFlipBookImageIndex = 0;
                _timeAlive = 0;
                GameManager.Instance.ChangeLevel(NextLevelPrefab);
                _canTrigger = false;
            }
            _timeAlive += Time.fixedDeltaTime;
        }

        protected override void Deinitialize()
        {
        }
    }
}
