using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlaySoundEffect")]
    public class PlaySoundEffect : SkillEffect
    {
        public ClipName clip;

        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;

        public override void Activate()
        {
            base.Activate();
            PlaySound();
            Activated = false;
        }

        public void PlaySound()
        {
            AudioManager.Instance.PlayClip(clip,this.gameObject,volume);
        }
    }
}
