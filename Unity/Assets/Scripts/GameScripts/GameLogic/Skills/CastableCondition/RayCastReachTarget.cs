using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition
{
    [AddComponentMenu("Skill/CastableCondition/RayCastReachTarget")]
    public class RayCastReachTarget : SkillCastableCondition
    {
        public List<int> RayCastPhysicalLayers = new List<int>();
        public float Range;

        public override bool CanCast()
        {
            if (Skill.Caster.Target == null)
            {
                return false;
            }
            RaycastHit2D raycast = Physics2D.Raycast(Skill.Caster.transform.position, 
                UtilityFunctions.GetDirection(Skill.Caster.transform.position, Skill.Caster.Target.position),
                Range, LayerMask.GetMask(RayCastPhysicalLayers.Select(l => LayerMask.LayerToName(l)).ToArray()));
            if (raycast.collider == null)
            {
                return false;
            }
            return raycast.collider.gameObject == Skill.Caster.Target.gameObject;
        }

        protected override void Initialize()
        {
            base.Initialize();
            if (RayCastPhysicalLayers == null)
            {
                RayCastPhysicalLayers = new List<int>();
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
