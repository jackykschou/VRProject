using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.ScaleWithTime
{
    public class SizeScaleWithTime : GameLogic 
    {
        [Range(0f, 10f)] 
        public float MinTime;
        [Range(0f, 10f)]
        public float MaxTime;

        private Vector3 _originalScale;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            if (time < MinTime)
            {
                transform.localScale = new Vector3(0f, 0f, _originalScale.z);
            }
            else if (time >= MaxTime)
            {
                transform.localScale = _originalScale;
            }
            else
            {
                float scale = time / MaxTime;
                transform.localScale = new Vector3(_originalScale.x * scale, _originalScale.y * scale, _originalScale.z);
            }
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            MaxTime = Mathf.Clamp(MaxTime, MinTime, 10f);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _originalScale = transform.localScale;
        }

        protected override void Deinitialize()
        {
        }
    }
}
