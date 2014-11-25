using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics
{
    [AddComponentMenu("LevelMechanics/TeleportPlayerOnEnter")]
    [RequireComponent(typeof(PlayerInteractiveAreaPhysicsBody))]
    public class TeleportPlayerOnEnter : GameLogic
    {
        public Transform TeleportDestination;

        [GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnTriggerStay2D)]
        public void TeleportPlayer(Collider2D coll)
        {
            GameManager.Instance.PlayerMainCharacter.transform.position = new Vector3(TeleportDestination.position.x, TeleportDestination.position.y, GameManager.Instance.PlayerMainCharacter.transform.position.z);
        }

        protected override void Deinitialize()
        {
        }
    }
}
