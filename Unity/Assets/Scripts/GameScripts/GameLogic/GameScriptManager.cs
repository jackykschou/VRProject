﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameScriptManager : MonoBehaviour 
    {
        private Dictionary<Type, Dictionary<GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>> _gameScriptEvents;
        private List<GameScript> _gameScripts;

        public bool Initialized 
        {
            get
            {
                return _initialized && _gameScriptsInitialized;
            }
        }
        public bool Disabled { get; set; }
        public bool Destroyed { get; set; }

        private bool _initialized;
        private bool _gameScriptsInitialized;
        private bool _firstTimeInitialized;
        private bool _deinitialized;

        public void UpdateInitialized()
        {
            _gameScriptsInitialized = _gameScripts.All(s => s.Initialized);
        }

        public void TriggerGameScriptEvent(GameScriptEvent gameScriptEvent, params object[] args)
        {
            if (Destroyed)
            {
                return;
            }

            foreach (var value in _gameScriptEvents.Values)
            {
                if (value.ContainsKey(gameScriptEvent))
                {
                    foreach (var pair in value[gameScriptEvent])
                    {
                        pair.Value.ForEach(m => m.Invoke(pair.Key, args));
                    }
                }
            }
        }

        void Start()
        {
            InitializeHelper();
        }

        void OnEnable()
        {
            InitializeHelper();
        }

        void OnSpawned()
        {
            InitializeHelper();
        }

        private void InitializeHelper()
        {
            if (_initialized)
            {
                return;
            }

            if (!_firstTimeInitialized)
            {
                _gameScriptEvents = new Dictionary<Type, Dictionary<GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>>();
                _gameScripts = GetComponents<GameScript>().ToList();
                _firstTimeInitialized = true;
                gameObject.CacheGameObject();
            }

            UpdateInitialized();

            _initialized = true;
            _deinitialized = false;
            Disabled = false;
            Destroyed = false;
        }

        void OnDespawned()
        {
            DeinitializeHelper();
        }

        void OnDisable()
        {
            DeinitializeHelper();
        }

        void OnDestroy()
        {
            DeinitializeHelper();
            gameObject.UncacheGameObject();
        }

        private void DeinitializeHelper()
        {
            if (_deinitialized || !_initialized)
            {
                return;
            }
            _gameScriptsInitialized = false;
            _initialized = false;
            _deinitialized = true;
        }

        public void UpdateGameScriptEvents(GameScript gameScript)
        {
            InitializeHelper();
            AddGameScriptEvents(gameScript);
        }

        private void AddGameScriptEvents(GameScript gameScript)
        {
            gameScript.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList()
                .ForEach(m =>
                {
                    foreach (var a in Attribute.GetCustomAttributes(m, typeof(GameScriptEventAttribute)))
                    {
                        GameScriptEventAttribute gameScriptEventAttribute = a as GameScriptEventAttribute;
                        if (gameScriptEventAttribute != null)
                        {
                            Type gameScriptType = gameScript.GetType();
                            if (!_gameScriptEvents.ContainsKey(gameScriptType))
                            {
                                _gameScriptEvents.Add(gameScriptType, new Dictionary<GameScriptEvent, Dictionary<GameScript, List<MethodInfo>>>());
                            }
                            if (!_gameScriptEvents[gameScriptType].ContainsKey(gameScriptEventAttribute.Event))
                            {
                                _gameScriptEvents[gameScriptType].Add(gameScriptEventAttribute.Event, new Dictionary<GameScript, List<MethodInfo>>());
                            }
                            if (!_gameScriptEvents[gameScriptType][gameScriptEventAttribute.Event].ContainsKey(gameScript))
                            {
                                _gameScriptEvents[gameScriptType][gameScriptEventAttribute.Event].Add(gameScript, new List<MethodInfo>());
                            }
                            _gameScriptEvents[gameScriptType][gameScriptEventAttribute.Event][gameScript].Add(m);
                        }
                    }
                });
        }
    }
}
