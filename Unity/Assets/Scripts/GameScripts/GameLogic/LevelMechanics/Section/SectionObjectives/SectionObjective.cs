using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    [RequireComponent(typeof(SectionObjectiveTracker))]
    public abstract class SectionObjective : SectionLogic
    {
        public abstract bool ObjectiveCompleted();
    }
}
