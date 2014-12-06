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

        private Dictionary<GameEvent, Dictionary<GameScript, List<MethodInfo>>> _gameEvents;

        private static readonly GameEventManager _instance = new GameEventManager();

        GameEventManager()
        {
            _gameEvents = new Dictionary<GameEvent, Dictionary<GameScript, List<MethodInfo>>>();
        }

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            if (gameEvent != GameEvent.OnGameEventSent)
            {
                TriggerGameEventSent(GameEvent.OnGameEventSent, gameEvent);
            }

            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            var dict = _gameEvents[gameEvent];
            int originalCount = dict.Count;
            for (int i = 0; i < dict.Count; ++i)
            {
                GameScript key = dict.Keys.ElementAt(i);
                if (key.GameScriptManager.Initialized && !key.GameScriptManager.Disabled)
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

        private void TriggerGameEventSent(GameEvent gameEvent, params System.Object[] args)
        {
            if (!_gameEvents.ContainsKey(gameEvent))
            {
                return;
            }

            var dict = _gameEvents[gameEvent];
            int originalCount = dict.Count;
            for (int i = 0; i < dict.Count; ++i)
            {
                var key = dict.Keys.ElementAt(i);
                dict[key].ForEach(m => m.Invoke(key, args));
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
