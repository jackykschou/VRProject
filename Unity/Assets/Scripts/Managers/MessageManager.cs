using System.Collections;
using UnityEngine;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor;


namespace Assets.Scripts.Managers
{
    [AddComponentMenu("Manager/MessageManager")]
    [ExecuteInEditMode]
    public class MessageManager : MonoBehaviour
    {
        public GameObject DeathScreen;
        public GameObject KillCountGUI;
        public PrefabSpawner PrefabSpawner;
        public GameObject TipText;
        public Camera MainCamera;
        public EaseType PreferredEaseType;

        private static MessageManager _instance;
        public static MessageManager Instance
        {
            get
            {
                if (_instance != null) 
                    return _instance;
                _instance = FindObjectOfType<MessageManager>();
                DontDestroyOnLoad(_instance.gameObject);
                return _instance;
            }
        }

        public void SetTipText(string tipText)
        {
            TipText.GetComponent<UnityEngine.UI.Text>().text = tipText;
        }

        public void DisplayDeathMessage()
        {
            DeathScreen.SetActive(true);
            StartCoroutine(DeactivateObjectIE(3.0f));
            StartCoroutine(FadeInDeathScreenIE(3.0f));
        }

        public void DisplayKillCount(bool active)
        {
            KillCountGUI.SetActive(active);
        }

        IEnumerator DeactivateObjectIE(float time)
        {
            while (time > 0)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                time -= Time.deltaTime;
            }
            DeathScreen.SetActive(false);
        }
        IEnumerator FadeInDeathScreenIE(float time)
        {
            float passedTime = 0.0f;
            UnityEngine.UI.Image image = DeathScreen.GetComponentInChildren<UnityEngine.UI.Image>();
            while (time > 0)
            {
                image.color = new Color(image.color.r,image.color.g,image.color.b,passedTime/time); 
                yield return new WaitForSeconds(Time.deltaTime);
                time -= Time.deltaTime;
                passedTime += Time.deltaTime;
            }
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f); 
        }

        public void DisplayMessage(string message,Vector3 direction, float despawnTime = 3.0f)
        {
            PrefabSpawner.SpawnPrefab(TopMiddleOfScreen(), o =>
            {
                TextMesh mesh = o.GetComponent<TextMesh>();
                TextMotor motor = o.GetComponent<TextMotor>();
                DamageTextDespawn dtd = o.GetComponent<DamageTextDespawn>();

                dtd.OrigDespawnTime = despawnTime;
                o.transform.parent = MainCamera.gameObject.transform;
                mesh.text = message;
                motor.Shoot(PreferredEaseType, direction, despawnTime + 5.0f, 1.5f);
            });
        }

        public void DisplayGameMessageFlyAway(string message, Vector3 direction, Vector3 directionToFly)
        {
            float speed = 5.0f;
            float distance = 1.5f;

            PrefabSpawner.SpawnPrefab(TopMiddleOfScreen(), o =>
            {
                TextMesh mesh = o.GetComponent<TextMesh>();
                TextMotor motor = o.GetComponent<TextMotor>();

                o.transform.parent = MainCamera.gameObject.transform;
                mesh.text = message;
                motor.Shoot(PreferredEaseType, direction, speed, distance, 5.0f);
                //float time = (direction*distance/speed).magnitude;
                //motor.Shoot(PreferredEaseType, directionToFly, 30.0f, 20.0f, 2.0f);
            });
            
        }

        public IEnumerator DelayedMove(TextMotor motor, Vector3 direction, float whenToMove)
        {
            yield return new WaitForSeconds(whenToMove);
            motor.Shoot(PreferredEaseType, direction, 5.0f, 20.0f);
        }

        Vector3 MiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0.0f));
        }
        Vector3 TopMiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height * 3.0f / 5.0f, 0.0f));
        }
        Vector3 TopRightMiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width * 3.0f / 4.0f, Screen.height * 3.0f / 5.0f, 0.0f));
        }
        Vector3 TopLeftMiddleOfScreen()
        {
            return MainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 4.0f, Screen.height * 3.0f / 5.0f, 0.0f));
        }

        void Awake()
        {
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
            if (MainCamera == null)
            {
                MainCamera = Camera.main;
            }
            if (DeathScreen == null)
            {
                DeathScreen = GameObject.Find("DeathScreen");
            }
            if (KillCountGUI == null)
            {
                KillCountGUI = GameObject.Find("KillCountMeter");
            }
        }
    }
}