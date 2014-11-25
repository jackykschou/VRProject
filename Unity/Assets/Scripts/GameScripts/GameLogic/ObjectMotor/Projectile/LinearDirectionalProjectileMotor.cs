using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/LinearDirectionalProjectileMotor")]
    public class LinearDirectionalProjectileMotor : ProjectileMotor
    {
        public EaseType EaseType;

        public override void Shoot()
        {
            TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, Direction.GetFacingDirection());
            MoveAlongWithStyle(EaseType, Direction, Speed * 1.2f, 100f);
        }
    }
}
