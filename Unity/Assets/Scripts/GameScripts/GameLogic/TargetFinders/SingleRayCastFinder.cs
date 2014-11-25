using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/SingleRayCastFinder")]
    [RequireComponent(typeof(PositionIndicator))]
    public class SingleRayCastFinder : TargetFinder
    {
        public float Range;
        public float RayAngleRandomness;
        public PositionIndicator PositionIndicator;

        public Prefab ProjectilePrefab;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PositionIndicator == null)
            {
                PositionIndicator = GetComponent<PositionIndicator>();
            }
        }

        protected override void Deinitialize()
        {
        }

        protected override void FindTargets()
        {
            ClearTargets();
            Vector2 castDirecation = Quaternion.AngleAxis(Random.Range(-RayAngleRandomness, RayAngleRandomness), Vector3.forward) * PositionIndicator.Direction;
            string[] layers = TargetPhysicalLayers.Select(l => LayerMask.LayerToName(l)).ToArray();
            int mask = LayerMask.GetMask(layers);
            RaycastHit2D raycast = Physics2D.Raycast(PositionIndicator.Position.position, castDirecation, Range, mask);
            PrefabManager.Instance.SpawnPrefabImmediate(ProjectilePrefab, PositionIndicator.Position.position, o =>
            {
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDirection, castDirecation);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDestination, raycast.collider != null ?
                    (Vector2)(PositionIndicator.Position.position + (Vector3)(castDirecation * Vector2.Distance(raycast.collider.transform.position, PositionIndicator.Position.position))) :
                    (Vector2)(PositionIndicator.Position.position + (new Vector3(castDirecation.x, castDirecation.y, 0) * 100f)));
                o.TriggerGameScriptEvent(GameScriptEvent.ShootProjectile);
            });
            if (raycast.collider != null)
            {
                AddTarget(raycast.collider.gameObject);
            }
        }
    }
}
