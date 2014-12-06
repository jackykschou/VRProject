using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/OnPlayerDeathReset")]
    public class OnPlayerDeathReset : GameLogic
    {
        [GameEvent(GameEvent.OnPlayerDeath)]
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