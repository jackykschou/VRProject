using UnityEngine;
using UnityEngine.UI;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBarSlider : GameLogic
    {
        public Text HealthText; // make required?
        private Slider _healthBar;
        private Image _healthColorImage;

        protected override void Initialize()
        {
            base.Initialize();
            _healthBar = GetComponent<Slider>();
            _healthColorImage = GameObject.Find("HealthBarFill").GetComponent<Image>();
            _healthBar.value = 1.0f;
        }

        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.PlayerHealthUpdate)]
        public void UpdateHealth(float percentage)
        {
            _healthBar.value = percentage;
            int percentageInt = (int)(Mathf.Ceil(percentage * 100f));
            if (HealthText != null)
            {
                HealthText.text = percentageInt + "%";
            }

            if (_healthColorImage != null)
            {
                _healthColorImage.color = Color.Lerp(Color.red, Color.green, _healthBar.value);
            }
        }
    }
}
