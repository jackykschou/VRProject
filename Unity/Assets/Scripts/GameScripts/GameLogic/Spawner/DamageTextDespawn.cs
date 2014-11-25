using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.ObjectMotor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Spawner
{
    [RequireComponent(typeof(TextMotor))]
    [RequireComponent(typeof(TextMesh))]
    public class DamageTextDespawn : GameLogic
    {
        public float OrigDespawnTime = 1.0f;
        public float MinSpeed = 5.0f;
        public float MaxSpeed = 10.0f;
        public float MinDistance = 1.5f;
        public float MaxDistance = 2.0f;

        private TextMesh _mesh;
        private Vector3 _direction;
        private TextMotor _motor;
        private float _speed;
        private float _distance;
        private float _timeLeft;
        private bool _shot;

        protected override void Update()
        {
            if (!_shot) 
                return;

            _mesh.color = new Color(_mesh.color.r, _mesh.color.g, _mesh.color.b, _timeLeft / OrigDespawnTime);
            _timeLeft -= Time.deltaTime;
        }

        public void OnSpawned()
        {
            _motor.Shoot(_direction,_speed,_distance);
            _timeLeft = OrigDespawnTime;
            DisableGameObject(OrigDespawnTime);
            _shot = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _direction = new Vector3(Random.Range(-.45f, .45f), Random.Range(0f, 1f), 0);
            _speed = Random.Range(MinSpeed, MaxSpeed);
            _distance = Random.Range(MinDistance, MaxDistance);
            _mesh = gameObject.GetComponent<TextMesh>();
            _mesh.renderer.sortingLayerName = SortingLayerConstants.SortingLayerNames.HighestLayer;
            _motor = gameObject.GetComponent<TextMotor>();
            _shot = false;
        }
        protected override void Deinitialize()
        {
        }

    }
}