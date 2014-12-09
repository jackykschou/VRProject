using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/TrailRenderView")]
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailRenderView : GameView
    {
        protected TrailRenderer Render;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Render = GetComponent<TrailRenderer>();
            Render.sortingLayerName = SortingLayerConstants.SortingLayerNames.ForegroundLayer;
        }

        protected override void Update()
        {
            base.Update();
            UpdateSortingOrder();
        }

        protected override void Deinitialize()
        {
        }

        public override Vector2 CenterPosition
        {
            get { return transform.position; }
        }

        public override Vector2 ForwardEdge
        {
            get { return transform.position; }
        }

        public override Vector2 BackwardEdge
        {
            get { return transform.position; }
        }

        public override Vector2 LeftwardEdge
        {
            get { return transform.position; }
        }

        public override Vector2 RightwardEdge
        {
            get { return transform.position; }
        }
    }
}
