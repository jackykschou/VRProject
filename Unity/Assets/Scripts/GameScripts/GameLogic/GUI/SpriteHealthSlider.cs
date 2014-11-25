using Assets.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class SpriteHealthSlider : GameLogic
    {
        public Image HealthSliderImage;
        private Slider _slider;
        private Canvas _canvas;
        protected override void Initialize()
        {
            base.Initialize();
            _slider = gameObject.GetComponentInChildren<Slider>();
            _slider.targetGraphic.enabled = true;
            _slider.value = 1.0f;
            HealthSliderImage.color = Color.green;
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();
            if (Mathf.Approximately(_slider.value, 0f))
            {
                _slider.targetGraphic.enabled = false;
            }
            else
            {
                _slider.targetGraphic.enabled = true;
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHealthChanged)]
        public void UpdateSlider(float changedAmount, GameValue.GameValue health)
        {
            _slider.value = health.Percentage;
            if (HealthSliderImage != null)
            {
                HealthSliderImage.color = Color.Lerp(Color.red, Color.green, _slider.value);
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DisableCanvas()
        {
            if (_canvas != null)
                _canvas.enabled = false;
        }

    }
}
