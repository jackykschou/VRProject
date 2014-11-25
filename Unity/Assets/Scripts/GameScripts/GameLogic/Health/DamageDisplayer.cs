using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.GameValue;
using Assets.Scripts.GameScripts.GameLogic.Spawner;
using Assets.Scripts.Managers;
using StateMachine.Action;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.Health
{
    [RequireComponent(typeof(PrefabSpawner))]
    public class DamageDisplayer : GameLogic
    {
        public PrefabSpawner PrefabSpawner;
        public Color textColor;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (PrefabSpawner == null)
            {
                PrefabSpawner = GetComponent<PrefabSpawner>();
            }
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectTakeDamage)]
        public void TakeDamage(float damage, bool crit, GameValue.GameValue health, GameValueChanger gameValueChanger)
        {
            PrefabSpawner.SpawnPrefabImmediate(transform.position, o =>
            {
                TextMesh textMesh = o.GetComponent<TextMesh>();
                textMesh.text = ((int)damage).ToString();
                textMesh.color = textColor;
                if (crit)
                {
                    textMesh.transform.localScale *= 1.5f;
                    textMesh.fontStyle = FontStyle.Italic;
                }
                else
                {
                    textMesh.fontStyle = FontStyle.Normal;
                }
            });
        }

        [GameScriptEvent(Constants.GameScriptEvent.OnObjectDealDamage)]
        public void OnObjectDealDamage(GameValue.GameValue health, GameValueChanger gameValueChanger, float damage, bool crit)
        {
            if (crit)
            {
                PrefabSpawner.SpawnPrefabImmediate(transform.position, o =>
                {
                    TextMesh textMesh = o.GetComponent<TextMesh>();
                    textMesh.text = "CRIT!";
                    textMesh.color = Color.blue;
                });
            }
        }

        protected override void Deinitialize()
        {
        }
    }
}
