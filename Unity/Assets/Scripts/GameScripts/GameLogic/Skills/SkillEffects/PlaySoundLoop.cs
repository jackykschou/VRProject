using UnityEngine;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;
using Assets.Scripts.Managers;
using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/PlaySoundLoop")]
    public class PlaySoundLoop : SkillEffect
    {
        public LoopName LoopName;

        [Range(0.0f, 1.0f)]
        public float Volume = 5.0f;

        public override void Activate()
        {
            base.Activate();
            Activated = false;
        }

        public void PlaySound()
        {
            AudioManager.Instance.PlayLoop(LoopName, Volume);
        }

        public void StopSound()
        {
            AudioManager.Instance.StopLoop(LoopName);
        }
    }
}
