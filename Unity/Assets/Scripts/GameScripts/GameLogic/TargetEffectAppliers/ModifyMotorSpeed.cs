using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("TargetEffectApplier/ModifyMotorSpeed")]
    public class ModifyMotorSpeed : TargetEffectApplier
    {
        public GameValueChanger SpeedChanger;

        protected override void ApplyEffect(GameObject target)
        {
            target.TriggerGameScriptEvent(GameScriptEvent.ChangeObjectMotorSpeed, SpeedChanger);
        }
    }
}
