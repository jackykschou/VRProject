using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    [AddComponentMenu("Skill/SkillEffect/SkillEffectActivateCondition/AnimatorAtState")]
    public class AnimatorAtState : SkillEffectActivateCondition 
    {
        public List<string> States;

        private List<int> _stateHashes;
        private UnityEngine.Animator _animator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _stateHashes = new List<int>();
            foreach (var s in States)
            {
                _stateHashes.Add(UnityEngine.Animator.StringToHash(s));
            }
        }

        public override bool CanActivate()
        {
            if (_animator == null)
            {
                _animator = SkillEffect.Skill.Caster.gameObject.GetComponent<UnityEngine.Animator>();
            }
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return States.Any(stateInfo.IsName);
        }
    }
}
