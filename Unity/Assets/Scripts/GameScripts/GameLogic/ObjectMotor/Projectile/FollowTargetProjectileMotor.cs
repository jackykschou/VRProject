using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    [AddComponentMenu("2DObjectMotor/ProjectileMotor/FollowTargetProjectileMotor")]
    public class FollowTargetProjectileMotor : ProjectileMotor
    {
        public float Acceleration;

        public override void Shoot()
        {
            StartCoroutine(FollowTarget());
        }

        protected override void Update()
        {
            base.Update();
            Destination = Target.transform.position;
        }

        public IEnumerator FollowTarget()
        {
            float followedTime = 0f;
            while (Target != null)
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                followedTime += Time.fixedDeltaTime;
                float speedUp = Acceleration * followedTime;
                TranslateLinearTowards(Target.gameObject, (Speed + speedUp) * Time.fixedDeltaTime);
            }
            if (Target == null)
            {
                DisableGameObject();
            }
        }
    }
}
