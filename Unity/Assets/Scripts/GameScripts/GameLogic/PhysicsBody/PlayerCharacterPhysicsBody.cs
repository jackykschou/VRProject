using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/Character/PlayerCharacterPhysicsBody")]
    public class PlayerCharacterPhysicsBody : CharacterPhysicsBody 
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.PlayerCharacter);
        }
    }
}
