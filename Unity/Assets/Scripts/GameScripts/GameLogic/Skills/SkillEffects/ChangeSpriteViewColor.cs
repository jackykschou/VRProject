using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/ChangeSpriteViewColor")]
    public class ChangeSpriteViewColor : SkillEffect
    {
        public Color Color;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.TriggerGameScriptEvent(GameScriptEvent.ChangeSpriteViewColor, Color);
            Activated = false;
        }
    }
}
