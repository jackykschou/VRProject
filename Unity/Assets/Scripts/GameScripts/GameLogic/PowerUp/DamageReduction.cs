using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [AddComponentMenu("PowerUp/DamageReduction")]
    public class DamageReduction : PowerUp
    {
        public float ChangeAmount;

        private float _changedAmount;

        protected override void Deinitialize()
        {
        }

        protected override void Initialize()
        {
            _changedAmount = 0f;
            base.Initialize();
        }

        protected override void Apply()
        {
            if (_changedAmount >= 0.8f)
            {
                return;
            }

            float changeAmount = ChangeAmount / AppliedCounter;
            _changedAmount += changeAmount;
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageReductionBy, changeAmount);
        }

        protected override void UnApply()
        {
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeDamageReductionBy, -_changedAmount);
        }
    }
}
