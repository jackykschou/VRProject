using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/DisableCollider2D")]
    public class DisableCollider2D : SkillEffect
    {
        public Collider2D Collider;

        public override void Activate()
        {
            base.Activate();
            Collider.enabled = false;
            Activated = false;
        }
    }
}
