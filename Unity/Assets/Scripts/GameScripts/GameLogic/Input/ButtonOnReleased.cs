using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [AddComponentMenu("Input/ButtonOnReleased")]
    public class ButtonOnReleased : PlayerInput
    {
        public override bool Detect()
        {
            if (base.Detect() && IsKeyReleased())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }

            return false;
        }

        private bool IsKeyReleased()
        {
            return KeyCodes.Any(c => UnityEngine.Input.GetButtonUp(InputConstants.GetKeyCodeName(c)));
        }
    }
}
