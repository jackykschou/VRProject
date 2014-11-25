using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.AILogic;
using StateMachine.Condition;
using UnityEngine;

namespace Assets.Scripts.AIStateMachine.StateMachineCondition
{
	[Info (category = "Custom",
	description = "Detected an enemy", 
	url = "")]
	public class EnemyDetected : StateCondition
	{
		public override void OnEnter()
		{
		
		}

		public override bool Validate()
		{
            GameObject target = stateMachine.owner.GetComponent<EnemyDetection>().FindClosestEnemy();
		    if (target != null)
		    {
                stateMachine.owner.GetComponent<EnemyDetection>().TriggerGameScriptEvent(GameScriptEvent.OnNewTargetDiscovered, target);
		        return true;
		    }
            return false;
		}
	}
}