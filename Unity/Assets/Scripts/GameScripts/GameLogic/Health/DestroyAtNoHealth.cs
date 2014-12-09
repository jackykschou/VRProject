using Assets.Scripts.Attributes;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [AddComponentMenu("HealthLogic/DestroyAtNoHealth")]
    public class DestroyAtNoHealth : GameLogic
    {
        [Range(0f, float.MaxValue)]
        public float Delay = 1.0f;

        protected override void Deinitialize()
        {
        }

        [GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void DestroyGameObject()
        {
            DisableGameObject(Delay);
        }
    }
}
