using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.ScaleWithTime
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleStartSizeScaleWithTime : GameLogic
    {
        [Range(0f, 10f)]
        public float MinTime;
        [Range(0f, 10f)]
        public float MaxTime;

        private float _originalStartSize;

        public ParticleSystem ParticleSystem;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            if (time < MinTime)
            {
                particleSystem.startSize = 0f;
                particleSystem.enableEmission = false;
            }
            else
            {
                particleSystem.enableEmission = true;
                if (time >= MaxTime)
                {
                    particleSystem.startSize = _originalStartSize;
                }
                else
                {
                    float scale = time / MaxTime;
                    particleSystem.startSize = _originalStartSize * scale;
                }
            }
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            MaxTime = Mathf.Clamp(MaxTime, MinTime, 10f);
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            ParticleSystem = GetComponent<ParticleSystem>();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _originalStartSize = particleSystem.startSize;
        }

        protected override void Deinitialize()
        {
        }
    }
}
