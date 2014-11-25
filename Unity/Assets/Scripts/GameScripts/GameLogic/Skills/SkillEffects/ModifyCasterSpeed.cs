using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [RequireComponent(typeof(GameValueChanger))]
    [AddComponentMenu("Skill/SkillEffect/ModifyCasterSpeed")]
    public class ModifyCasterSpeed : SkillEffect
    {
        public GameValueChanger SpeedChanger;

        public override void Activate()
        {
            base.Activate();
            Skill.Caster.gameObject.TriggerGameScriptEvent(GameScriptEvent.ChangeObjectMotorSpeed, SpeedChanger);
            Activated = false;
        }
    }
}
