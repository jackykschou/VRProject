using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.AILogic
{
    [AddComponentMenu("AILogic/EnemyDetection")]
    public class EnemyDetection : GameLogic
    {
        [Range(0f, float.MaxValue)] 
        public float DetectionRadius;

        public GameObject FindClosestEnemy()
        {
            GameObject enemy = FindEnemies().FirstOrDefault();

            if (enemy != null)
            {
                TriggerGameScriptEvent(GameScriptEvent.OnNewTargetDiscovered, enemy);
            }

            return FindEnemies().FirstOrDefault();
        }

        public List<GameObject> FindEnemies()
        {
            var enemies = new SortedDictionary<float, GameObject>();
            foreach (var hit in Physics2D.CircleCastAll(GameView.CenterPosition, DetectionRadius, Vector2.zero, 0f, LayerConstants.LayerMask.Character))
            {
                if (TagConstants.IsEnemy(gameObject.tag, hit.collider.gameObject.tag))
                {
                    enemies.Add(Vector2.Distance(hit.collider.transform.position, transform.position), hit.collider.gameObject);
                }
            }
            return enemies.Values.ToList();
        }

        protected override void Deinitialize()
        {
        }
    }
}
