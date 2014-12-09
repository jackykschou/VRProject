using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/GameView")]
    [RequireComponent(typeof(Renderer))]
    public abstract class GameView : GameScript
    {
        public Renderer Renderer;

        public FacingDirection FacingDirection { get; private set; }

        protected override void Initialize()
        {
            FacingDirection = FacingDirection.Down;
        }

        protected abstract override void Deinitialize();

        public abstract Vector2 CenterPosition { get; }

        public Vector2 ForwardDirection 
        {
            get { return UtilityFunctions.GetFacingDirectionVector(FacingDirection); } 
        }
        public Vector2 BackwardDirection 
        {
            get { return -UtilityFunctions.GetFacingDirectionVector(FacingDirection); }
        }
        public Vector2 LeftwardDirection 
        {
            get
            {
                Vector2 dir = UtilityFunctions.GetFacingDirectionVector(FacingDirection);
                return Quaternion.AngleAxis(-90f, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            }
        }

        public Vector2 RightwardDirection
        {
            get
            {
                Vector2 dir = UtilityFunctions.GetFacingDirectionVector(FacingDirection);
                return Quaternion.AngleAxis(90f, Vector3.forward) * new Vector3(dir.x, dir.y, 0);
            }
        }

        public abstract Vector2 ForwardEdge { get; }
        public abstract Vector2 BackwardEdge { get; }
        public abstract Vector2 LeftwardEdge { get; }
        public abstract Vector2 RightwardEdge { get; }

        [Attributes.GameScriptEvent(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            FacingDirection = facingDirection;
        }

        protected void UpdateSortingOrder()
        {
            Renderer.sortingOrder = (int)(transform.position.y * WorldScaleConstant.LayerSortingScale);
        }
    }
}
