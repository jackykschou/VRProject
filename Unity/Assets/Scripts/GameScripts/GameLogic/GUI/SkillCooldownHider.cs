using System.Collections.Generic;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class SkillCooldownHider : GameLogic
    {
        public List<GameObject> SkillCooldownBox;
 
        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.EnableAbility)]
        public void ShowCooldownBox(int skillId)
        {
            if(skillId >= 1 && skillId < 5)
                SkillCooldownBox[skillId - 1].SetActive(true);
        }

        [GameEventAttribute(GameEvent.DisableAbility)]
        public void HideCooldownBox(int skillId)
        {
            if (skillId >= 1 && skillId < 5)
                SkillCooldownBox[skillId - 1].SetActive(false);
        }

    }
}