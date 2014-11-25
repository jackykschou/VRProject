using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [AddComponentMenu("Input/AxisOnHold")]
    public class AxisOnHold : PlayerInput
    {
        private const float AxisValueThreshold = 0.1f;

        public float GetAxisValue()
        {
            float max = KeyCodes.Max(c => UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(c)));
            float min = KeyCodes.Min(c => UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(c)));
            return Mathf.Abs(max) > Mathf.Abs(min) ? max : min;
        }

        public override bool Detect()
        {
            if (base.Detect() && IsAxisOnHold())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }
            return false;
        }

        private bool IsAxisOnHold()
        {
            return KeyCodes.Any(c => Mathf.Abs(UnityEngine.Input.GetAxis(InputConstants.GetKeyCodeName(c))) > AxisValueThreshold);
        }
    }
}
