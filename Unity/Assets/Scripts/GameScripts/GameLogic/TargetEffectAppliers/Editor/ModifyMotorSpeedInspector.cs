using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(ModifyMotorSpeed))]
    public class ModifyMotorSpeedInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ModifyMotorSpeed modifyMotorSpeed = (ModifyMotorSpeed)target;

#pragma warning disable 618
            modifyMotorSpeed.SpeedChanger = EditorGUILayout.ObjectField("GameValueChanger", modifyMotorSpeed.SpeedChanger, typeof(GameValueChanger)) as GameValueChanger;
#pragma warning restore 618

        }
    }
}
