using System;

namespace Assets.Scripts.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class GameEventAttribute : Attribute
    {
        public Constants.GameEvent Event{get; private set;}

        public GameEventAttribute(Constants.GameEvent gameEvent)
        {
            Event = gameEvent;
        }
    }
}
