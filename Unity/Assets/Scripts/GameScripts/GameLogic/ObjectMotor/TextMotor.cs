using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [AddComponentMenu("2DObjectMotor/TextMotor")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class TextMotor : ObjectMotor2D
    {
        public EaseType EaseType;

        public void Shoot(Vector3 dir, float speed, float distance)
        {
            MoveByWithStyle(EaseType, dir * distance, speed);
            rigidbody2D.gravityScale = .1f;
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = true;
        }

        public void Shoot(EaseType type, Vector3 dir, float speed, float distance, float delay = 0.0f)
        {
            MoveByWithStyle(type, dir * distance, speed, delay);
            rigidbody2D.gravityScale = 0.0f;
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = true;
        }
    }
}
