using UnityEngine.UI;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class KillCountMeter : GameLogic
    {
        public Text KillCountText;
        public Text WaveCountText;
        private int _currentKillCount;
        private int _currentWaveNumber;

        protected override void Initialize()
        {
            base.Initialize();
            _currentKillCount = 0;
            _currentWaveNumber = 0;
            if(KillCountText != null)
                KillCountText.text = _currentKillCount.ToString();
            if (WaveCountText != null)
                WaveCountText.text = _currentWaveNumber.ToString();
        }

        protected override void Deinitialize()
        {
        }

        [GameEventAttribute(GameEvent.OnSectionEnemyDespawned)]
        public void IncreaseCount(int sectionId)
        {
            _currentKillCount++;
            if (KillCountText != null)
                KillCountText.text = _currentKillCount.ToString();
        }

        [GameEventAttribute(GameEvent.WaveCountIncreased)]
        public void WaveCountUpdate(int count)
        {
            _currentWaveNumber = count;
            if (WaveCountText != null)
                WaveCountText.text = _currentWaveNumber.ToString();
        }
    }
}
