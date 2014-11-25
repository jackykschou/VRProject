using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    public class PlayFootStep : GameLogic 
    {
        public ClipName Clip;

        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        public void PlayFootStepSound()
        {
            AudioManager.Instance.PlayClip(Clip, gameObject, volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
