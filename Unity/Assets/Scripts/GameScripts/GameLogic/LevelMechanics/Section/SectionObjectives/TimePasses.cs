using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section.SectionObjectives
{
    public class TimePasses : SectionObjective
    {
        [Range(0f, 500f)] 
        public int Time;

        private bool _timePassed;

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _timePassed = false;
            }
            StartCoroutine(CountTime());
        }

        protected override void Initialize()
        {
            base.Initialize();
            _timePassed = false;
        }

        protected override void Deinitialize()
        {
        }

        public override bool ObjectiveCompleted()
        {
            return _timePassed;
        }

        IEnumerator CountTime()
        {
            yield return new WaitForSeconds(Time);
            _timePassed = true;
        }
    }
}
