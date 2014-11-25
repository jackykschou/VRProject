using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Health.Health))]
    public class HealthBarUpdate : GameLogic
    {
        Health.Health _health;

        protected override void Initialize()
        {
            base.Initialize();
            _health = gameObject.GetComponent<Health.Health>();
        }

        protected override void Update()
        {
            base.Update();
            TriggerGameEvent(GameEvent.PlayerHealthUpdate, (_health.HitPoint.Value / _health.HitPoint.Max));
        }

        protected override void Deinitialize()
        {
        }
    }
}