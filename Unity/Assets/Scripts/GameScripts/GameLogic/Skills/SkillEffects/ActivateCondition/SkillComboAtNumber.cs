using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Attributes;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

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

        [GameScriptEvent(GameScriptEvent.OnSkillComboChanged)]
        public void OnSkillComboChanged(int number)
        {
            _skillCurrentComboNumber = number;
        }
    }
}
