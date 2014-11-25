using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(SpawnProjectileTowardsTarget))]
    public class SpawnProjectileTowardsTargetInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpawnProjectileTowardsTarget spawnProjectileTowardsTarget = (SpawnProjectileTowardsTarget)target;

            spawnProjectileTowardsTarget.Prefab = (Prefab)EditorGUILayout.EnumPopup("Prefab", spawnProjectileTowardsTarget.Prefab);

#pragma warning disable 618
            spawnProjectileTowardsTarget.PositionIndicator = EditorGUILayout.ObjectField("Position Indicator", spawnProjectileTowardsTarget.PositionIndicator, typeof(PositionIndicator)) as PositionIndicator;
#pragma warning restore 618
        }
    }
}
