using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using Assets.Scripts.Utility;
using StateMachine;
using StateMachine.Action;
using UnityEngine;

namespace Assets.Scripts.AIStateMachine.StateMachineActions
{
    [Info(category = "Custom",
    description = "Randomly Patrol around target",
    url = "")]
    public class PatrolAroundTarget : StateAction
    {
        [FieldInfo(tooltip = "Maxium time allowed of a patrol path before renewing a new path")]
        public FloatParameter MaxiumSinglePathTime;
        [FieldInfo(tooltip = "Maximum Radius within which the patrol point is allowed to chose from")]
        public FloatParameter MaximumPatrolPointSelectionRadius;
        [FieldInfo(tooltip = "Minimum Radius within which the patrol point is allowed to chose from")]
        public FloatParameter MinimumPatrolPointSelectionRadius;

        private PathFinding _pathFinding;
        private AISkillCaster _aiSkillCaster;
        private float _currentPathPatroltime;
        private GameObject _patrolPoint;
        private Transform _targetTransform;

        public override void OnEnter()
        {
            _currentPathPatroltime = 0f;
            if (_patrolPoint == null)
            {
                _patrolPoint = new GameObject();
                _patrolPoint.transform.parent = stateMachine.owner.transform;
            }
            if (_pathFinding == null)
            {
                _pathFinding = stateMachine.owner.GetComponent<PathFinding>();
            }
            if (_aiSkillCaster == null)
            {
                _aiSkillCaster = stateMachine.owner.GetComponent<AISkillCaster>();
            }
            _targetTransform = _aiSkillCaster.Target;
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            _currentPathPatroltime += Time.fixedDeltaTime;

            if (stateMachine.owner.HitPointAtZero() || stateMachine.owner.IsInterrupted() || _targetTransform == null)
            {
                return;
            }

            if (_currentPathPatroltime >= MaxiumSinglePathTime || !_pathFinding.CurrentPathReachable || (Vector2.Distance(_patrolPoint.transform.position, stateMachine.owner.transform.position) <= 0.5f))
            {
                Vector3 newPatrolPointPosition = new Vector3(_targetTransform.position.x + Random.Range(MinimumPatrolPointSelectionRadius, MaximumPatrolPointSelectionRadius) * (UtilityFunctions.RollChance(0.5f) ? 1 : -1),
                                                             _targetTransform.position.y + Random.Range(MinimumPatrolPointSelectionRadius, MaximumPatrolPointSelectionRadius) * (UtilityFunctions.RollChance(0.5f) ? 1 : -1),
                                                             stateMachine.owner.transform.position.z);
                _patrolPoint.transform.position = newPatrolPointPosition;
                _pathFinding.UpdateTarget(_patrolPoint);
                _currentPathPatroltime = 0f;
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
