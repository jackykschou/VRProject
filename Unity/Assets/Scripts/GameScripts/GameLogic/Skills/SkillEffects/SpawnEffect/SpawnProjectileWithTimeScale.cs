using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.SpawnEffect
{
    [RequireComponent(typeof(PositionIndicator))]
    [AddComponentMenu("Skill/SkillEffect/SpawnProjectileWithTimeScale")]
    public class SpawnProjectileWithTimeScale : SkillEffect
    {
        public PrefabSpawner PrefabSpawner;
        public PositionIndicator Position;
        [Range(-360f, 360f)]
        public float ShootAngle;

        private float _time;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            _time = 0f;
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
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime, _time);
                Vector2 castDirecation = Quaternion.AngleAxis(ShootAngle, Vector3.forward) * Skill.Caster.PointingDirection;
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateProjectileDirection, castDirecation);
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateProjectileTarget, Skill.Caster.Target);
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateProjectileDestination, (Vector2)Skill.Caster.Target.transform.position);
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateGameValueChangerOwner, Skill.Caster.gameObject);
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.UpdateGameValueOwner, Skill.Caster.gameObject);
                TriggerGameScriptEvent(Constants.GameScriptEvent.HeavyChargeShootCritChangeAndDamageUpdate, o);
                o.TriggerGameScriptEvent(Constants.GameScriptEvent.ShootProjectile);
            });
        }

        [GameScriptEvent(Constants.GameScriptEvent.UpdateSkillButtonHoldEffectTime)]
        void UpdateSkillHoldEffectTime(float time)
        {
            _time = time;
        }
    }
}
