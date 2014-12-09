using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/SpawnPrefabOnGameObjectDestroy")]
    public class SpawnPrefabOnGameObjectDestroy : GameLogic
    {
        public Prefab Prefab;

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectDisabled)]
        public void OnObjectDestroyed()
        {
            PrefabManager.Instance.SpawnPrefabImmediate(Prefab, transform.position);
        }

        protected override void Deinitialize()
        {
        }
    }
}
