using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/DamageReduction")]
    public class DamageReduction : SkillEffect 
    {
        public float ChangeAmount;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.gameObject.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageReductionBy, ChangeAmount);
            Activated = false;
        }
    }
}
