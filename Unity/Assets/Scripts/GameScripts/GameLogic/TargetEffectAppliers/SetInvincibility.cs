using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/SetInvincibility")]
    public class SetInvincibility : TargetEffectApplier
    {
        public bool Enable;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.SetHealthInvincibility, Enable);
        }
    }
}
