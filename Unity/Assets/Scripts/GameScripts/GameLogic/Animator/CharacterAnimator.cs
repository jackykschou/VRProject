using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Animator
{
    [AddComponentMenu("ObjectAnimator/CharacterAnimator")]
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class CharacterAnimator : ObjectAnimator
    {
        [GameScriptEvent(GameScriptEvent.UpdateFacingDirection)]
        public void UpdateFacingDirection(FacingDirection facingDirection)
        {
            Animator.SetFloat(AnimatorControllerConstants.AnimatorParameterName.FacingDirection, (int)facingDirection);
        }

        [GameScriptEvent(GameScriptEvent.OnCharacterMove)]
        public void OnObjectMove(Vector2 direction)
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Move);
        }

        [GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void PlayDeathAnimation()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Death);
        }

        [GameScriptEvent(GameScriptEvent.OnCharacterInterrupted)]
        public void InterruptCharacter()
        {
            SetAnimatorBoolState(AnimatorControllerConstants.AnimatorParameterName.Interrupt);
        }

        protected override void Deinitialize()
        {
        }

    }
}
