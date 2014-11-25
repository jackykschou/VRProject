using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(DirectionalKnockBack))]
    public class DirectionalKnockBackInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DirectionalKnockBack directionalKnockBack = (DirectionalKnockBack)target;
            directionalKnockBack.KnockBackSpeed = EditorGUILayout.FloatField("Speed", directionalKnockBack.KnockBackSpeed);
            directionalKnockBack.Time = EditorGUILayout.FloatField("Time", directionalKnockBack.Time);
            directionalKnockBack.Time = Mathf.Clamp(directionalKnockBack.Time, 0f, float.MaxValue);

#pragma warning disable 618
            directionalKnockBack.PositionIndicator = EditorGUILayout.ObjectField("Position Indicator", directionalKnockBack.PositionIndicator, typeof(PositionIndicator)) as PositionIndicator;
#pragma warning restore 618
        }
    }
}
