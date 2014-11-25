using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [AddComponentMenu("Input/ButtonOnHold")]
    public class ButtonOnHold : PlayerInput
    {
        protected bool IsHolding;

        public float LastHoldTime {
            get { return _lastReleaseTime - _lastHoldStartTime; }
        }
        private float _lastHoldStartTime;
        private float _lastReleaseTime;

        protected override void Initialize()
        {
            base.Initialize();
            IsHolding = false;
            _lastHoldStartTime = 0f;
            _lastReleaseTime = 0f;
        }

        protected override void Update()
        {
            base.Update();
            if (IsHolding && IsKeyReleased())
            {
                IsHolding = false;
                CoolDownTimeDispatcher.Dispatch();
                _lastReleaseTime = Time.fixedTime;
            }
        }

        public override bool Detect()
        {
            if ((base.Detect() || IsHolding) && IsKeyOnHold())
            {
                if (!IsHolding)
                {
                    _lastHoldStartTime = Time.fixedTime;
                }
                IsHolding = true;
                return true;
            }
            return false;
        }

        private bool IsKeyOnHold()
        {
            return KeyCodes.Any(c => UnityEngine.Input.GetButton(InputConstants.GetKeyCodeName(c)));
        }

        private bool IsKeyReleased()
        {
            return KeyCodes.Any(c => UnityEngine.Input.GetButtonUp(InputConstants.GetKeyCodeName(c)));
        }
    }
}
