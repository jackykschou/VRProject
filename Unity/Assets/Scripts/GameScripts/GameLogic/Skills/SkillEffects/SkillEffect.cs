 using System.Collections.Generic;
 using System.Linq;
 using Assets.Scripts.Constants;
 using Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition;
 using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(Skill))]
    public abstract class SkillEffect : GameLogic
    {
        public Skill Skill;
        public bool Activated { get; protected set; }
        public List<SkillEffectActivateCondition> ActivateConditions;

        public bool CanActivate()
        {
            return ActivateConditions.All(c => c.CanActivate());
        }

        public virtual void Activate()
        {
            Activated = true;
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Skill = GetComponent<Skill>();
            if (ActivateConditions == null)
            {
                ActivateConditions = new List<SkillEffectActivateCondition>();
            }
            ActivateConditions.ForEach(c => c.SkillEffect = this);
        }

        protected override void Deinitialize()
        {
        }

        public void TriggerCasterGameScriptEvent(GameScriptEvent Event, params object[] args)
        {
            Skill.Caster.TriggerGameScriptEvent(Event, args);
        }
    }
}
