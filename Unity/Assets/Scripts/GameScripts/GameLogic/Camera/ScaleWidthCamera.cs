using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class ScaleWidthCamera : GameLogic 
    {
        [Range(0, float.MaxValue)]
        public float TargetWidth = 1250;

        private float _targetWidth;

        protected override void Update()
        {
            base.Update();
            float height = _targetWidth / Screen.width * Screen.height;
            camera.orthographicSize = (int)(height / Constants.WorldScaleConstant.PixelToUnit / 2f);
        }

        
        [GameEventAttribute(GameEvent.SetCameraWidth)]
        public void SetCameraWidth(float pixelDensity)
        {
            _targetWidth = pixelDensity;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _targetWidth = TargetWidth;
        }

        protected override void Deinitialize()
        {
        }
    }
}
