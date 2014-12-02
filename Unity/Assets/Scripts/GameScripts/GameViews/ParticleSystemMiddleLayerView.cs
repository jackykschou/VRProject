using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/ParticleSystemMiddleLayerView")]
    [RequireComponent(typeof (ParticleSystem))]
    public class ParticleSystemMiddleLayerView : GameView
    {
        protected ParticleSystem _ParticleSystem;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _ParticleSystem = GetComponent<ParticleSystem>();
            _ParticleSystem.renderer.sortingLayerName = SortingLayerConstants.SortingLayerNames.MiddleLayer;
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

