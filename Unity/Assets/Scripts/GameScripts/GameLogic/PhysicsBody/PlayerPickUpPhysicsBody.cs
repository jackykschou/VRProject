using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Projectile/PlayerPickUpPhysicsBody")]
    public class PlayerPickUpPhysicsBody : PhysicsBody2D 
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Collider.isTrigger = true;
            Rigidbody.isKinematic = false;
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.PlayerPickUp);
        }
    }
}
