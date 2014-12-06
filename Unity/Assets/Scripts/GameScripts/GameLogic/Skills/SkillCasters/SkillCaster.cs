using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Attributes;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;

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

        [GameScriptEvent(GameScriptEvent.UpdateSkills)]
        public void UpdateSkills()
        {
            Skills = GetComponentsInChildren<Skill>().ToList();
        }

        [GameScriptEvent(GameScriptEvent.RemoveSkill)]
        public void RemoveSkill(Skill skill)
        {
            Skills.Remove(skill);
        }

        [GameScriptEvent(GameScriptEvent.OnNewTargetDiscovered)]
        public void UpdateTarget(GameObject target)
        {
            Target = target.transform;
        }

        [GameScriptEvent(GameScriptEvent.OnCharacterMove)]
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
