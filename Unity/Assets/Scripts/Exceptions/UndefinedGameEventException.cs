using UnityEngine;

namespace Assets.Scripts.Exceptions
{
    public class UndefinedGameEventException : UnityException 
    {
        public UndefinedGameEventException()
        {
        }

        public UndefinedGameEventException(string message) : base(message)
        {
        }
    }
}
