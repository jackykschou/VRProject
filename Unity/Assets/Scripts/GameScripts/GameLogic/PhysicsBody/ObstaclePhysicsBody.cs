namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    public abstract class ObstaclePhysicsBody : PhysicsBody2D
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Collider.isTrigger = false;
            Rigidbody.isKinematic = true;
        }
    }
}
