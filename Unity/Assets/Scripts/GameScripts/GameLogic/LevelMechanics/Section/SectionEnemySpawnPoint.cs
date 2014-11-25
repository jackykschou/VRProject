using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.LevelMechanics.Section
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(PrefabSpawner))]
    [RequireComponent(typeof(FixTimeDispatcher))]
    [AddComponentMenu("LevelMechanics/Section/SectionEnemySpawnPoint")]
    public class SectionEnemySpawnPoint : SectionLogic 
    {
        public PrefabSpawner PrefabSpawner;

        [Range(0f, 100f)] 
        public float SpawnRadius = 0f;

        public bool FadeInEnemy = false;

        [Range(0f, 5f)]
        public float FadeInTime = 0f;

        public FixTimeDispatcher SpawnCoolDown;

        public Collider2D TriggerArea;

        public bool Activated = true;

        private bool _triggered;
        private bool _deactivated;

        public bool CanSpawn 
        {
            get { return _triggered && PrefabSpawner.CanSpawn() && SpawnCoolDown.CanDispatch() && Activated && SectionActivated && GameManager.Instance.PlayerMainCharacter != null && !GameManager.Instance.PlayerMainCharacter.HitPointAtZero(); }
        }

        public override void OnSectionActivated(int sectionId)
        {
            base.OnSectionActivated(sectionId);
            if (sectionId == SectionId)
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointActivated, gameObject, SectionId);
                TriggerArea.enabled = true;
                _deactivated = false;
            }
        }

        public override void OnSectionDeactivated(int sectionId)
        {
            base.OnSectionDeactivated(sectionId);
            if (sectionId == SectionId && !_deactivated)
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointDeactivated, gameObject, SectionId);
                TriggerArea.enabled = false;
                _deactivated = true;
            }
        }

        protected override void OnTriggerStay2D(Collider2D coll)
        {
            _triggered = true;
        }

        protected override void Update()
        {
            base.Update();
            if (_deactivated)
            {
                return;
            }
            if (SectionActivated && (!Activated || !PrefabSpawner.CanSpawn()))
            {
                TriggerGameEvent(GameEvent.OnSectionEnemySpawnPointDeactivated, gameObject, SectionId);
                TriggerArea.enabled = false;
                _deactivated = true;
                return;
            }
            if (_triggered)
            {
                StartCoroutine(SpawnEnemy());
            }
        }

        private IEnumerator SpawnEnemy()
        {
            const float blockRadius = 0.2f;

            if (!CanSpawn)
            {
                yield break;
            }
            SpawnCoolDown.Dispatch();
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
            while (!UtilityFunctions.LocationPathFindingReachable(transform.position, spawnPosition) ||
                Physics2D.OverlapCircle(spawnPosition, blockRadius, LayerConstants.LayerMask.Obstacle) != null ||
                (Vector2.Distance(spawnPosition, GameManager.Instance.PlayerMainCharacter.transform.position) < 1.0f))
            {
                spawnPosition = new Vector3(Random.Range(transform.position.x - SpawnRadius, transform.position.x + SpawnRadius),
                Random.Range(transform.position.y - SpawnRadius, transform.position.y + SpawnRadius), transform.position.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            PrefabSpawner.SpawnPrefab(spawnPosition, o =>
            {
                if (FadeInEnemy)
                {
                    StartCoroutine(FadeInEnemyIE(o, FadeInTime));
                }
                TriggerGameEvent(GameEvent.OnSectionEnemySpawned, SectionId);
                TriggerGameScriptEvent(GameScriptEvent.OnSectionEnemySpawned, o);
            });
        }

        public IEnumerator FadeInEnemyIE(GameObject o, float time)
        {
            float timePass = 0.0f;
            SpriteRenderer render = o.GetComponent<SpriteRenderer>();
            if (render != null)
            {
                render.color = new Color(render.color.r, render.color.g, render.color.b, 0.0f);
            }
            var motor = o.GetComponent<ObjectMotor2D>();
            float originalSpeed = motor.Speed.Value;
            motor.Speed.Value = 0f;
            while (timePass < time)
            {
                if (render != null)
                {
                    render.color = new Color(render.color.r, render.color.g, render.color.b, timePass / time);
                }
                yield return new WaitForSeconds(Time.deltaTime);
                timePass += Time.deltaTime;
            }
            if (render != null)
            {
                render.color = new Color(render.color.r, render.color.g, render.color.b, 1.0f);
            }
            if (motor != null)
            {
                motor.Speed.Value = originalSpeed;
            }
        }

        [Attributes.GameEvent(GameEvent.SurvivalDifficultyIncreased)]
        public void SurvivalDifficultyIncreased(int difficulty)
        {
            PrefabSpawner.NumberOfSpawn += (int)(PrefabSpawner.NumberOfSpawn * 0.1f * difficulty);
            PrefabSpawner.LimitSpawnValue += (int)(PrefabSpawner.LimitSpawnValue * 0.1f * difficulty);
        }


        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
            TriggerArea = GetComponent<Collider2D>();
            TriggerArea.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer(LayerConstants.LayerNames.SpawnArea);
        }

        protected override void Initialize()
        {
            base.Initialize();
            TriggerArea.enabled = false;
            Activated = true;
            _triggered = false;
            _deactivated = false;
        }

        protected override void Deinitialize()
        {
        }
    }
}
