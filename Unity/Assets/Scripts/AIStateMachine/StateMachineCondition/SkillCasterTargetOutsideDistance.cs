using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using StateMachine;
using StateMachine.Condition;
using UnityEngine;

namespace Assets.Scripts.AIStateMachine.StateMachineCondition
{
	[Info (category = "Custom",
	description = "Detect if the target of skillcaster is outside of a specific distance", 
	url = "")]
    public class SkillCasterTargetOutsideDistance : StateCondition
	{
        [FieldInfo(tooltip = "The specific distance")]
        public FloatParameter Distance;

        private SkillCaster _skillCaster;

		public override void OnEnter()
		{
            if (_skillCaster == null)
            {
                _skillCaster = stateMachine.owner.GetComponent<SkillCaster>();
            }
		}

		public override bool Validate()
		{
            if (_skillCaster.Target == null)
            {
                return false;
            }
            return Vector2.Distance(_skillCaster.Target.position, stateMachine.owner.transform.position) > Distance;
		}
	}
}