﻿using Assets.Scripts.Attributes;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    public abstract class CharacterPhysicsBody : PhysicsBody2D
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Rigidbody.isKinematic = false;
            Collider.isTrigger = false;
        }

        [GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DisableCharacterCollider()
        {
            Collider.enabled = false;
        }

        [GameScriptEvent(GameScriptEvent.ResetHealth)]
        public void ResetHealth()
        {
            Collider.enabled = true;
        }
    }
}
