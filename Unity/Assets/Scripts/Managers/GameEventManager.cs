using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;

namespace Assets.Scripts.Managers
{
    public class GameEventManager
    {
        public static GameEventManager Instance { get { return _instance; } }

        private readonly Dictionary<GameEvent, Dictionary<GameScript, List<MethodInfo>>> _gameEvents = new Dictionary<GameEvent, Dictionary<GameScript, List<MethodInfo>>>();

        private static readonly GameEventManager _instance = new GameEventManager();

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            if (gameEvent != GameEvent.OnGameEventSent)
            {
                TriggerGameEventLogic(GameEvent.OnGameEventSent, gameEvent);
            }

            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            TriggerGameEventLogic(gameEvent, args);
        }

        private void TriggerGameEventLogic(GameEvent gameEvent, params System.Object[] args)
        {
            var dict = _gameEvents[gameEvent];
            int originalCount = dict.Count;
            for (int i = 0; i < dict.Count; ++i)
            {
                GameScript key = dict.Keys.ElementAt(i);
                if (key.GameScriptManager.Initialized && !key.GameScriptManager.Destroyed)
                {
                    dict[key].ForEach(m => m.Invoke(key, args));
                }
                if (dict.Count != originalCount)
                {
                    originalCount = dict.Count;
                    --i;
                }
            }
        }

        public void SubscribeGameEvent(GameScript subscriber, GameEvent gameEvent, MethodInfo info)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                _gameEvents.Add(gameEvent, new Dictionary<GameScript, List<MethodInfo>>());
            }
            if (!_gameEvents[gameEvent].ContainsKey(subscriber))
            {
                _gameEvents[gameEvent].Add(subscriber, new List<MethodInfo>());
            }
            _gameEvents[gameEvent][subscriber].Add(info);
        }

        public void UnsubscribeGameEvent(GameScript subscriber, GameEvent gameEvent)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            if (!_gameEvents[gameEvent].ContainsKey(subscriber))
            {
                return;
            }
            _gameEvents[gameEvent].Remove(subscriber);
        }
    }
}
