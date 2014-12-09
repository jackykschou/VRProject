using System.Collections.Generic;
using Assets.Scripts.Attributes;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class SkillCooldownHider : GameLogic
    {
        public List<GameObject> SkillCooldownBox;
 
        protected override void Deinitialize()
        {
        }

        [GameEvent(GameEvent.EnableAbility)]
        public void ShowCooldownBox(int skillId)
        {
            if(skillId >= 1 && skillId < 5)
                SkillCooldownBox[skillId - 1].SetActive(true);
        }

        [GameEvent(GameEvent.DisableAbility)]
        public void HideCooldownBox(int skillId)
        {
            if (skillId >= 1 && skillId < 5)
                SkillCooldownBox[skillId - 1].SetActive(false);
        }

    }
}