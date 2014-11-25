using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/MoveToLocationProjectileMotor")]
    public class MoveToLocationProjectileMotor : ProjectileMotor
    {
        public EaseType EaseType;

        public override void Shoot()
        {
            TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection,
                UtilityFunctions.GetDirection(transform.position, Destination).GetFacingDirection());
            MoveToWithStyle(EaseType, Destination, Speed);
        }
    }
}
