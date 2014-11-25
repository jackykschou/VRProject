using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(DisplayGeneralMessage))]
    public class DisplayGeneralMessageInspector : TargetEffectApplierInspector
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DisplayGeneralMessage displayGeneralMessage = (DisplayGeneralMessage)target;

            displayGeneralMessage.Message = EditorGUILayout.TextField("Message", displayGeneralMessage.Message);
        }
        
    }
}
