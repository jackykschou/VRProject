using System;
using System.Security.Cryptography;
using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.ObjectMotor.Projectile
{
    public abstract class ProjectileMotor : ObjectMotor2D
    {
        private bool _arrived = false;

        public Transform Target;
        public Vector2 Direction;
        public Vector2 Destination;

        private Action _onDestinationArrivalAction;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateProjectileDirection)]
        public void UpdateDirection(Vector2 direction)
        {
            Direction = direction;
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateProjectileTarget)]
        public void UpdateTarget(Transform target)
        {
            Target = target;
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateProjectileDestination)]
        public void UpdateTarget(Vector2 destination)
        {
            Destination = destination;
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateOnProjectileArrivalAction)]
        public void UpdateOnProjectileArrivalAction(Action onArrival)
        {
            _onDestinationArrivalAction = onArrival;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _arrived = false;
        }

        protected override void Update()
        {
            base.Update();
            if (!_arrived && (Vector2.Distance(transform.position, Destination) <= 0.1f))
            {
                _arrived = true;
                if (_onDestinationArrivalAction != null)
                {
                    _onDestinationArrivalAction();
                }
                TriggerGameScriptEvent(Constants.GameScriptEvent.OnProjectileArriveDestination, Destination);
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.ShootProjectile)]
        public abstract void Shoot();
    }
}
