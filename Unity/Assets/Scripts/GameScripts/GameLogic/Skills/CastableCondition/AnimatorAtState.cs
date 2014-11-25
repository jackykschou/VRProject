using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/AnimatorAtState")]
    public class AnimatorAtState : SkillCastableCondition
    {
        public List<string> States;

        private List<int> _stateHashes;
        private UnityEngine.Animator _animator;

        protected override void Initialize()
        {
            base.Initialize();
            _stateHashes = new List<int>();
            foreach (var s in States)
            {
                _stateHashes.Add(UnityEngine.Animator.StringToHash(s));
            }
        }

        protected override void Deinitialize()
        {
        }

        public override bool CanCast()
        {
            if (_animator == null)
            {
                _animator = Skill.Caster.gameObject.GetComponent<UnityEngine.Animator>();
            }
            var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            return States.Any(stateInfo.IsName);
        }
    }
}
