using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using StateMachine.Condition;

namespace Assets.Scripts.AIStateMachine.StateMachineCondition{
	[Info (category = "Custom",
	description = "AICaster does not have any skill to cast", 
	url = "")]
	public class NoSkillToCast : StateCondition
	{
		public override void OnEnter()
		{
		
		}

		public override bool Validate()
		{
            return !stateMachine.owner.gameObject.GetComponent<AISkillCaster>().CanCastAnySkill()
                && !stateMachine.owner.gameObject.GetComponent<AISkillCaster>().CastingActiveSkill;
		}
	}
}