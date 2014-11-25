using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using Assets.Scripts.Utility;
using StateMachine.Action;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
	[Info (category = "Custom",
    description = "Rotate towards target of the skill caster", 
	url = "Link")]
	public class RotateTowardsTarget : StateAction
	{
        private RotatesTowardTarget _rotatesTowardTarget;

		public override void OnEnter()
		{
            if (_rotatesTowardTarget == null)
            {
                _rotatesTowardTarget = stateMachine.owner.GetComponent<RotatesTowardTarget>();
            }
		}

		public override void OnUpdate()
		{
		    if (stateMachine.owner.HitPointAtZero() || stateMachine.owner.IsInterrupted())
		    {
		        return;
		    }

            _rotatesTowardTarget.RotateTowardsTarget();
		}
	}
}