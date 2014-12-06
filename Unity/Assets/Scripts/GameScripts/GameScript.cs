using System;
using System.Collections;
using System.Reflection;
using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts
{
    [RequireComponent(typeof(GameScriptEditorUpdate))]
    [RequireComponent(typeof(GameScriptManager))]
    public abstract class GameScript : MonoBehaviour
    {
        public string LabelName;
        public GameScriptManager GameScriptManager { get; set; }

        private bool _firstTimeInitialized;

        public bool Initialized {
            get { return _initialized; }
        }
        private bool _initialized;
        public bool Deinitialized
        {
            get { return _deinitialized; }
        }
        private bool _deinitialized;
        public bool Disabled
        {
            get { return _disabled; }
        }
        private bool _disabled;
        public bool Destroyed
        {
            get { return _destroyed; }
        }
        private bool _destroyed;

        protected virtual void FirstTimeInitialize()
        {
        }

        protected abstract void Initialize();

        protected abstract void Deinitialize();

        public virtual void EditorUpdate()
        {
        }

        public void TriggerGameScriptEvent(GameScriptEvent gameScriptEvent, params object[] args)
        {
            StartCoroutine(TriggerGameScriptEventIE(gameScriptEvent, args));
        }

        IEnumerator TriggerGameScriptEventIE(GameScriptEvent gameScriptEvent, params object[] args)
        {
            while (GameScriptManager == null || !GameScriptManager.Initialized)
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }

            gameObject.TriggerGameScriptEvent(gameScriptEvent, args);
        }

        public void TriggerGameEvent(GameEvent gameEvent, params System.Object[] args)
        {
            GameEventManager.Instance.TriggerGameEvent(gameEvent, args);
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

        void InitializeHelper()
        {
            if (_initialized)
            {
                return;
            }

            if (!_firstTimeInitialized)
            {
                InitializeFields();
                SubscribeGameEvents();
                FirstTimeInitialize();
            }

            Initialize();

            if (!_firstTimeInitialized)
            {
                gameObject.CacheGameObject();
                _firstTimeInitialized = true;
            }
            _deinitialized = false;
            _initialized = true;
            _disabled = false;
            _destroyed = false;
            GameScriptManager.UpdateInitialized();
        }

        private void InitializeFields()
        {
            GameScriptManager = GetComponent<GameScriptManager>();
            GameScriptManager.UpdateGameScriptEvents(this);
        }

        private void SubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEventAttribute))
                    {
                        GameEventAttribute gameEventSubscriberAttribute = attr as GameEventAttribute;
                        GameEventManager.Instance.SubscribeGameEvent(this, gameEventSubscriberAttribute.Event, info);
                    }
                }
            }
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
        }

        void DeinitializeHelper()
        {
            if (_deinitialized || !_initialized)
            {
                return;
            }

            if (_disabled && !PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                UnsubscribeGameEvents();
                gameObject.UncacheGameObject();
            }

            Deinitialize();
            _initialized = false;
            _deinitialized = true;
        }

        private void UnsubscribeGameEvents()
        {
            foreach (var info in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var attr in Attribute.GetCustomAttributes(info))
                {
                    if (attr.GetType() == typeof(GameEventAttribute))
                    {
                        GameEventAttribute gameEventSubscriberAttribute = attr as GameEventAttribute;
                        GameEventManager.Instance.UnsubscribeGameEvent(this, gameEventSubscriberAttribute.Event);
                    }
                }
            }
        }

        public void DisableGameObject(float delay = 0f)
        {
            StartCoroutine(DisableGameObjectIE(delay));
        }

        IEnumerator DisableGameObjectIE(float delay)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            ImmediateDisableGameObject();
        }

        public void ImmediateDisableGameObject()
        {
            if (!gameObject.activeSelf || _destroyed || GameScriptManager.Destroyed)
            {
                return;
            }

            _destroyed = true;

            TriggerGameScriptEvent(GameScriptEvent.OnObjectDestroyed);

            _disabled = true;

            if (PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                PrefabManager.Instance.DespawnPrefab(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        {
        }

        protected virtual void OnTriggerEnter2D(Collider2D coll)
        { }

        protected virtual void OnTriggerStay2D(Collider2D coll)
        { }

        protected virtual void OnTriggerExit2D(Collider2D coll)
        { }

        protected virtual void OnCollisionEnter2D(Collision2D coll)
        { }

        protected virtual void OnCollisionStay2D(Collision2D coll)
        { }

        protected virtual void OnCollisionExit2D(Collision2D coll)
        { }
    }
}
