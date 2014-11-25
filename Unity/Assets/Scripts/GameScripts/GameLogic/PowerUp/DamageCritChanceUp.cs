using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [AddComponentMenu("PowerUp/DamageCritChanceUp")]
    public class DamageCritChanceUp : PowerUp 
    {
        public float ChangeAmount;

        private float _changedAmount;

        protected override void Initialize()
        {
            _changedAmount = 0f;
            base.Initialize();
        }

        protected override void Deinitialize()
        {
        }

        protected override void Apply()
        {
            if (_changedAmount >= 1.0f)
            {
                return;
            }

            float changeAmount;
            if (_changedAmount < 0.3f)
            {
                changeAmount = ChangeAmount;
            }
            else if (_changedAmount < 0.5f)
            {
                changeAmount = ChangeAmount / 2.0f;
            }
            else if (_changedAmount < 0.75f)
            {
                changeAmount = ChangeAmount / 3.0f;
            }
            else
            {
                changeAmount = ChangeAmount / 4.0f;
            }
            _changedAmount += changeAmount;
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageCriticalChanceBy, changeAmount);
        }

        protected override void UnApply()
        {
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageCriticalChanceBy, -_changedAmount);
        }
    }
}
