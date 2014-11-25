using Assets.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition.Editor
{
    [CustomEditor(typeof( RayCastReachTarget))]
    public class RayCastReachTargetInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            RayCastReachTarget finder = (RayCastReachTarget)target;

            finder.RayCastPhysicalLayers.Resize(EditorGUILayout.IntField("Ray Cast Physical Layers Size", finder.RayCastPhysicalLayers.Count));

            for (int i = 0; i < finder.RayCastPhysicalLayers.Count; ++i)
            {
                finder.RayCastPhysicalLayers[i] = EditorGUILayout.LayerField(finder.RayCastPhysicalLayers[i]);
            }

            finder.Range = EditorGUILayout.FloatField("Range", finder.Range);
            finder.Range = Mathf.Clamp(finder.Range, 0f, float.MaxValue);
        }
    }
}
