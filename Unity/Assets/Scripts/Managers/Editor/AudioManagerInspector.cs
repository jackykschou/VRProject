using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Managers.Editor
{
    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {

            // EditorGUI.indentLevel = 5;
            AudioManager manager = (AudioManager)target;

            if (GUILayout.Button("Update"))
                manager.UpdateManager();
            if (GUILayout.Button("Remove All"))
                manager.DeleteClips();

            DrawDefaultInspector();
        }
    }
}
