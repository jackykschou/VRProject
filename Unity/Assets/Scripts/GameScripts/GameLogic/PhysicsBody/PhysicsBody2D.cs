using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicsBody2D : GameLogic
    {
        protected Collider2D Collider;
        protected Rigidbody2D Rigidbody;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Collider = GetComponent<Collider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0f;
            Rigidbody.fixedAngle = true;
        }

        protected override void Initialize()
        {
            Collider.enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectDestroyed)]
        public void DisableCollider()
        {
            Collider.enabled = false;
        }

        protected override void OnCollisionEnter2D(Collision2D coll)
        {
            if (Disabled || Deinitialized || !Initialized)
            {
                return;
            }
            TriggerGameScriptEvent(GameScriptEvent.OnPhysicsBodyOnTriggerEnter2D, coll);
        }

        protected override void OnCollisionStay2D(Collision2D coll)
        {
            if (Disabled || Deinitialized || !Initialized)
            {
                return;
            }
            TriggerGameScriptEvent(GameScriptEvent.OnPhysicsBodyOnCollisionStay2D, coll);
        }

        protected override void OnCollisionExit2D(Collision2D coll)
        {
            if (Disabled || Deinitialized || !Initialized)
            {
                return;
            }
            TriggerGameScriptEvent(GameScriptEvent.OnPhysicsBodyOnCollisionExit2D, coll);
        }

        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            if (Disabled || Deinitialized || !Initialized)
            {
                return;
            }
            TriggerGameScriptEvent(GameScriptEvent.OnPhysicsBodyOnTriggerEnter2D, coll);
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            if (Disabled || Deinitialized || !Initialized)
            {
                return;
            }
            TriggerGameScriptEvent(GameScriptEvent.OnPhysicsBodyOnTriggerStay2D, coll);
        }

        protected override void OnTriggerExit2D(Collider2D coll)
        {
            if (Disabled || Deinitialized || !Initialized)
            {
                return;
            }
            TriggerGameScriptEvent(GameScriptEvent.OnPhysicsBodyOnTriggerExit2D, coll);
        }
    }
}
