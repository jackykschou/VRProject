using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Managers.Editor
{
    [CustomEditor(typeof(PrefabManager))]
    public class PrefabManagerInspector : UnityEditor.Editor
    {
        private bool _prefabPreloadAmountFolded;

        public override void OnInspectorGUI()
        {
            PrefabManager manager = (PrefabManager)target;

            DrawDefaultInspector();

            _prefabPreloadAmountFolded = EditorGUILayout.Foldout(_prefabPreloadAmountFolded, "Prefab preload amounts");

            if (_prefabPreloadAmountFolded)
            {
                if (manager.PrefabPreloadAmountsKeys != null)
                {
                    for (int i = 0; i < manager.PrefabPreloadAmountsKeys.Count; ++i)
                    {
                        EditorGUILayout.TextField(manager.PrefabPreloadAmountsKeys[i]);
                        manager.PrefabPreloadAmountsValues[i] = EditorGUILayout.IntField(manager.PrefabPreloadAmountsValues[i]);
                    }
                }
            }

            if (GUILayout.Button("Update"))
            {
                manager.UpdateManager();
            }
        }
    }
}
