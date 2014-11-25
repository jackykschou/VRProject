using Assets.Scripts.Constants;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [AddComponentMenu("PowerUp/ExplodeOnDeath")]
    public class ExplodeOnDeath : PowerUp
    {
        private GameObject _smoke;

        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectHasNoHitPoint)]
        public void OnObjectHasNoHitPoint()
        {
            PrefabManager.Instance.SpawnPrefab(Prefab.OnDeathPreExplosion, Owner.transform.position);
        }

        protected override void Deinitialize()
        {
        }

        protected override void Apply()
        {
            PrefabManager.Instance.SpawnPrefab(Prefab.ExplosionOnDeathSmoke, Owner.transform.position, o =>
            {
                _smoke = o;
            });
            _smoke.transform.parent = Owner.transform;
            _smoke.transform.position = Owner.transform.position;
        }

        protected override void UnApply()
        {
            if (_smoke != null)
            {
                _smoke.transform.parent = null;
                PrefabManager.Instance.DespawnPrefab(_smoke);
            }
        }
    }
}
