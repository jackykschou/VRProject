using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/ChangeLevelOnSectionDeactivated")]
    public class ChangeLevelOnSectionDeactivated : SectionLogic
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

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId && !_levelChanged && !GameManager.Instance.PlayerMainCharacter.HitPointAtZero())
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
