using Assets.Scripts.Utility;
using StateMachine;
using StateMachine.Action;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
	[Info (category = "Custom",
	description = "Follow target", 
	url = "")]
	public class FollowTarget : StateAction
	{
        [FieldInfo(tooltip = "AI will not move if distance between the target is within distance")]
	    public FloatParameter MinimumDistance;

	    private PathFinding _pathFinding;

	    public override void OnEnter()
		{
            if (_pathFinding == null)
            {
                _pathFinding = stateMachine.owner.GetComponent<PathFinding>();
            }
		}

	    public override void OnFixedUpdate()
	    {
            if (stateMachine.owner.HitPointAtZero() || stateMachine.owner.IsInterrupted())
            {
                return;
            }

            _pathFinding.TrySearchPath();

            Vector2 moveDirection = _pathFinding.GetMoveDirection();

            if (_pathFinding.Target == null || (Vector2.Distance(_pathFinding.Target.position, stateMachine.owner.transform.position) <= MinimumDistance) ||
                (moveDirection == Vector2.zero) || !_pathFinding.CurrentPathReachable)
            {
                stateMachine.owner.TriggerGameScriptEvent(GameScriptEvent.RotateTowardsTarget);
                return;
            }

            stateMachine.owner.TriggerGameScriptEvent(GameScriptEvent.CharacterNonRigidMove, moveDirection);
	    }
	}
}