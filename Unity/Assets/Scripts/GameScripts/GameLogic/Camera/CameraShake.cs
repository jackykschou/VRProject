using System.Collections;
using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Camera
{
    public class CameraShake : GameLogic
    {
        private bool _shaking;

        protected override void Initialize()
        {
            _shaking = false;
        }

        [GameEvent(Constants.GameEvent.ShakeCamera)]
        public void ShakeCamera(float shakeIntensity, float duration)
        {
            if (_shaking)
            {
                return;
            }
            StartCoroutine(ShakeCameraIE(shakeIntensity, duration));
        }

        protected override void Deinitialize()
        {
        }

        private IEnumerator ShakeCameraIE(float shakeIntensity, float duration)
        {
            _shaking = true;
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float percentComplete = elapsed / duration;
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                x *= shakeIntensity * damper;
                y *= shakeIntensity * damper;

                Vector3 originalPos = UnityEngine.Camera.main.transform.position;
                UnityEngine.Camera.main.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

                yield return null;
            }

            _shaking = false;
        }
    }
}