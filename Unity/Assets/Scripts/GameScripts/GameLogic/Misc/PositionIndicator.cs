using System;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/PositionIndicator")]
    public class PositionIndicator : GameLogic
    {
        public Transform Follower;
        public Transform Position;

        private Vector3 _downPos;
        private Vector3 _upPos;
        private Vector3 _leftPos;
        private Vector3 _rightPos;

        public Vector2 Direction 
        {
            get { return UtilityFunctions.GetFacingDirectionVector(_currentFacingDirection); }
        }

        private FacingDirection _currentFacingDirection;

        protected override void Initialize()
        {
            base.Initialize();
            if (Follower == null)
            {
                Follower = transform;
            }
            if (Position == null)
            {
                Position = transform;
            }

            _downPos = Position.localPosition;
            Position.RotateAround (Follower.position, Vector3.forward, 90);
            _rightPos = Position.localPosition;
            Position.RotateAround(Follower.position, Vector3.forward, 90);
            _upPos = Position.localPosition;
            Position.RotateAround(Follower.position, Vector3.forward, 90);
            _leftPos = Position.localPosition;
            Position.RotateAround(Follower.position, Vector3.forward, 90);

            Position.localPosition = _downPos;
            _currentFacingDirection = FacingDirection.Down;
        }

        protected override void Deinitialize()
        {
            Position.localPosition = _downPos;
            _currentFacingDirection = FacingDirection.Down;
        }

        [GameScriptEvent(GameScriptEvent.UpdateFacingDirection)]
        public void UpdatePosition(FacingDirection facingDirection)
        {
            _currentFacingDirection = facingDirection;
            switch (facingDirection)
            {
                case FacingDirection.Up:
                    Position.localPosition = _upPos;
                    break;
                case FacingDirection.Down:
                    Position.localPosition = _downPos;
                    break;
                case FacingDirection.Left:
                    Position.localPosition = _leftPos;
                    break;
                case FacingDirection.Right:
                    Position.localPosition = _rightPos;
                    break;
            }
        }
    }
}
