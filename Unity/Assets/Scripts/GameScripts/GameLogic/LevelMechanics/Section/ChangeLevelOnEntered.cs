using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.PhysicsBody;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [AddComponentMenu("LevelMechanics/Section/ChangeLevelOnEntered")]
    [RequireComponent(typeof(PlayerInteractiveAreaPhysicsBody))]
    public class ChangeLevelOnEntered : SectionLogic
    {
        public Prefab ToLevel;
        [Range(0f, 10f)] 
        public float ChangeLevelDelay;
        private bool _changed;
        private bool _activated;

        protected override void Initialize()
        {
            base.Initialize();
            _changed = false;
            _activated = false;
        }

        protected override void Deinitialize()
        {
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId)
            {
                _activated = true;
            }
        }

        [Attributes.GameScriptEvent(Constants.GameScriptEvent.OnPhysicsBodyOnTriggerStay2D)]
        public void OnPhysicsBodyOnTriggerStay2D(Collider2D coll)
        {
            if (!GameManager.Instance.PlayerMainCharacter.HitPointAtZero() && !_changed && _activated)
            {
                _changed = true;
                StartCoroutine(ChangeLevel());
            }
        }

        private IEnumerator ChangeLevel()
        {
            yield return new WaitForSeconds(ChangeLevelDelay);
            GameManager.Instance.ChangeLevel(ToLevel);
        }
    }
}
