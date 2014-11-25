using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    [AddComponentMenu("Skill/SkillEffect/SkillEffectActivateCondition/SkillComboAtNumber")]
    public class SkillComboAtNumber : SkillEffectActivateCondition
    {
        public List<int> ComboNumbers;

        private int _skillCurrentComboNumber;

        public override bool CanActivate()
        {
            return (ComboNumbers == null) || ComboNumbers.Any(n => n == _skillCurrentComboNumber);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnSkillComboChanged)]
        public void OnSkillComboChanged(int number)
        {
            _skillCurrentComboNumber = number;
        }
    }
}
