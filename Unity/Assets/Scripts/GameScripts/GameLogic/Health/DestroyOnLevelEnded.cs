using Assets.Scripts.Constants;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    public class DestroyOnLevelEnded : GameLogic
    {
        protected override void Deinitialize()
        {
        }

        [Attributes.GameEvent(GameEvent.OnLevelEnded)]
        public void DisableGameObjectOnLevelEnded()
        {
            DisableGameObject();
        }
    }
}
