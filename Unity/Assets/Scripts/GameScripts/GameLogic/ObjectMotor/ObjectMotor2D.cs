using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor
{
    [RequireComponent(typeof(GameValue.GameValue))]
    public abstract class ObjectMotor2D : GameLogic
    {
        public GameValue.GameValue Speed;

        private float _velocityX;
        private float _velocityY;

        protected override void Initialize()
        {
            base.Initialize();
            _velocityY = 0f;
            _velocityY = 0f;
        }

        protected override void Deinitialize()
        {
        }

        public void TranslateLinearTowards(Vector2 direction, float speed)
        {
            direction = direction.normalized;
            gameObject.transform.Translate(new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        public void TranslateLinearTowards(GameObject target, float speed)
        {
            TranslateLinearTowards(gameObject.GetDirection(target), speed);
        }

        public void TranslateSmoothTowards(Vector2 direction, float speed)
        {
            direction = direction.normalized;
            const float smoothDampSmoothness = 5.0f;

            Vector3 destination = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime;

            float posX = Mathf.SmoothDamp(transform.position.x, destination.x, ref _velocityX, Time.fixedDeltaTime * smoothDampSmoothness);
            float posY = Mathf.SmoothDamp(transform.position.y, destination.y, ref _velocityY, Time.fixedDeltaTime * smoothDampSmoothness);

            transform.position = new Vector3(posX, posY, transform.position.z);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        public void TranslateSmoothTowards(GameObject target, float speed)
        {
            TranslateSmoothTowards(gameObject.GetDirection(target), speed);
        }

        public void InstantMoveTo(Vector2 position)
        {
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        public void MoveAlongWithStyle(EaseType style, Vector2 direction, float speed, float moveAlongDistance)
        {
            direction = direction.normalized;

            Vector2 destination = new Vector2(transform.position.x, transform.position.y) + direction * moveAlongDistance;
            float moveDuration = moveAlongDistance / speed;
            gameObject.MoveTo(new Vector3(destination.x, destination.y, 0), moveDuration, 0, style);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        public void MoveToWithStyle(EaseType style, Vector2 position, float speed)
        {
            float moveDuration = Vector3.Distance(new Vector3(position.x, position.y, 0), transform.position) / speed;
            gameObject.MoveTo(new Vector3(position.x, position.y, 0), moveDuration, 0, style);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        public void MoveAddWithStyle(EaseType style, Vector2 amount, float speed)
        {
            float moveDuration = amount.magnitude / speed;
            gameObject.MoveAdd(new Vector3(amount.x, amount.y, 0), moveDuration, 0, style);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        public void MoveByWithStyle(EaseType style, Vector2 amount, float speed, float delay = 0.0f)
        {
            float moveDuration = amount.magnitude / speed;
            gameObject.MoveBy(amount, moveDuration, delay, style);
            TriggerGameScriptEvent(GameScriptEvent.OnObjectMove);
        }

        [GameScriptEventAttribute(GameScriptEvent.ChangeObjectMotorSpeed)]
        public void ChangeSpeed(GameValueChanger speedChanger)
        {
            Speed.ChangeGameValue(speedChanger);
        }

        [GameScriptEventAttribute(GameScriptEvent.UnchangeObjectMotorSpeed)]
        public void UnchangeSpeed(GameValueChanger speedChanger)
        {
            Speed.UnchangeGameValue(speedChanger);
        }
    }
}
