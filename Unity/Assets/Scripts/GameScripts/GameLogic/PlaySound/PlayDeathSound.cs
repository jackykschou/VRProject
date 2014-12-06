using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    public class PlayDeathSound : GameLogic 
    {
        public ClipName Clip;

        [Range(0.0f, 1.0f)]
        public float Volume = 1.0f;

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void StartPlayDeathSound()
        {
            AudioManager.Instance.PlayClip(Clip, gameObject, Volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
