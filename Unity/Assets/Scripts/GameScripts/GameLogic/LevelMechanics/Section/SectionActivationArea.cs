using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionActivationArea")]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PlayerInteractiveAreaPhysicsBody))]
    public class SectionActivationArea : SectionLogic 
    {
        private bool _activated;
        private bool _canSpawn;

        protected override void Initialize()
        {
            base.Initialize();
            _activated = false;
        }

        protected override void Deinitialize()
        {
            _canSpawn = false;
        }

        [GameScriptEvent(GameScriptEvent.OnPhysicsBodyOnTriggerStay2D)]
        protected override void OnTriggerEnter2D(Collider2D coll)
        {
            if (_activated || !_canSpawn || !Initialized || Deinitialized)
            {
                return;
            }
            TriggerGameEvent(GameEvent.OnSectionActivated, SectionId);
            _activated = true;
        }

        [GameScriptEvent(GameScriptEvent.SurvivalAreaSpawned)]
        [GameEvent(GameEvent.OnLevelStarted)]
        public void AllowSpawn()
        {
            _canSpawn = true;
        }
    }
}
