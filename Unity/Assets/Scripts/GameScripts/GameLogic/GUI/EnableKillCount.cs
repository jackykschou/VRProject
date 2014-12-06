using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Managers;
using UnityEngine;
using System.Collections;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class EnableKillCount : GameLogic
    {

        protected override void Deinitialize()
        {

        }

        [GameEvent(GameEvent.OnLevelStarted)]
        public void ShowKillCount()
        {
            MessageManager.Instance.DisplayKillCount(true);
        }

        [GameEvent(GameEvent.OnLevelEnded)]
        public void HideKillCount()
        {
            MessageManager.Instance.DisplayKillCount(false);
        }
    }   
}