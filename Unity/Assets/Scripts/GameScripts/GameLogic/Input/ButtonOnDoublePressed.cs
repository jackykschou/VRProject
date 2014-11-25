using System.Linq;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [AddComponentMenu("Input/ButtonOnDoublePressed")]
    public class ButtonOnDoublePressed : PlayerInput
    {
        public FixTimeDispatcher ClickBufferTimeDispatcher;

        public override bool Detect()
        {
            if (base.Detect() && IsKeyDoubleClicked())
            {
                CoolDownTimeDispatcher.Dispatch();
                return true;
            }

            return false;
        }

        private bool IsKeyDoubleClicked()
        {
            if (IsKeyPressed())
            {
                if (HasClickedOnce())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private bool IsKeyPressed()
        {
            return KeyCodes.Any(c => UnityEngine.Input.GetButtonDown(InputConstants.GetKeyCodeName(c)));
        }

        private bool HasClickedOnce()
        {
            return !ClickBufferTimeDispatcher.Dispatch();
        }
    }
}
