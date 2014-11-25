using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

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

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DisableCharacterCollider()
        {
            Collider.enabled = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.ResetHealth)]
        public void ResetHealth()
        {
            Collider.enabled = true;
        }
    }
}
