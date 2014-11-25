using Assets.Scripts.Constants;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.PhysicsBody
{
    [AddComponentMenu("PhysicsBody/EnemyCharacterPhysicsBody")]
    public class EnemyCharacterPhysicsBody : CharacterPhysicsBody
    {
        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.Enemy);
        }
    }
}
