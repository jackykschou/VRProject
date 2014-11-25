using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner.Editor
{
    [CustomEditor(typeof(PrefabSpawner))]
    public class PrefabSpawnerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            PrefabSpawner spawner = (PrefabSpawner)target;

            spawner.LabelName = EditorGUILayout.TextField("Label Name", spawner.LabelName);

            if (spawner.Prefabs == null)
            {
                spawner.Prefabs = new List<Prefab>();
            }

            spawner.Prefabs.Resize(EditorGUILayout.IntField("Prefabs Size", spawner.Prefabs.Count));

            for (int i = 0; i < spawner.Prefabs.Count; ++i)
            {
                spawner.Prefabs[i] = (Prefab)EditorGUILayout.EnumPopup("Prefab " + i, spawner.Prefabs[i]);
            }

            if (spawner.Prefabs == null)
            {
                spawner.Prefabs = new List<Prefab>();
            }

            if (spawner.Prefabs.Count != spawner.SpawnPickWeights.Count)
            {
                spawner.SpawnPickWeights.Resize(spawner.Prefabs.Count);
            }

            if (spawner.Prefabs.Count != spawner.PrefabSpawnValues.Count)
            {
                spawner.PrefabSpawnValues.Resize(spawner.Prefabs.Count);
            }

            for (int i = 0; i < spawner.SpawnPickWeights.Count; ++i)
            {
                spawner.SpawnPickWeights[i] = EditorGUILayout.FloatField("Weight " + i, spawner.SpawnPickWeights[i]);
            }

            spawner.UseLimitSpawnValue = EditorGUILayout.Toggle("Use Limit Spawn Value", spawner.UseLimitSpawnValue);

            if (spawner.UseLimitSpawnValue)
            {
                spawner.LimitSpawnValue = EditorGUILayout.IntField("Limit Spawn Value", spawner.LimitSpawnValue);

                for (int i = 0; i < spawner.PrefabSpawnValues.Count; ++i)
                {
                    spawner.PrefabSpawnValues[i] = EditorGUILayout.IntField("Prefab spawn value " + i, spawner.PrefabSpawnValues[i]);
                }
            }

            spawner.LimitNumberOfSpawn = EditorGUILayout.Toggle("Limit Number Of Spawn", spawner.LimitNumberOfSpawn);

            if (spawner.LimitNumberOfSpawn)
            {
                spawner.NumberOfSpawn = EditorGUILayout.IntField("Number Of Spawn", spawner.NumberOfSpawn);
            }

            spawner.SpawnChance = EditorGUILayout.FloatField("Spawn Chance", spawner.SpawnChance);
        }
    }
}
