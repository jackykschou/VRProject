using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Skills;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [RequireComponent(typeof(Skill))]
    [AddComponentMenu("PowerUp/SkillPowerUp")]
    public class SkillPowerUp : PowerUp 
    {
        protected override void Deinitialize()
        {
        }

        protected override void Apply()
        {
            if (AppliedCounter == 1)
            {
                Owner.TriggerGameScriptEvent(GameScriptEvent.UpdateSkills);
            }
        }

        protected override void UnApply()
        {
            Owner.TriggerGameScriptEvent(GameScriptEvent.RemoveSkill, GetComponent<Skill>());
        }
    }
}
