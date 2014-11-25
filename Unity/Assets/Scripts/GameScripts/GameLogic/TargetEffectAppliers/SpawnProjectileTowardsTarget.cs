using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/SpawnProjectileTowardsTarget")]
    [RequireComponent(typeof(PositionIndicator))]
    public class SpawnProjectileTowardsTarget : TargetEffectApplier
    {
        public Prefab Prefab;
        public PositionIndicator PositionIndicator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            PositionIndicator = GetComponent<PositionIndicator>();
        }

        protected override void ApplyEffect(GameObject target)
        {
            PrefabManager.Instance.SpawnPrefabImmediate(Prefab, PositionIndicator.Position.position, o =>
            {
                Vector2 castDirecation = UtilityFunctions.GetDirection(GameView.CenterPosition, target.transform.position);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDirection, castDirecation);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileTarget, target.transform);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDestination, (Vector2)target.transform.position);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateGameValueChangerOwner, gameObject);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateGameValueOwner, gameObject);
                o.TriggerGameScriptEvent(GameScriptEvent.ShootProjectile);
            });
        }
    }
}
