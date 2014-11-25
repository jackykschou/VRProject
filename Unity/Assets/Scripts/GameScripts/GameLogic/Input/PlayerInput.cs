using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Input
{
    [RequireComponent(typeof(FixTimeDispatcher))]
    public class PlayerInput : GameLogic
    {
        [SerializeField]
        protected List<InputKeyCode> KeyCodes;

        public FixTimeDispatcher CoolDownTimeDispatcher;

        public virtual bool Detect()
        {
            return !IsInCooldown();
        }

        protected override void Deinitialize()
        {
        }

        protected bool IsInCooldown()
        {
            return !CoolDownTimeDispatcher.CanDispatch();
        }
    }
}
