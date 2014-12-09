using System.Collections;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [AddComponentMenu("2DObjectMotor/CharacterMotor")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : ObjectMotor2D
    {
        private const float DecelerationRate = 0.95f;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            rigidbody2D.isKinematic = false;
            rigidbody2D.fixedAngle = true;
        }

        [GameScriptEvent(GameScriptEvent.CharacterRigidMove)]
        public void MoveCharacterRigid(Vector2 direction)
        {
            direction = direction.normalized;
            FacingDirection newDirection = direction.GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterMove, direction);
            RigidMove(direction, Speed);
        }

        [GameScriptEvent(GameScriptEvent.CharacterNonRigidMove)]
        public void MoveCharacterNonRigid(Vector2 direction)
        {
            direction = direction.normalized;
            FacingDirection newDirection = direction.GetFacingDirection();
            if (newDirection != GameView.FacingDirection)
            {
                TriggerGameScriptEvent(GameScriptEvent.UpdateFacingDirection, newDirection);
            }
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterMove, direction);
            transform.Translate(direction * Speed * Time.fixedDeltaTime);
        }

        public void RigidMove(Vector2 direction, float speed)
        {
            direction = direction.normalized;
            rigidbody2D.AddForce(direction * speed * WorldScaleConstant.SpeedScale * Time.fixedDeltaTime);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        [GameScriptEvent(GameScriptEvent.OnCharacterKnockBacked)]
        [GameScriptEvent(GameScriptEvent.PushCharacter)]
        public void OneTimePush(Vector2 direction, float speed, float time)
        {
            direction = direction.normalized;
            StartCoroutine(OneTimePushIE(direction, speed, time));
        }

        IEnumerator OneTimePushIE(Vector2 direction, float speed, float time)
        {
            float timer = time;
            while (timer > 0f)
            {
                RigidMove(direction, speed);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
                speed *= timer/time;
                timer -= Time.fixedDeltaTime;
            }
        }

        public void RigidMove(GameObject target, float speed)
        {
            RigidMove(gameObject.GetDirection(target), speed);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            rigidbody2D.velocity *= DecelerationRate * Time.fixedDeltaTime;
        }
    }
}
