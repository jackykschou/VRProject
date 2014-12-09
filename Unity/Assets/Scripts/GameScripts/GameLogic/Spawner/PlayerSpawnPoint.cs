using Assets.Scripts.Attributes;
using Assets.Scripts.Managers;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner
{
    public class PlayerSpawnPoint : GameLogic 
    {
        protected override void Deinitialize()
        {
        }

        [GameEvent(GameEvent.OnLevelFinishedLoading)]
        public void SpawnPlayer()
        {
            GameManager.Instance.PlayerMainCharacter.transform.position = transform.position;
        }
    }
}
