﻿#if DEBUG
using Assets.Scripts.Attributes;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.Tests.GameLogic
{
    public class SampleGameLogic : GameScripts.GameLogic.GameLogic
    {
        protected override void Initialize()
        {
            Debug.Log("SampleGameLogic Initialize");


            TriggerGameScriptEvent(Constants.GameScriptEvent.Example);

            TriggerGameEvent(GameEvent.ExampleEvent);
        }

        protected override void Deinitialize()
        {
            Debug.Log("SampleGameLogic Deinitialize");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        public void GameScriptEventPublic()
        {
            Debug.Log("SampleGameLogic GameScriptEventPublic");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        private void GameScriptEventPrivate()
        {
            Debug.Log("SampleGameLogic GameScriptEventPrivate");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        public static void GameScriptEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameScriptEventStaticPublic");
        }

        [GameScriptEvent(Constants.GameScriptEvent.Example)]
        private static void GameScriptEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameScriptEventStaticPrivate");
        }

        [GameEvent(GameEvent.ExampleEvent)]
        public void GameEventPublic()
        {
            Debug.Log("SampleGameLogic GameEventPublic");
        }

        [GameEvent(GameEvent.ExampleEvent)]
        private void GameEventPrivate()
        {
            Debug.Log("SampleGameLogic GameEventPrivate");
        }

        [GameEvent(GameEvent.ExampleEvent)]
        public static void GameEventStaticPublic()
        {
            Debug.Log("SampleGameLogic GameEventStaticPublic");
        }

        [GameEvent(GameEvent.ExampleEvent)]
        private static void GameEventStaticPrivate()
        {
            Debug.Log("SampleGameLogic GameEventStaticPrivate");
        }
    }
}
#endif
