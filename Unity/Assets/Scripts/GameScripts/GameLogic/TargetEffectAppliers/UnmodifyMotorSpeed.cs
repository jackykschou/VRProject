using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("TargetEffectApplier/UnmodifyMotorSpeed")]
    public class UnmodifyMotorSpeed : TargetEffectApplier
    {
        public GameValueChanger SpeedChanger;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.UnchangeObjectMotorSpeed, SpeedChanger);
        }
    }
}
