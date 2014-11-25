using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PositionIndicator))]
    [AddComponentMenu("Skill/SkillEffect/SpawnPrefab")]
    public class SpawnPrefab : SkillEffect
    {
        public Prefab Prefab;
        public PositionIndicator PositionIndicator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PositionIndicator == null)
            {
                PositionIndicator = GetComponent<PositionIndicator>();
            }
        }

        public override void Activate()
        {
            base.Activate();
            PrefabManager.Instance.SpawnPrefabImmediate(Prefab, PositionIndicator.Position.position);
            Activated = false;
        }
    }
}
