using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using Assets.Scripts.Utility;
using StateMachine;
using StateMachine.Action;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
    [Info(category = "Custom",
    description = "Move Away From The Target", 
	url = "")]
    public class MoveAwayFromSkillCasterTarget : StateAction
	{
        private PathFinding _pathFinding;
        private SkillCaster _skillCaster;
        private GameObject _movePoint;

        [FieldInfo(tooltip = "Radius where the character is moving away from the target")]
        public FloatParameter Radius;

		public override void OnEnter()
		{
            if (_movePoint == null)
            {
                _movePoint = new GameObject();
                _movePoint.transform.parent = stateMachine.owner.transform;
            }
            if (_pathFinding == null)
            {
                _pathFinding = stateMachine.owner.GetComponent<PathFinding>();
            }
            if (_skillCaster == null)
            {
                _skillCaster = stateMachine.owner.GetComponent<SkillCaster>();
            }
            _movePoint.transform.position = Vector3.zero;
		}

        public override void OnExit()
        {
            _pathFinding.UpdateTarget(_skillCaster.Target.gameObject);
        }

        public override void OnFixedUpdate()
	    {
            if (stateMachine.owner.HitPointAtZero() || stateMachine.owner.IsInterrupted() || _skillCaster.Target == null)
            {
                return;
            }

            if (!UtilityFunctions.LocationPathFindingReachable(_movePoint.transform.position, stateMachine.owner.transform.position))
	        {
                _movePoint.transform.position = new Vector3(_skillCaster.Target.position.x + Random.Range(-Radius, Radius), _skillCaster.Target.position.y + Random.Range(-Radius, Radius), _skillCaster.Target.position.z);
                _pathFinding.UpdateTarget(_movePoint);
	        }

            _pathFinding.TrySearchPath();

            Vector2 moveDirection = _pathFinding.GetMoveDirection();

            if ((moveDirection == Vector2.zero) || !_pathFinding.CurrentPathReachable)
            {
                return;
            }

            stateMachine.owner.TriggerGameScriptEvent(GameScriptEvent.CharacterNonRigidMove, moveDirection);
	    }
	}
}