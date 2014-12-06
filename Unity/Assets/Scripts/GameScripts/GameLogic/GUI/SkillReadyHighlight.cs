using Assets.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Image))]
    public class SkillReadyHighlight : GameLogic
    {
        [Range(0,5)]
        public int SkillId;
        [Range(0.0f, 10.0f)]
        public float HighlightDuration = .5f;
        [Range(0.0f, 1.0f)]
        public float MaxAlpha = .75f;

        private float _highlightDuration;
        private Image _highlightImage;
        private bool _hasActivated;
        private bool _skillUsed;

        protected override void Update()
        {
            base.Update();
            if (_hasActivated)
            {
                if ((_highlightDuration -= Time.deltaTime) < 0.0f)
                {
                    DisableHighlight();
                }
            }
        }

        [GameEvent(GameEvent.OnPlayerSkillCoolDownUpdate)]
        public void UpdateSkillCoolDown(int id, float percentage)
        {
            if (id == SkillId)
            {
                if (percentage > .99f)
                {
                    if (!_skillUsed)
                        return;
                    EnableHighlight(HighlightDuration);
                }
                else
                {
                    _skillUsed = true;
                }
            }
        }

        public void EnableHighlight(float origHighlightDuration)
        {
            if (_hasActivated)
                return;
            _highlightImage.color = new Color(_highlightImage.color.r, _highlightImage.color.g, _highlightImage.color.b, 1.0f);
            _highlightDuration = origHighlightDuration;
            _hasActivated = true;
        }

        public void DisableHighlight()
        {
            if (!_skillUsed)
                return;
            _highlightImage.color = new Color(_highlightImage.color.r, _highlightImage.color.g, _highlightImage.color.b, 0.0f);
            _highlightDuration = 0.0f;
            _skillUsed = false;
            _hasActivated = false;
        }
        protected override void Initialize()
        {
            base.Initialize();
            _highlightImage = GetComponent<Image>();
            _skillUsed = false;
            _hasActivated = false;
            DisableHighlight();
        }
        
        protected override void Deinitialize()
        {
        }
    }
}
