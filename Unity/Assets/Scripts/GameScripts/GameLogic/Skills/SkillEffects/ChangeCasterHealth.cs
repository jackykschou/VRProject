using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ChangeCasterHealth")]
    [RequireComponent(typeof(HealthChanger))]
    public class ChangeCasterHealth : SkillEffect
    {
        public HealthChanger HealthChanger;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.ObjectChangeHealth, HealthChanger);
            Activated = false;
        }
    }
}
