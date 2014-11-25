using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/RefreshSkillsOnEnterSection")]
    public class RefreshSkillsOnEnterSection : SectionLogic 
    {
        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                GameManager.Instance.PlayerMainCharacter.TriggerGameScriptEvent(GameScriptEvent.RefreshSkillCoolDown);
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
