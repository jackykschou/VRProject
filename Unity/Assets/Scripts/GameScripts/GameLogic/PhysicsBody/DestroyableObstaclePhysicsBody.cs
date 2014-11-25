using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Obstacle/DestroyableObstaclePhysicsBody")]
    public class DestroyableObstaclePhysicsBody : ObstaclePhysicsBody
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.DestroyableObstacle);
        }
    }
}
