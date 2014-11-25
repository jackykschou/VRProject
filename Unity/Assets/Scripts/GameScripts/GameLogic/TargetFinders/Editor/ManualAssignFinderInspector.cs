using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(ManualAssignFinder))]
    public class ManualAssignFinderInspector : TargetFinderInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ManualAssignFinder manualAssignFinder = (ManualAssignFinder)target;

#pragma warning disable 618
            manualAssignFinder.Target = EditorGUILayout.ObjectField("Target", manualAssignFinder.Target, typeof(GameObject)) as GameObject;
#pragma warning restore 618
        }
    }
}
