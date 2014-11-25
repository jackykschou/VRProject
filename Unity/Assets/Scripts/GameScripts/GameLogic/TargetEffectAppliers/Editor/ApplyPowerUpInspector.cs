using Assets.Scripts.Constants;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(ApplyPowerUp))]
    public class ApplyPowerUpInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ApplyPowerUp applyPowerUp = (ApplyPowerUp) target;

            applyPowerUp.PowerUpPrefab = (Prefab)EditorGUILayout.EnumPopup("Prefab", applyPowerUp.PowerUpPrefab);
        }
    }
}
