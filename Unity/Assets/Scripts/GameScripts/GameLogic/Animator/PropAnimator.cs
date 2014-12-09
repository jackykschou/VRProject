using Assets.Scripts.Constants;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("ObjectAnimator/PropAnimator")]
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class PropAnimator : ObjectAnimator
    {
        protected override void Deinitialize()
        {
        }

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void PlayDeathAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.GateDeactivated)]
        public void PlayDeactivateAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.GateActivated)]
        public void PlayActivateAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Idle);
        }
    }
}
