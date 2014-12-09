using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/StaticSpriteGameView")]
    [RequireComponent(typeof(SpriteRenderer))]
    public class StaticSpriteGameView : GameView
    {
        protected SpriteRenderer Render;
        private Color _originalSpriteColor;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Render = GetComponent<SpriteRenderer>();
            if (Render.sortingLayerName == SortingLayerConstants.SortingLayerNames.Default)
            {
                Render.sortingLayerName = SortingLayerConstants.SortingLayerNames.CharacterLayer;
            }
            _originalSpriteColor = Render.color;
        }

        protected override void Initialize()
        {
            base.Initialize();
            renderer.enabled = true;
            UpdateSortingOrder();
            Render.color = _originalSpriteColor;
        }

        protected override void Deinitialize()
        {
        }

        public override Vector2 CenterPosition
        {
            get { return Render.bounds.center; }
        }

        public override Vector2 ForwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return Render.bounds.center + new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                    case FacingDirection.Down:
                        return Render.bounds.center - new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                    case FacingDirection.Left:
                        return Render.bounds.center - new Vector3(Mathf.Abs(Render.bounds.extents.x), 0, 0);
                    default:
                        return Render.bounds.center + new Vector3(Mathf.Abs(Render.bounds.extents.x), 0, 0);
                }
            }
        }

        public override Vector2 BackwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return Render.bounds.center - new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                    case FacingDirection.Down:
                        return Render.bounds.center + new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                    case FacingDirection.Left:
                        return Render.bounds.center + new Vector3(Mathf.Abs(Render.bounds.extents.x), 0, 0);
                    default:
                        return Render.bounds.center - new Vector3(Mathf.Abs(Render.bounds.extents.x), 0, 0);
                }
            }
        }

        public override Vector2 LeftwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return Render.bounds.center - new Vector3(Mathf.Abs(Render.bounds.extents.x), 0, 0);
                    case FacingDirection.Down:
                        return Render.bounds.center + new Vector3(Mathf.Abs(Render.bounds.extents.y), 0, 0);
                    case FacingDirection.Left:
                        return Render.bounds.center - new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                    default:
                        return Render.bounds.center + new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                }
            }
        }

        public override Vector2 RightwardEdge
        {
            get
            {
                switch (FacingDirection)
                {
                    case FacingDirection.Up:
                        return Render.bounds.center + new Vector3(Mathf.Abs(Render.bounds.extents.x), 0, 0);
                    case FacingDirection.Down:
                        return Render.bounds.center - new Vector3(Mathf.Abs(Render.bounds.extents.y), 0, 0);
                    case FacingDirection.Left:
                        return Render.bounds.center + new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                    default:
                        return Render.bounds.center - new Vector3(0, Mathf.Abs(Render.bounds.extents.y), 0);
                }
            }
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectDisabled)]
        public void DisableRender()
        {
            renderer.enabled = false;
        }

        [Attributes.GameScriptEvent(GameScriptEvent.SpawnPrefabOnSpriteGameViewOnRandomPosition)]
        public void SpawnPrefabOnSpriteGameView(Prefab prefab)
        {
            PrefabManager.Instance.SpawnPrefab(prefab, new Vector2(Render.bounds.center.x + Random.Range(-Render.bounds.extents.x * 0.5f, Render.bounds.extents.x * 0.5f), 
                Render.bounds.center.y + Random.Range(-Render.bounds.extents.y * 0.5f, Render.bounds.extents.y * 0.5f)));
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ChangeSpriteViewColor)]
        public void ChangeSpriteViewColor(Color color)
        {
            Render.color = color;
        }

        [Attributes.GameScriptEvent(GameScriptEvent.ChangeSpriteViewToOriginalColor)]
        public void ChangeSpriteViewToOriginalColor()
        {
            Render.color = _originalSpriteColor;
        }
    }
}
