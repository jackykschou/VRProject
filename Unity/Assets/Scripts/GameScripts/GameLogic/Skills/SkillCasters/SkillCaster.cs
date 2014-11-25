using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    public abstract class SkillCaster : GameLogic
    {
        public List<Skill> Skills;
        public bool CastingActiveSkill
        {
            get { return Skills.Any(s => s.IsActivate && !s.IsPassive); }
        }
        public bool Moving;
        public Transform Target;
        public abstract Vector2 PointingDirection { get; }

        private float _movingTimer;

        protected override void Initialize()
        {
            base.Initialize();
            Skills = GetComponentsInChildren<Skill>().ToList();
        }

        [GameScriptEventAttribute(GameScriptEvent.UpdateSkills)]
        public void UpdateSkills()
        {
            Skills = GetComponentsInChildren<Skill>().ToList();
        }

        [GameScriptEventAttribute(GameScriptEvent.RemoveSkill)]
        public void RemoveSkill(Skill skill)
        {
            Skills.Remove(skill);
        }

        [GameScriptEventAttribute(GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform;
        }

        [GameScriptEventAttribute(GameScriptEvent.OnCharacterMove)]
        public void OnCharacterMove(Vector2 direction)
        {
            Moving = true;
            if (_movingTimer <= 0f)
            {
                _movingTimer = Time.fixedDeltaTime;
                StartCoroutine(SetMoving());
            }
            else
            {
                _movingTimer = Time.fixedDeltaTime;
            }
        }


        IEnumerator SetMoving()
        {
            while (_movingTimer > 0f)
            {
                yield return new WaitForFixedUpdate();
                _movingTimer -= Time.fixedDeltaTime;
            }
            Moving = false;
        }
    }
}
