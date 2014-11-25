using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/AStarGraphUpdater")]
    public class AStarGraphUpdater : GameLogic 
    {
        protected override void Initialize()
        {
            base.Initialize();
            RescanGraph();
        }

        protected override void Deinitialize()
        {
        }

        public void UpdateGraph(Bounds bounds)
        {
            AstarPath.active.UpdateGraphs(bounds);
        }

        [GameEventAttribute(GameEvent.SurvivalSectionStarted)]
        [GameEventAttribute(GameEvent.OnLevelFinishedLoading)]
        public void RescanGraph()
        {
            if (AstarPath.active != null)
            {
                AstarPath.active.Scan();
            }
        }
    }
}
