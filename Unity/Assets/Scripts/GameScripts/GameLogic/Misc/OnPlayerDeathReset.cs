using Assets.Scripts.Managers;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/OnPlayerDeathReset")]
    public class OnPlayerDeathReset : GameLogic
    {
        [GameEventAttribute(GameEvent.OnPlayerDeath)]
        public void ShowDeathMessage()
        {
            MessageManager.Instance.DisplayDeathMessage();
        }


        // Use this for deinitialization
        protected override void Deinitialize()
        {
        }
    }
}