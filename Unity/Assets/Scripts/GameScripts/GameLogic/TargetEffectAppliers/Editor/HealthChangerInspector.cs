using Assets.Scripts.GameScripts.GameLogic.GameValue;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(HealthChanger))]
    public class HealthChangerInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HealthChanger healthChanger = (HealthChanger)target;

#pragma warning disable 618
            healthChanger.GameValueChanger = EditorGUILayout.ObjectField("GameValueChanger", healthChanger.GameValueChanger, typeof(GameValueChanger)) as GameValueChanger;
#pragma warning restore 618
        }
    }
}