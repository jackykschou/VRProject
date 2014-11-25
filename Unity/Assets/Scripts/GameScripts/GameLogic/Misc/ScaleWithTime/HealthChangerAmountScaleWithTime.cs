using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.ScaleWithTime
{
    [RequireComponent(typeof(HealthChanger))]
    public class HealthChangerAmountScaleWithTime : GameLogic
    {
        [Range(0f, 10f)]
        public float MinTime;
        [Range(0f, 10f)]
        public float MaxTime;

        private HealthChanger _healthChanger;
        private float _originalAmount;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            if (time < MinTime)
            {
                _healthChanger.GameValueChanger.RawAmount = 0f;
            }
            else if (time >= MaxTime)
            {
                return;
            }
            else
            {
                float scale = time / MaxTime;
                _healthChanger.GameValueChanger.RawAmount = scale * _originalAmount;
            }
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _healthChanger = GetComponent<HealthChanger>();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _originalAmount = _healthChanger.GameValueChanger.RawAmount;
        }

        protected override void Deinitialize()
        {
        }
    }
}
