using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameScripts.GameViews;

namespace Assets.Scripts.GameScripts.GameLogic
{
    public abstract class GameLogic : GameScript
    {
        public GameView GameView { get; set; }

        protected override void FirstTimeInitialize()
        {
            GameView = GetComponent<GameView>();
            if (GameView == null)
            {
                GameView = GetComponentInParent<GameView>();
            }
            if (GameView == null)
            {
                GameView = GetComponentInChildren<GameView>();
            }
        }

        protected override void Initialize()
        {
        }

        protected abstract override void Deinitialize();

        protected T GetGameLogic<T>() where T : GameLogic
        {
            return GetComponent<T>();
        }

        protected List<T> GetGameLogics<T>() where T : GameLogic
        {
            return GetComponents<T>().ToList();
        }
    }
}