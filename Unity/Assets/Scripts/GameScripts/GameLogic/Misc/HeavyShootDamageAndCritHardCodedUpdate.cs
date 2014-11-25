using Assets.Scripts.Attributes;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/HeavyShootDamageAndCritHardCodedUpdate")]
    public class HeavyShootDamageAndCritHardCodedUpdate : GameLogic
    {
        private float _critChangeAmount;
        private float _damagePercentage;

        [GameScriptEvent(Constants.GameScriptEvent.ChangeDamageCriticalChanceBy)]
        public void ChangeDamageCriticalChanceBy(float changeAmount)
        {
            _critChangeAmount += changeAmount;
        }

        [GameScriptEvent(Constants.GameScriptEvent.ChangeHealthChangerDamageRawAmountToInitialPercentage)]
        public void ChangeHealthChangerDamageRawAmountToInitialPercentage(float percentage)
        {
            _damagePercentage = percentage;
        }

        [GameScriptEvent(Constants.GameScriptEvent.HeavyChargeShootCritChangeAndDamageUpdate)]
        public void HeavyChargeShootCritChangeAndDamageUpdate(GameObject target)
        {
            target.TriggerGameScriptEvent(Constants.GameScriptEvent.ChangeDamageCriticalChanceBy, _critChangeAmount);
            target.TriggerGameScriptEvent(Constants.GameScriptEvent.ChangeHealthChangerDamageRawAmountToInitialPercentage, _damagePercentage);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _critChangeAmount = 0f;
            _damagePercentage = 1f;
        }

        protected override void Deinitialize()
        {
        }
    }
}
