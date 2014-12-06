using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.GameScripts.GameLogic.Health;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class GameObjectExtenstion
    {
        public static readonly Dictionary<GameObject, GameScriptManager> GameScriptEventManagersCache = new Dictionary<GameObject, GameScriptManager>();
        public static readonly Dictionary<GameObject, Health> HealthCache = new Dictionary<GameObject, Health>();
        public static readonly Dictionary<GameObject, CharacterInterrupt> OnHitInterruptCache = new Dictionary<GameObject, CharacterInterrupt>();

        public static void CacheGameObject(this GameObject o)
        {
            GameScriptManager gameScriptManager = o.GetComponent<GameScriptManager>();
            Health health = o.GetComponent<Health>();
            CharacterInterrupt characterInterrupt = o.GetComponent<CharacterInterrupt>();

            if (gameScriptManager != null && !GameScriptEventManagersCache.ContainsKey(o))
            {
                GameScriptEventManagersCache.Add(o, gameScriptManager);
            }
            if (health != null && !HealthCache.ContainsKey(o))
            {
                HealthCache.Add(o, health);
            }
            if (characterInterrupt != null && !OnHitInterruptCache.ContainsKey(o))
            {
                OnHitInterruptCache.Add(o, characterInterrupt);
            }
        }

        public static void UncacheGameObject(this GameObject o)
        {
            GameScriptEventManagersCache.Remove(o);
            HealthCache.Remove(o);
            OnHitInterruptCache.Remove(o);
        }

        public static void TriggerGameScriptEvent(this GameObject o, GameScriptEvent gameScriptEvent, params object[] args)
        {
            if (GameScriptEventManagersCache.ContainsKey(o))
            {
                GameScriptEventManagersCache[o].TriggerGameScriptEvent(gameScriptEvent, args);
            }

            foreach (Transform t in o.transform)
            {
                TriggerGameScriptEvent(t.gameObject, gameScriptEvent, args);
            }

        }

        public static bool IsInterrupted(this GameObject o)
        {
            if (!OnHitInterruptCache.ContainsKey(o))
            {
                return false;
            }

            return OnHitInterruptCache[o].Interrupted;
        }

        public static bool HitPointAtZero(this GameObject o)
        {
            if (!HealthCache.ContainsKey(o))
            {
                return false;
            }

            return HealthCache[o].HitPointAtZero;
        }
    }
}
