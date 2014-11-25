using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [AddComponentMenu("Input/ButtonOnPressed")]
    public class ButtonOnPressed : PlayerInput
    {
        public override bool Detect()
        {
            if (base.Detect() && IsKeyPressed())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }

            return false;
        }

        private bool IsKeyPressed()
        {
            return KeyCodes.Any(c => UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(c)));
        }
    }
}
