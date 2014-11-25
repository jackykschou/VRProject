using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/SectionMusicChange")]
    public class SectionMusicChange : SectionLogic
    {
        public Intensity OnActivate;
        public Intensity OnDeactivate;

        protected override void Deinitialize()
        {
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (SectionId == sectionId)
            {
                switch (OnActivate)
                {
                    case Intensity.Light:
                        AudioManager.Instance.SetLightIntensity(LevelManager.Instance.BackGroundMusicLoop);
                        break;
                    case Intensity.Medium:
                        AudioManager.Instance.SetMediumIntensity(LevelManager.Instance.BackGroundMusicLoop);
                        break;
                    case Intensity.Heavy:
                        AudioManager.Instance.SetHeavyIntensity(LevelManager.Instance.BackGroundMusicLoop);
                        break;
                }
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (SectionId == sectionId)
            {
                switch (OnDeactivate)
                {
                    case Intensity.Light:
                        AudioManager.Instance.SetLightIntensity(LevelManager.Instance.BackGroundMusicLoop);
                        break;
                    case Intensity.Medium:
                        AudioManager.Instance.SetMediumIntensity(LevelManager.Instance.BackGroundMusicLoop);
                        break;
                    case Intensity.Heavy:
                        AudioManager.Instance.SetHeavyIntensity(LevelManager.Instance.BackGroundMusicLoop);
                        break;
                }
            }
        }
    }
}
