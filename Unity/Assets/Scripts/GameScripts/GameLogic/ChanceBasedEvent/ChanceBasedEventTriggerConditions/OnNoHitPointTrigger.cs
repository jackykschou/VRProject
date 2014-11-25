using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ChanceBasedEvent.ChanceBasedEventTriggerConditions
{
    [AddComponentMenu("ChanceBasedEventTrigger/OnNoHitPointTrigger")]
    public class OnNoHitPointTrigger : ChanceBasedEventTrigger 
    {
        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHasNoHitPoint)]
        public void OnObjectHasNoHitPoint()
        {
            Trigger();
        }
    }
}
