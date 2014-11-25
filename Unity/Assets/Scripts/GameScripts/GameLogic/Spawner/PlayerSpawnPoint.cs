using Assets.Scripts.Managers;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner
{
    public class PlayerSpawnPoint : GameLogic 
    {
        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.OnLevelFinishedLoading)]
        public void SpawnPlayer()
        {
            GameManager.Instance.PlayerMainCharacter.transform.position = transform.position;
        }
    }
}
