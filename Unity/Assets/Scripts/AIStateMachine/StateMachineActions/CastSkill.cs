using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using StateMachine.Action;

namespace Assets.Scripts.AIStateMachine.StateMachineActions{
	[Info (category = "Custom",
	description = "Use AICaster to cast a skill", 
	url = "")]
	public class CastSkill : StateAction
	{
	    private AISkillCaster _aiSkillCaster;

		public override void OnEnter()
		{
            if (_aiSkillCaster == null)
            {
                _aiSkillCaster = stateMachine.owner.GetComponent<AISkillCaster>();
            }
		}

		public override void OnUpdate()
		{
            _aiSkillCaster.CastSkill();
		}
	}
}