using Assets.Scripts.Attributes;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc.EventBridger
{
    [AddComponentMenu("Misc/DisableGameObjectOnSurvivalSectionEnded")]
    public class DisableGameObjectOnSurvivalSectionEnded : GameLogic 
    {
        [GameEvent(Constants.GameEvent.SurvivalSectionEnded)]
        public void SurvivalSectionEnded()
        {
            DisableGameObject();
        }

        protected override void Deinitialize()
        {
        }
    }
}
