using Assets.Scripts.Managers;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/TeleportPlayer")]
    public class TeleportPlayer : TargetEffectApplier
    {
        protected override void ApplyEffect(GameObject target)
        {
            GameEventManager.Instance.TriggerGameEvent(GameEvent.SurvivalSectionEnded);
        }
    }
}
