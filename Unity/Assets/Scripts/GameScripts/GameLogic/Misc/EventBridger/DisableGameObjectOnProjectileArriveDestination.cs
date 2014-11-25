using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/DisableGameObjectOnProjectileArriveDestination")]
    public class DisableGameObjectOnProjectileArriveDestination : GameLogic
    {
        [Range(0f, 10f)]
        public float Delay = 0f;

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnProjectileArriveDestination)]
        public void OnProjectileArriveDestination(Vector2 destination)
        {
            DisableGameObject(Delay);
        }
    }
}
