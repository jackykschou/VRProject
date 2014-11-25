using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Constants
{
    public enum InputKeyCode
    {
        VerticalAxis,
        HorizontalAxis,
        VerticalAxisJoystick,
        HorizontalAxisJoystick,
        Attack1,
        Attack2,
        Attack3,
        Attack4,
        Dash,
        MouseY,
        Submit,
        Cancel
    };

    public static class InputConstants
    {
        public const float DoubleClickBufferTime = 0.35f;

        private static readonly Dictionary<InputKeyCode, string> KeyCodeMapping = new Dictionary<InputKeyCode, string>()
        {
            {InputKeyCode.VerticalAxis, "VerticalAxis"},
            {InputKeyCode.HorizontalAxis, "HorizontalAxis"},
            {InputKeyCode.VerticalAxisJoystick, "VerticalAxisJoystick"},
            {InputKeyCode.HorizontalAxisJoystick, "HorizontalAxisJoystick"},
            {InputKeyCode.Attack1, "Attack1"},
            {InputKeyCode.Attack2, "Attack2"},
            {InputKeyCode.Attack3, "Attack3"},
            {InputKeyCode.Attack4, "Attack4"},
            {InputKeyCode.Dash, "Dash"},
            {InputKeyCode.MouseY, "Mouse Y"},
            {InputKeyCode.Submit, "Submit"},
            {InputKeyCode.Cancel, "Cancel"}
        };

        public static string GetKeyCodeName(InputKeyCode keyCode)
        {
            if (KeyCodeMapping.ContainsKey(keyCode))
            {
                return KeyCodeMapping[keyCode];
            }
            else  //no input matches
            {
                throw new KeyNotFoundException();
            }
        }
	
        public static Vector3 GetMousePostition3D()
        {
            return Input.mousePosition;
        }

        public static Vector2 GetMousePostition2D()
        {
            return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }
}
