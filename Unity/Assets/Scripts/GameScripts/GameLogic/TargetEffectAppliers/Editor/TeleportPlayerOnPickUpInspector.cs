using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(TeleportPlayer))]
    public class TeleportPlayerOnPickUpInspector : TargetEffectApplierInspector {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
