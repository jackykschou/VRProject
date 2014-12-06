using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraFollow : GameLogic
    {
        public Transform Target;

        [Range(0, float.MaxValue)]
        public float Damping = 5f;

        protected override void Deinitialize()
        {
        }

        protected override void Update()
        {
            base.Update();

            if (Target == null)
            {
                return;
            }


            float distance = Vector2.Distance(transform.position,Target.position);

            Vector3 wantedPosition = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * Damping * distance);
            transform.position = new Vector3(wantedPosition.x, wantedPosition.y, transform.position.z);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.CameraFollowTarget)]
        public void UpdateTarget(Transform target)
        {
            Target = target;
        }
    }
}
