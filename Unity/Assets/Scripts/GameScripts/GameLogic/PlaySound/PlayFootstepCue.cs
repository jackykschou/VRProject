using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    public class PlayFootStepCue : GameLogic
    {
        public CueName Cue;

        [Range(0.0f, 1.0f)]
        public float Volume = 1.0f;

        public void PlayFootStepSound()
        {
            AudioManager.Instance.PlayCue(Cue, gameObject, Volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
