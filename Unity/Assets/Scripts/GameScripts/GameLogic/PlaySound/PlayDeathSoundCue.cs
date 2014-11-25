using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    [AddComponentMenu("PlaySound/PlayDeathSoundCue")]
    public class PlayDeathSoundCue : GameLogic
    {
        public CueName Cue;

        [Range(0.0f, 1.0f)]
        public float Volume = 1.0f;

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void StartPlayDeathSound()
        {
            AudioManager.Instance.PlayCue(Cue, gameObject, Volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
