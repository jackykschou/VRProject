using Assets.Scripts.Constants;
using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers.Editor
{
    [CustomEditor(typeof(SpawnPrefabOnSpriteGameViewRandomPosition))]
    public class SpawnPrefabOnSpriteGameViewRandomPositionInspector : TargetEffectApplierInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpawnPrefabOnSpriteGameViewRandomPosition spawnPrefabOnSpriteGameViewRandomPosition = (SpawnPrefabOnSpriteGameViewRandomPosition)target;

            spawnPrefabOnSpriteGameViewRandomPosition.Prefab = (Prefab)EditorGUILayout.EnumPopup("Prefab", spawnPrefabOnSpriteGameViewRandomPosition.Prefab);
        }
    }
}
