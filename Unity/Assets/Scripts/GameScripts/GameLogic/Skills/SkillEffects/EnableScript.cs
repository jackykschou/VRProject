using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/EnableScript")]
    public class EnableScript : SkillEffect 
    {
        public MonoBehaviour Script;

        public override void Activate()
        {
            base.Activate();
            Script.enabled = true;
            Activated = false;
        }
    }
}
