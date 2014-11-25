using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/RepeatSkillEffect")]
    public class RepeatSkillEffect : SkillEffect
    {
        public SkillEffect SkillEffect;
        [Range(0, 100)] 
        public int RepeatTime;
        [Range(0, 10f)]
        public float RepeatCooldown;

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(Repeat());
        }

        IEnumerator Repeat()
        {
            int repeatCounter = 0;
            while (repeatCounter < RepeatTime)
            {
                if (SkillEffect.CanActivate())
                {
                    SkillEffect.Activate();
                }
                repeatCounter++;
                if (RepeatCooldown > 0f)
                {
                     yield return new WaitForSeconds(RepeatCooldown);
                }
            }
        }
    }
}
