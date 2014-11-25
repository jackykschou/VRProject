using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(SingleRayCastFinder))]
    public class SingleRayCastFinderInspector : TargetFinderInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SingleRayCastFinder finder = (SingleRayCastFinder)target;

            finder.Range = EditorGUILayout.FloatField("Range", finder.Range);
            finder.Range = Mathf.Clamp(finder.Range, 0f, float.MaxValue);

            finder.RayAngleRandomness = EditorGUILayout.FloatField("Ray Angle Randomness", finder.RayAngleRandomness);
            finder.RayAngleRandomness = Mathf.Clamp(finder.RayAngleRandomness, -360f, 360f);

            finder.ProjectilePrefab = (Prefab) EditorGUILayout.EnumPopup("Projectile Prefab", finder.ProjectilePrefab);

#pragma warning disable 618
            finder.PositionIndicator = EditorGUILayout.ObjectField("Position Indicator", finder.PositionIndicator, typeof(PositionIndicator)) as PositionIndicator;
#pragma warning restore 618
        }
    }
}
