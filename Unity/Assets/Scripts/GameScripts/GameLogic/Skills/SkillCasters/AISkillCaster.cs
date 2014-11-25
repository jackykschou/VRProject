using System.Linq;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters
{
    public class AISkillCaster : SkillCaster
    {
        public FixTimeDispatcher MinimumCoolDown;

        public override Vector2 PointingDirection
        {
            get
            {
                if (Target == null)
                {
                   return UtilityFunctions.GetFacingDirectionVector(GameView.FacingDirection);
                }
                return UtilityFunctions.GetDirection(transform.position, Target.position).normalized;
            }
        }

        public bool CanCastAnySkill()
        {
            return Skills.Any(s => s.CanActivate() && (MinimumCoolDown.CanDispatch() || s.IsPassive)) && !gameObject.HitPointAtZero();
        }

        public void CastSkill()
        {
            if (!CanCastAnySkill())
            {
                return;
            }

            int index = Random.Range(0, Skills.Count);
            while (!Skills[index].CanActivate())
            {
                index = Random.Range(0, Skills.Count);
            }

            Skills[index].Activate();
            MinimumCoolDown.Dispatch();
        }

        protected override void Deinitialize()
        {
        }
    }
}
