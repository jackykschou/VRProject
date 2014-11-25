using System.Collections.Generic;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/RefreshSkillCoolDown")]
    public class RefreshSkillCoolDown : SkillEffect
    {
        public List<Skill> Skills;

        public override void Activate()
        {
            base.Activate();
            Skills.ForEach(s => s.TriggerGameScriptEvent(GameScriptEvent.RefreshSkillCoolDown));
            Activated = false;
        }
    }
}
