using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/DisableScript")]
    public class DisableScript : SkillEffect 
    {
        public MonoBehaviour Script;

        public override void Activate()
        {
            base.Activate();
            Script.enabled = false;
            Activated = false;
        }
    }
}
