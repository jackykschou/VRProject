using Assets.Scripts.Constants;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("ObjectAnimator/CharacterAnimator")]
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class CharacterAnimator : ObjectAnimator
    {
        [GameScriptEventAttribute(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            Animator.SetFloat(AnimatorControllerConstants.AnimatorParameterName.FacingDirection, (int)facingDirection);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCharacterMove)]
        public void OnObjectMove(Vector2 direction)
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Move);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void PlayDeathAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCharacterInterrupted)]
        public void InterruptCharacter()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Interrupt);
        }

        protected override void Deinitialize()
        {
        }

    }
}
