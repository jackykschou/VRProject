using System.Linq;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders
{
    [AddComponentMenu("TargetFinder/CircleAreaFinder")]
    [RequireComponent(typeof(PositionIndicator))]
    public class CircleAreaFinder : TargetFinder 
    {
        public float Radius;
        public PositionIndicator PositionIndicator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PositionIndicator == null)
            {
                PositionIndicator = GetComponent<PositionIndicator>();
            }
        }

        protected override void FindTargets()
        {
            ClearTargets();
            string[] layers = TargetPhysicalLayers.Select(l => LayerMask.LayerToName(l)).ToArray();
            int mask = LayerMask.GetMask(layers);
            foreach (var col in Physics2D.OverlapCircleAll(PositionIndicator.Position.position, Radius, mask))
            {
                AddTarget(col.gameObject);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
