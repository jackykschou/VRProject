using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/TimeCoolDown")]
    [RequireComponent(typeof(FixTimeDispatcher))]
    public class SkillCooldown : SkillCastableCondition
    {
        public FixTimeDispatcher CoolDownDispatcher;

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();
            UpdateSkillCooldown();
        }

        public override bool CanCast()
        {
            return CoolDownDispatcher.CanDispatch();
        }

        public void UpdateSkillCooldown()
        {
            TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateSkillCooldownPercentage, Skill, CoolDownDispatcher.DispatchCoolDownPercentage);
        }

        [GameScriptEvent(Constants.GameScriptEvent.SkillCastTriggerSucceed)]
        public void ResetCooldown(Skill skill)
        {
            if (skill == Skill)
            {
                CoolDownDispatcher.Dispatch();
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.RefreshSkillCoolDown)]
        public void RefreshSkillCoolDown()
        {
            CoolDownDispatcher.TurnDispatchable();
        }
    }
}
