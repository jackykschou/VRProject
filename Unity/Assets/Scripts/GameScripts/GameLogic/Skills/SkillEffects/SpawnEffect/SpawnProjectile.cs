using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.Skills.CastableCondition;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;
using GameScriptEvent = Assets.Scripts.Constants.GameScriptEvent;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PrefabSpawner))]
    [RequireComponent(typeof(TargetNotNull))]
    [RequireComponent(typeof(PositionIndicator))]
    [AddComponentMenu("Skill/SkillEffect/SpawnProjectile")]
    public class SpawnProjectile : SkillEffect
    {
        public PrefabSpawner PrefabSpawner;
        public PositionIndicator Position;
        [Range(-360f, 360f)]
        public float ShootAngle;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        public override void Activate()
        {
            base.Activate();
            StartSpawnProjectile();
            Activated = false;
        }

        public void StartSpawnProjectile()
        {
            PrefabSpawner.SpawnPrefabImmediate(Position.Position.position, o =>
            {
                Vector2 castDirecation = Quaternion.AngleAxis(ShootAngle, Vector3.forward) * Skill.Caster.PointingDirection;
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDirection, castDirecation);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileTarget, Skill.Caster.Target);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateProjectileDestination, (Vector2)Skill.Caster.Target.transform.position);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateGameValueChangerOwner, Skill.Caster.gameObject);
                o.TriggerGameScriptEvent(GameScriptEvent.UpdateGameValueOwner, Skill.Caster.gameObject);
                TriggerGameScriptEvent(GameScriptEvent.HeavyChargeShootCritChangeAndDamageUpdate, o);
                o.TriggerGameScriptEvent(GameScriptEvent.ShootProjectile);
            });
        }
    }
}
