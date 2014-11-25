using System.Collections.Generic;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionSkillHighlight")]
    public class SectionSkillHighlight : SectionLogic
    {
        public List<int> SkillsToHighlight;
        public bool ResetOnDeactivate = true;
        private const float Duration = .6f;


        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.OnLevelEnded)]
        public void LevelEnded()
        {
            for (int i = 0; i <= 5; i++)
            {
                TriggerGameEvent(GameEvent.EnableAbility, i);
                TriggerGameEvent(GameEvent.DisableHighlightSkill, i);
            }
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                for(int i = 0; i <= 5; i++)
                    TriggerGameEvent(GameEvent.DisableHighlightSkill, i);
                foreach (int skillId in SkillsToHighlight)
                {
                    TriggerGameEvent(GameEvent.EnableAbility, skillId);
                    TriggerGameEvent(GameEvent.EnableHighlightSkill, skillId, Duration);
                }
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId)
            {
                if (ResetOnDeactivate)
                {
                    foreach (int skillId in SkillsToHighlight)
                    {
                        TriggerGameEvent(GameEvent.DisableHighlightSkill, skillId);
                    }
                }
            }
        }
    }
}
