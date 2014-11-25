using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Misc
{
    [AddComponentMenu("Misc/LifeTimeLimit")]
    public class LifeTimeLimit : GameLogic 
    {
        [Range(0f, float.MaxValue)]
        public float LifeTime;

        protected override void Initialize()
        {
            base.Initialize();
            DisableGameObject(LifeTime);
        }

        protected override void Deinitialize()
        {
        }
    }
}
