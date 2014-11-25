using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using StateMachine.Condition;

namespace Assets.Scripts.AIStateMachine.StateMachineCondition
{
	[Info (category = "Custom",
	description = "AI caster has a skill to cast", 
	url = "")]
	public class CanCastSkill : StateCondition
	{
		public override void OnEnter()
		{
		
		}

		public override bool Validate()
		{
            return stateMachine.owner.gameObject.GetComponent<AISkillCaster>().CanCastAnySkill();
		}
	}
}