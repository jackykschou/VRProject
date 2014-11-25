using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(SetInvincibility))]
    public class SetInvincibilityInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SetInvincibility setInvincibility = (SetInvincibility)target;

            setInvincibility.Enable = EditorGUILayout.Toggle("Enable", setInvincibility.Enable);
        }
    }
}
