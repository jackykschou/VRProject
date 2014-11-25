using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects
{
    [AddComponentMenu("Skill/SkillEffect/SetAnimatorBool")]
    public class SetAnimatorBool : SkillEffect 
    {
        public string BoolParameterName;

        public override void Activate()
        {
            base.Activate();
            SetBoolParameter();
            Activated = false;
        }

        public void SetBoolParameter()
        {
            TriggerCasterGameScriptEvent(GameScriptEvent.SetAnimatorBoolState, BoolParameterName);
        }
    }
}
