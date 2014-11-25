using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(FixTimeDispatcher))]
    [AddComponentMenu("Misc/InterruptHitDamageThreshold")]
    public class InterruptHitDamageThreshold : GameLogic 
    {
        [Range(0f, float.MaxValue)]
        public float HitDamageThreshold;

        public FixTimeDispatcher HitDamageThresholdResetTime;

        private float _accumulatedHitDamage;

        [GameScriptEventAttribute(GameScriptEvent.OnObjectTakeDamage)]
        public void UpdateDamageThreshold(float damage, bool crit, GameValue.GameValue health, GameValueChanger gameValueChanger)
        {
            if (HitDamageThresholdResetTime.CanDispatch())
            {
                if (damage >= HitDamageThreshold)
                {
                    TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
                }
                else
                {
                    _accumulatedHitDamage += damage;
                }
            }
            else
            {
                _accumulatedHitDamage += damage;
                if (_accumulatedHitDamage >= HitDamageThreshold)
                {
                    TriggerGameScriptEvent(GameScriptEvent.InterruptCharacter);
                }
            }
            HitDamageThresholdResetTime.ResetTime();
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCharacterInterrupted)]
        public void ResetAccumulatedHitDamage()
        {
            _accumulatedHitDamage = 0f;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _accumulatedHitDamage = 0f;
        }

        protected override void Deinitialize()
        {
        }
        
    }
}
