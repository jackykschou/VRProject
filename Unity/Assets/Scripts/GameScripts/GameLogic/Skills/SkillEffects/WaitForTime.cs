using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/WaitForTime")]
    public class WaitForTime : SkillEffect 
    {
        [Range(0, 10)]
        public float WaitTime;

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(StartWaitForTime());
        }

        IEnumerator StartWaitForTime()
        {
            yield return new WaitForSeconds(WaitTime);
            Activated = false;
        }
    }
}
