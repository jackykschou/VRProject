using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Constants;
using UnityEngine;

using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;


namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public abstract class ObjectAnimator : GameLogic
    {
        private Dictionary<string, float> _animationBoolParametesrAutoResetBufferMap;

        private const float BoolResetBufferFrameTime = 0.1f;

        public UnityEngine.Animator Animator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Animator = GetComponent<UnityEngine.Animator>();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _animationBoolParametesrAutoResetBufferMap = new Dictionary<string, float>();
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Idle);
        }

        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.SetAnimatorBoolState)]
        public void SetAnimatorBoolState(string state)
        {
            Animator.SetBool(state, true);
            if (_animationBoolParametesrAutoResetBufferMap.ContainsKey(state))
            {
                _animationBoolParametesrAutoResetBufferMap[state] = BoolResetBufferFrameTime;
            }
            else
            {
                _animationBoolParametesrAutoResetBufferMap.Add(state, BoolResetBufferFrameTime);
            }
        }

        [GameScriptEventAttribute(GameScriptEvent.SetAnimatorFloatState)]
        public void SetAnimatorIntState(string state, float value)
        {
            Animator.SetFloat(state, value);
        }

        protected override void Update()
        {
            base.Update();
            List<string> keys = _animationBoolParametesrAutoResetBufferMap.Keys.ToList();
            foreach (var k in keys)
            {
                if (_animationBoolParametesrAutoResetBufferMap[k] <= 0)
                {
                    _animationBoolParametesrAutoResetBufferMap.Remove(k);
                    Animator.SetBool(k, false);
                }
                else
                {
                    _animationBoolParametesrAutoResetBufferMap[k] -= Time.deltaTime;
                }
            }
        }
    }
}
