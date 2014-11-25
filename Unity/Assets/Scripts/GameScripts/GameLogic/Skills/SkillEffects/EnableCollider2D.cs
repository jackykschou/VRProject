using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/EnableCollider2D")]
    public class EnableCollider2D : SkillEffect 
    {
        public Collider2D Collider;

        public override void Activate()
        {
            base.Activate();
            Collider.enabled = true;
            Activated = false;
        }
    }
}
