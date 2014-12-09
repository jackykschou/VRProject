using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PlaySound
{
    [AddComponentMenu("PlaySound/PlayTakeDamageSound")]
    public class PlayTakeDamageSound : GameLogic
    {
        public ClipName Clip;

        [Range(0.0f, 1.0f)]
        public float Volume = 1.0f;

        [GameScriptEvent(GameScriptEvent.OnObjectTakeDamage)]
        public void StartPlayDamageSound(float f, bool crit, GameValue.GameValue health, GameValueChanger gameValueChanger)
        {
            AudioManager.Instance.PlayClip(Clip, gameObject, Volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
