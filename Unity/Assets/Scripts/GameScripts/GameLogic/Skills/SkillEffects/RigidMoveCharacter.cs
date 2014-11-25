using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/CharacterRigidMove")]
    public class RigidMoveCharacter : SkillEffect
    {
        private Vector2 _direction;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.CharacterRigidMove, _direction);
            Activated = false;
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateMoveDirection)]
        public void UpdateMoveDirection(Vector2 moveDirection)
        {
            _direction = moveDirection;
        }
    }
}
