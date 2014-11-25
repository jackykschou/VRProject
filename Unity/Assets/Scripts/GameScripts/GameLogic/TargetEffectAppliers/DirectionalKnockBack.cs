using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/DirectionalKnockBack")]
    [RequireComponent(typeof(PositionIndicator))]
    public class DirectionalKnockBack : TargetEffectApplier 
    {
        public float KnockBackSpeed;
        public float Time;
        public PositionIndicator PositionIndicator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PositionIndicator == null)
            {
                PositionIndicator = GetComponent<PositionIndicator>();
            }
        }

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.OnCharacterKnockBacked, PositionIndicator.Direction.normalized, KnockBackSpeed, Time);
        }
    }
}
