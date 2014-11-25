using System.Collections.Generic;
using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [AddComponentMenu("Misc/DisableGameObjectOnTriggerStay")]
    [RequireComponent(typeof(PhysicsBody2D))]
    public class DisableGameObjectOnTriggerStay : GameLogic
    {
        [Range(0f, 10f)]
        public float Delay = 0f;
        public List<int> TargetPhysicalLayers = new List<int>();

        private bool _gameObjectDisabled;

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnTriggerStay2D)]
        protected void OnPhysicsBodyOnTriggerStay2D(Collider2D coll)
        {
            if (TargetPhysicalLayers.Contains(coll.gameObject.layer))
            {
                DisableOnTriggerCollided();
            }
        }

        private void DisableOnTriggerCollided()
        {
            if (!_gameObjectDisabled)
            {
                _gameObjectDisabled = true;
                DisableGameObject(Delay);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _gameObjectDisabled = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
