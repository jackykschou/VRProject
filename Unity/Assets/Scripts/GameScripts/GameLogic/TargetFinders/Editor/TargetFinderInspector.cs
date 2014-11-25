using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers;
using Assets.Scripts.Utility;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(TargetFinder))]
    public class TargetFinderInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            TargetFinder finder = (TargetFinder)target;

            finder.LabelName = EditorGUILayout.TextField("Label Name", finder.LabelName);

            if (finder.TargetTags == null)
            {
                finder.TargetTags = new List<string>();
            }

            finder.TargetTags.Resize(EditorGUILayout.IntField("Target tag list size", finder.TargetTags.Count));

            for (int i = 0; i < finder.TargetTags.Count; ++i)
            {
                finder.TargetTags[i] = EditorGUILayout.TagField(finder.TargetTags[i]);
            }

            if (finder.TargetPhysicalLayers == null)
            {
                finder.TargetPhysicalLayers = new List<int>();
            }

            finder.TargetPhysicalLayers.Resize(EditorGUILayout.IntField("Target physical layer list size", finder.TargetPhysicalLayers.Count));

            for (int i = 0; i < finder.TargetPhysicalLayers.Count; ++i)
            {
                finder.TargetPhysicalLayers[i] = EditorGUILayout.LayerField(finder.TargetPhysicalLayers[i]);
            }

            if (finder.TargetEffectAppliers == null)
            {
                finder.TargetEffectAppliers = new List<TargetEffectApplier>();
            }

            finder.TargetEffectAppliers.Resize(EditorGUILayout.IntField("Target effect applier list size", finder.TargetEffectAppliers.Count));

#pragma warning disable 618
            for (int i = 0; i < finder.TargetEffectAppliers.Count; ++i)
            {
                finder.TargetEffectAppliers[i] = EditorGUILayout.ObjectField("Applier" + i, finder.TargetEffectAppliers[i], typeof(TargetEffectApplier)) as TargetEffectApplier;
            }
#pragma warning restore 618
        }
    }
}
