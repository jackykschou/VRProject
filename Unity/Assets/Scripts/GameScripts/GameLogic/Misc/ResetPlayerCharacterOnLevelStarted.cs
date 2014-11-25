using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/ResetPlayerCharacterOnLevelStarted")]
    public class ResetPlayerCharacterOnLevelStarted : GameLogic
    {
        [GameEvent(Constants.GameEvent.OnLevelStarted)]
        public void OnLevelStarted()
        {
            GameManager.Instance.PlayerMainCharacter.SetActive(false);
            GameManager.Instance.PlayerMainCharacter.SetActive(true);
            GameEventManager.Instance.TriggerGameEvent(Constants.GameEvent.OnPlayerReset);
        }

        protected override void Deinitialize()
        {
        }
    }
}
