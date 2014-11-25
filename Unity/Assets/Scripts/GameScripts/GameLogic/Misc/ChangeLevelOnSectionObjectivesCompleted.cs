using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    public class ChangeLevelOnSectionObjectivesCompleted : SectionLogic
    {
        public Prefab LevelPrefab;
        [Range(0, 100f)]
        public float Delay;

        private bool _levelChanged;

        protected override void Initialize()
        {
            base.Initialize();
            _levelChanged = false;
        }

        [Attributes.GameEvent(GameEvent.OnSectionObjectivesCompleted)]
        public void OnSectionObjectivesCompleted(int sectionId)
        {
            if (!_levelChanged && sectionId == SectionId)
            {
                _levelChanged = true;
                StartCoroutine(ChangeLevel());
            }
        }

        IEnumerator ChangeLevel()
        {
            yield return new WaitForSeconds(Delay);
            GameManager.Instance.ChangeLevel(LevelPrefab);
        }

        protected override void Deinitialize()
        {
        }
    }
}
