using System.Collections.Generic;
using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    public abstract class PowerUp : GameLogic
    {
        protected Prefab PowerUpPrefab;

        protected int AppliedCounter;
        private static Dictionary<GameObject, Dictionary<Prefab, PowerUp>> _powerUpMap = new Dictionary<GameObject, Dictionary<Prefab, PowerUp>>();
        public GameObject Owner;

        protected abstract void Apply();
        protected abstract void UnApply();
            
        [Attributes.GameScriptEvent(GameScriptEvent.ApplyPowerUp)]
        public void ApplyPowerUp(GameObject target, Prefab powerUprefab)
        {
            if (!_powerUpMap.ContainsKey(target) ||
                !_powerUpMap[target].ContainsKey(powerUprefab))
            {
                PowerUpPrefab = powerUprefab;
                if (!_powerUpMap.ContainsKey(target))
                {
                    _powerUpMap.Add(target.gameObject, new Dictionary<Prefab, PowerUp>());
                }
                _powerUpMap[target].Add(powerUprefab, this);
                AppliedCounter = 1;
                Owner = target;
                transform.position = Owner.transform.position;
                transform.parent = Owner.transform;
                Apply();
            }
            else
            {
                _powerUpMap[target][powerUprefab].AppliedCounter++;
                _powerUpMap[target][powerUprefab].Apply();
                DisableGameObject();
            }
        }
        
        [Attributes.GameScriptEvent(GameScriptEvent.OnObjectDestroyed)]
        [Attributes.GameEvent(GameEvent.OnPlayerReset)]
        public void UnapplyPowerUp()
        {
            if (Owner == null)
            {
                return;
            }

            UnApply();
            _powerUpMap[Owner].Remove(PowerUpPrefab);
            if (_powerUpMap[Owner].Count == 0)
            {
                _powerUpMap.Remove(Owner);
            }
            transform.parent = null;
            Owner = null;

            if (!Destroyed)
            {
                DisableGameObject();
            }
        }
    }
}
