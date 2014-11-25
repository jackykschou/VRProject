using Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    public class UpdateSceneSpawnPointDispatcher : MonoBehaviour
    {
        public float X = 10f;
        void Update ()
        {
            var objs = FindObjectsOfType(typeof (GameObject)) as GameObject[];
            foreach (var o in objs)
            {
                Updataaaa(o);
            }
        }

        void Updataaaa(GameObject obj)
        {
            if (obj.name.Contains("EnemySpawnPoint"))
            {
                obj.GetComponent<SectionEnemySpawnPoint>().SpawnCoolDown = obj.GetComponent<FixTimeDispatcher>();
            }
            foreach (Transform c in obj.transform)
            {
                Updataaaa(c.gameObject);
            }
        }
    }
}
