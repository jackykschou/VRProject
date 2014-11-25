using System;

namespace Assets.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class GameScriptEvent : Attribute
    {
        public Constants.GameScriptEvent Event { get; private set; }

        public GameScriptEvent(Constants.GameScriptEvent gameScriptEvent)
        {
            Event = gameScriptEvent;
        }
    }
}
