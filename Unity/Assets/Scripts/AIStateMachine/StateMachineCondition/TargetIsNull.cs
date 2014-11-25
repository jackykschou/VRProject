using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using Assets.Scripts.Utility;
using StateMachine.Condition;

namespace Assets.Scripts.AIStateMachine.StateMachineCondition
{
	[Info (category = "Custom",
	description = "The Target of the skill caster is null", 
	url = "")]
	public class TargetIsNull : StateCondition
	{
		public override void OnEnter()
		{
		
		}

		public override bool Validate()
		{
            return stateMachine.owner.gameObject.GetComponent<SkillCaster>().Target == null || stateMachine.owner.gameObject.GetComponent<SkillCaster>().Target.gameObject.HitPointAtZero();
		}
	}
}