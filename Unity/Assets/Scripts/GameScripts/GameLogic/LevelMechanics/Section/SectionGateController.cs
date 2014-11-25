using UnityEngine;
using System.Collections;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(SectionGateBlock))]
    public class SectionGateController : SectionLogic
    {
        [Range(0, 20)]
        public int MinDelay = 3;
        [Range(0, 20)]
        public int MaxDelay = 5;

        private SectionGateBlock _gateBlock;
        private bool _enabled;
        private float _timeElapsed;
        private float _curTargetTime;
        private bool _on;

        protected override void Deinitialize()
        {
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _gateBlock = GetComponent<SectionGateBlock>();
            _enabled = false;
            _on = false;
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                _enabled = true;
                _curTargetTime = Random.Range(MinDelay, MaxDelay);
                _timeElapsed = 0.0f;
                Flip(false);
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                _timeElapsed = 0.0f;
                Flip(false);
                _enabled = false;
            }
        }

        protected override void Update()
        {
            base.Update();
            if (_enabled)
            {
                _timeElapsed += Time.deltaTime;
                if (_timeElapsed > _curTargetTime)
                {
                    _curTargetTime = Random.Range(MinDelay, MaxDelay);
                    Flip(!_on);
                    _timeElapsed = 0.0f;
                }
            }
        }

        private void Flip(bool on)
        {
            if (on)
            {
                _gateBlock.LockGate();
            }
            else
            {
                _gateBlock.UnLockGate();
            }
            _on = on;
        }
    }
}
