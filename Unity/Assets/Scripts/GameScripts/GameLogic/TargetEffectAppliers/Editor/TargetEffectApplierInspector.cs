using System.Collections.Generic;
using Assets.Scripts.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(TargetEffectApplier))]
    public class TargetEffectApplierInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            TargetEffectApplier applier = (TargetEffectApplier)target;

            applier.LabelName = EditorGUILayout.TextField("Label Name", applier.LabelName);

            applier.HasApplyCoolDown = EditorGUILayout.Toggle("Has Apply Cool Down", applier.HasApplyCoolDown);
            if (applier.HasApplyCoolDown)
            {
                applier.ApplyCooldown = EditorGUILayout.FloatField("Apply Cooldown", applier.ApplyCooldown);
                applier.ApplyCooldown = Mathf.Clamp(applier.ApplyCooldown, 0f, float.MaxValue);
            }

            if (applier.TargetTags == null)
            {
                applier.TargetTags = new List<string>();
            }

            applier.TargetTags.Resize(EditorGUILayout.IntField("Target tag list size", applier.TargetTags.Count));

            for (int i = 0; i < applier.TargetTags.Count; ++i)
            {
                applier.TargetTags[i] = EditorGUILayout.TagField(applier.TargetTags[i]);
            }

            if (applier.TargetPhysicalLayers == null)
            {
                applier.TargetPhysicalLayers = new List<int>();
            }

            applier.TargetPhysicalLayers.Resize(EditorGUILayout.IntField("Target physical layer list size", applier.TargetPhysicalLayers.Count));

            for (int i = 0; i < applier.TargetPhysicalLayers.Count; ++i)
            {
                applier.TargetPhysicalLayers[i] = EditorGUILayout.LayerField(applier.TargetPhysicalLayers[i]);
            }

            applier.OneTimeOnlyPerTarget = EditorGUILayout.Toggle("OneTimeOnlyPerTarget", applier.OneTimeOnlyPerTarget);
        }
    }
}
