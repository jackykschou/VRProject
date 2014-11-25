using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Managers;
using UnityEngine;
using System.Collections;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class EnableKillCount : GameLogic
    {

        protected override void Deinitialize()
        {

        }

        [GameEventAttribute(GameEvent.OnLevelStarted)]
        public void ShowKillCount()
        {
            MessageManager.Instance.DisplayKillCount(true);
        }

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void HideKillCount()
        {
            MessageManager.Instance.DisplayKillCount(false);
        }
    }   
}