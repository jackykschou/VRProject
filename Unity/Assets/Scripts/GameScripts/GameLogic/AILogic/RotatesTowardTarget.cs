using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.AILogic
{
    [AddComponentMenu("AILogic/RotateTowardsTarget")]
    public class RotatesTowardTarget : GameLogic
    {
        public Transform Target;

        private SkillCaster _skillCaster;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _skillCaster = GetComponent<SkillCaster>();
        }

        [GameScriptEventAttribute(GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform;
        }

        [GameScriptEventAttribute(GameScriptEvent.RotateTowardsTarget)]
        public void RotateTowardsTarget()
        {
            if (Target == null)
            {
                return;
            }

            if (_skillCaster != null && _skillCaster.CastingActiveSkill)
            {
                return;
            }

            FacingDirection newDirection = UtilityFunctions.GetDirection(transform.position, Target.position).GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
