using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Camera;
using Assets.Scripts.Managers;
using UnityEngine;
namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionScaleCamera")]
    public class SectionScaleCamera : SectionLogic
    {
        [Range(0.0f, 5000f)]
        public float ScaleTo = 2000.0f;

        private float _origScale;

        protected override void Initialize()
        {
            base.Initialize();
            _origScale = GameManager.Instance.MainCamera.GetComponent<ScaleWidthCamera>().TargetWidth;
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                TriggerGameEvent(GameEvent.SetCameraWidth, ScaleTo);
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId)
            {
                TriggerGameEvent(GameEvent.SetCameraWidth, _origScale);
            }
        }
        protected override void Deinitialize()
        {
        }
    }
}

