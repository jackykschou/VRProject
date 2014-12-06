using Assets.Scripts.Constants;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("ObjectAnimator/PropAnimator")]
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class PropAnimator : ObjectAnimator
    {
        protected override void Deinitialize()
        {
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void PlayDeathAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        [GameScriptEventAttribute(GameScriptEvent.GateDeactivated)]
        public void PlayDeactivateAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        [GameScriptEventAttribute(GameScriptEvent.GateActivated)]
        public void PlayActivateAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Idle);
        }
    }
}
