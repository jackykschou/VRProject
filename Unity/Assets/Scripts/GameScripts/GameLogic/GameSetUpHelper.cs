#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public class GameSetUpHelper : GameLogic
    {
        private const string HudPrefabName = "Prefabs/GameSetUp/MainHUD";
        private const string MainCharacterPrefabName = "Prefabs/GameSetUp/MainCharacter";
        private const string AudioManagerPrefabName = "Prefabs/GameSetUp/AudioManager";
        private const string MainCameraPrefabName = "Prefabs/GameSetUp/Main Camera";
        private const string GameManagerPrefabName = "Prefabs/GameSetUp/GameManager";
        private const string LoadingScreenPrefabName = "Prefabs/GameSetUp/LoadingScreen";
        private const string PrefabManagerPrefabName = "Prefabs/GameSetUp/PrefabManager";
        private const string AStarPrefabName = "Prefabs/GameSetUp/AStar";
        private const string MessageManagerPrefabName = "Prefabs/GameSetUp/MessageManager";
        private const string ChanceBasedEventManagerName = "Prefabs/GameSetUp/ChanceBasedEventManager";

        [SerializeField]
        private GameObject _hud;
        [SerializeField]
        private GameObject _mainCharacter;
        [SerializeField]
        private GameObject _audioManager;
        [SerializeField]
        private GameObject _mainCamera;
        [SerializeField]
        private GameObject _gameManager;
        [SerializeField]
        private GameObject _loadingScreen;
        [SerializeField]
        private GameObject _prefabManager;
        [SerializeField]
        private GameObject _aStar;
        [SerializeField]
        private GameObject _messageManager;
        [SerializeField]
        private GameObject _chanceBasedEventManager;


        protected override void Initialize()
        {
            base.Initialize();
            InstantiateGameObjects();
        }

        private GameObject CreateGameObject(string gameObjectname)
        {
#if UNITY_EDITOR
            GameObject o = AssetDatabase.LoadAssetAtPath("Assets/Resources/" + gameObjectname + ".prefab", typeof(GameObject)) as GameObject;
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(o); 
#else
            GameObject o = Resources.Load(gameObjectname) as GameObject;
            GameObject instance = Instantiate(o, o.transform.position, Quaternion.identity) as GameObject;
#endif
            instance.name = o.name;
            return instance;
        }

        private void InstantiateGameObjects()
        {
            if (_hud == null)
            {
                _hud = CreateGameObject(HudPrefabName);
            }
            if (_mainCharacter == null)
            {
                _mainCharacter = CreateGameObject(MainCharacterPrefabName);
            }
            if (_audioManager == null)
            {
                _audioManager = CreateGameObject(AudioManagerPrefabName);
            }
            if (_mainCamera == null)
            {
                _mainCamera = CreateGameObject(MainCameraPrefabName);
            }
            if (_gameManager == null)
            {
                _gameManager = CreateGameObject(GameManagerPrefabName);
            }
            if (_loadingScreen == null)
            {
                _loadingScreen = CreateGameObject(LoadingScreenPrefabName);
            }
            if (_prefabManager == null)
            {
                _prefabManager = CreateGameObject(PrefabManagerPrefabName);
            }
            if (_aStar == null)
            {
                _aStar = CreateGameObject(AStarPrefabName);
            }
            if (_messageManager == null)
            {
                _messageManager = CreateGameObject(MessageManagerPrefabName);
            }
            if (_chanceBasedEventManager == null)
            {
                _chanceBasedEventManager = CreateGameObject(ChanceBasedEventManagerName);
            }
        }

        public override void EditorUpdate()
        {
            base.EditorUpdate();
            InstantiateGameObjects();
        }

        protected override void Deinitialize()
        {
        }
    }
}
