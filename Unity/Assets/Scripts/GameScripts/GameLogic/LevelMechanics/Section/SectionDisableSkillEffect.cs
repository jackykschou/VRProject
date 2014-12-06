using UnityEngine;
using System.Collections.Generic;

using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionDisableSkillEfect")]
    public class SectionDisableSkillEffect : SectionLogic
    {
        public List<int> DisabledSkills;
        public bool ResetOnDeactivate = true;

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                for(int i = 0; i <= 5; i++)
                    TriggerGameEvent(GameEvent.EnableAbility, i);
                foreach (int skillId in DisabledSkills)
                    TriggerGameEvent(GameEvent.DisableAbility, skillId);
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId)
                if(ResetOnDeactivate)
                    foreach (int skillId in DisabledSkills)
                        TriggerGameEvent(GameEvent.EnableAbility, skillId);
        }

    }
}
