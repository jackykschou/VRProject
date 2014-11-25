using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    [AddComponentMenu("PlaySound/PlayWarpInOnSpawned")]
    public class PlayWarpInOnSpawned : GameLogic
    {
        public CueName Cue;

        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        public void OnSpawned()
        {
            AudioManager.Instance.PlayCue(Cue, gameObject, volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
