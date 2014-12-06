using System;

namespace Assets.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class GameScriptEventAttribute : Attribute
    {
        public Constants.GameScriptEvent Event { get; private set; }

        public GameScriptEventAttribute(Constants.GameScriptEvent gameScriptEvent)
        {
            Event = gameScriptEvent;
        }
    }
}
