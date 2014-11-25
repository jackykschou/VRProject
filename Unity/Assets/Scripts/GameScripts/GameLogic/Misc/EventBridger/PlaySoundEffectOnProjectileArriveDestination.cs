using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [AddComponentMenu("Misc/PlaySoundEffectOnProjectileArriveDestination")]
    public class PlaySoundEffectOnProjectileArriveDestination : GameLogic
    {
        public ClipName Clip;

        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.OnProjectileArriveDestination)]
        public void OnProjectileArriveDestination(Vector2 destination)
        {
            AudioManager.Instance.PlayClip(Clip, gameObject, volume);
        }

        protected override void Deinitialize()
        {
        }
    }
}
