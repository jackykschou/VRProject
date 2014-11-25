using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(UnmodifyMotorSpeed))]
    public class UnmodifyMotorSpeedInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            UnmodifyMotorSpeed unmodifyMotorSpeed = (UnmodifyMotorSpeed)target;

#pragma warning disable 618
            unmodifyMotorSpeed.SpeedChanger = EditorGUILayout.ObjectField("GameValueChanger", unmodifyMotorSpeed.SpeedChanger, typeof(GameValueChanger)) as GameValueChanger;
#pragma warning restore 618
        }
    }
}
