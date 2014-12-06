using System;
using System.Collections;
using System.Reflection;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts
{
    [RequireComponent(typeof(GameScriptEditorUpdate))]
    [RequireComponent(typeof(GameScriptManager))]
    public abstract class GameScript : MonoBehaviour
    {
        public string LabelName;
        public GameScriptManager GameScriptManager { get; set; }

        private bool _firstTimeInitialized;

        public bool Initialized { get; private set; }
        public bool Deinitialized { get; private set; }
        public bool Disabled{ get; private set; }
        public bool Destroyed { get; private set; }


        public virtual void EditorUpdate()
        {
        }

        public void TriggerGameScriptEvent(GameScriptEvent gameScriptEvent, params object[] args)
        {
            StartCoroutine(TriggerGameScriptEventIe(gameScriptEvent, args));
        }

        IEnumerator TriggerGameScriptEventIe(GameScriptEvent gameScriptEvent, params object[] args)
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
            if (Initialized)
            {
                return;
            }

            if (!_firstTimeInitialized)
            {
                GameScriptManager = GetComponent<GameScriptManager>();
                GameScriptManager.UpdateGameScriptEvents(this);
                SubscribeGameEvents();
                FirstTimeInitialize();
            }

            Initialize();

            if (!_firstTimeInitialized)
            {
                gameObject.CacheGameObject();
                _firstTimeInitialized = true;
            }

            Deinitialized = false;
            Initialized = true;
            Disabled = false;
            Destroyed = false;

            GameScriptManager.UpdateInitialized();
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
            if (Deinitialized || !Initialized)
            {
                return;
            }

            if (Disabled && !PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                UnsubscribeGameEvents();
                gameObject.UncacheGameObject();
            }

            Deinitialize();
            Initialized = false;
            Deinitialized = true;
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
            if (!gameObject.activeSelf || Destroyed || GameScriptManager.Destroyed)
            {
                return;
            }

            Destroyed = true;

            TriggerGameScriptEvent(GameScriptEvent.OnObjectDestroyed);

            Disabled = true;

            if (PrefabManager.Instance.IsSpawnedFromPrefab(gameObject))
            {
                PrefabManager.Instance.DespawnPrefab(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected abstract void Initialize();

        protected abstract void Deinitialize();

        protected virtual void FirstTimeInitialize()
        {
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
