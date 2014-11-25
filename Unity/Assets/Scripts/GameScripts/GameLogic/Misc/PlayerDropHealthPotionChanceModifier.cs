using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/PlayerDropHealthPotionChanceModifier")]
    public class PlayerDropHealthPotionChanceModifier : GameLogic 
    {
        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectHealthChanged)]
        public void OnObjectHealthChanged(float changedAmount, GameValue.GameValue health)
        {
            float currentChange =
                ChanceBasedEventManager.Instance.EventCurrentChances[(int) Managers.ChanceBasedEvent.DropHealthPotion];

            if (health.Percentage < 0.1f && currentChange < 0.2f)
            {
                ChanceBasedEventManager.Instance.ChangeEventCurrentChanceTo(Managers.ChanceBasedEvent.DropHealthPotion, 0.2f);
            }
            else if (health.Percentage < 0.4f && currentChange < 0.15f)
            {
                ChanceBasedEventManager.Instance.ChangeEventCurrentChanceTo(Managers.ChanceBasedEvent.DropHealthPotion, 0.15f);
            }
            else if (health.Percentage < 0.7f && currentChange < 0.08f)
            {
                ChanceBasedEventManager.Instance.ChangeEventCurrentChanceTo(Managers.ChanceBasedEvent.DropHealthPotion, 0.08f);
            }
        }
    }
}
