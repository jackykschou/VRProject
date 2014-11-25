using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/OneTimeOnly")]
    public class OneTimeOnly : SkillCastableCondition
    {
        private bool _alreadyActivated;

        protected override void Initialize()
        {
            base.Initialize();
            _alreadyActivated = false;
        }

        protected override void Deinitialize()
        {
        }

        public override bool CanCast()
        {
            return !_alreadyActivated;
        }

        [GameScriptEvent(Constants.GameScriptEvent.SkillCastTriggerSucceed)]
        public void ResetCooldown(Skill skill)
        {
            if (skill == Skill)
            {
                _alreadyActivated = true;
            }
        }
    }
}
