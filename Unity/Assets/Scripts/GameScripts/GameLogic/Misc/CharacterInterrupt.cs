using System.Collections;
using Assets.Scripts.Attributes;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [RequireComponent(typeof(FixTimeDispatcher))]
    [AddComponentMenu("Misc/CharacterInterrupt")]
    public class CharacterInterrupt : GameLogic
    {
        public bool Interrupted { get; private set; }

        [Range(0f, float.MaxValue)]
        public float InterruptionDuration;

        public FixTimeDispatcher InterruptCoolDown;

        [GameScriptEvent(GameScriptEvent.InterruptCharacter)]
        public void InterruptCharacter()
        {
            if (!InterruptCoolDown.CanDispatch())
            {
                return;
            }
            InterruptCoolDown.Dispatch();
            TriggerGameScriptEvent(GameScriptEvent.OnCharacterInterrupted);
            StartCoroutine(CountDownInterruption());
        }

        IEnumerator CountDownInterruption()
        {
            Interrupted = true;
            yield return new WaitForSeconds(InterruptionDuration);
            Interrupted = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Interrupted = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
