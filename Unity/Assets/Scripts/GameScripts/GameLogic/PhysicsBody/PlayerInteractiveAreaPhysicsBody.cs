using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Projectile/PlayerInteractiveAreaPhysicsBody")]
    public class PlayerInteractiveAreaPhysicsBody : PhysicsBody2D
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.PlayerInteractiveArea);
            Collider.isTrigger = true;
            Rigidbody.isKinematic = true;
        }
    }
}
