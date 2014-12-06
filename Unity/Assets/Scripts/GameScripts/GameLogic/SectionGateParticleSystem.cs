using Assets.Scripts.Attributes;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic
{
    [RequireComponent(typeof(ParticleSystem))]
    public class SectionGateParticleSystem : GameLogic
    {
        private ParticleSystem _gateParticleSystem;


        [GameScriptEvent(GameScriptEvent.GateActivated)]
        public void PlayActivateParticleSystem()
        {
            _gateParticleSystem.Play();
        }

        [GameScriptEvent(GameScriptEvent.GateDeactivated)]
        public void PlayDeactivateParticleSystem()
        {
            _gateParticleSystem.Stop();
            _gateParticleSystem.Clear();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _gateParticleSystem = GetComponent<ParticleSystem>();
        }

        protected override void Deinitialize()
        {
        }
    }
}
