using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlaySoundCue")]
    public class PlaySoundCue : SkillEffect
    {
        public CueName Cue;

        [Range(0.0f, 1.0f)]
        public float Volume = 1.0f;

        public override void Activate()
        {
            base.Activate();
            PlaySound();
            Activated = false;
        }

        public void PlaySound()
        {
            AudioManager.Instance.PlayCue(Cue, this.gameObject, Volume);
        }
    }
}
