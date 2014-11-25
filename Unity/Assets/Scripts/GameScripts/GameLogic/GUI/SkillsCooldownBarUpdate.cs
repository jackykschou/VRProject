using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(PlayerCharacterSkillsCaster))]
    public class SkillsCooldownBarUpdate : GameLogic
    {
        private PlayerCharacterSkillsCaster _playerCharacterSkillsCaster;

        protected override void Initialize()
        {
            base.Initialize();
            _playerCharacterSkillsCaster = gameObject.GetComponent<PlayerCharacterSkillsCaster>();
        }

        protected override void Update()
        {
            TriggerGameEvent(GameEvent.OnPlayerSkillCoolDownUpdate, 1, _playerCharacterSkillsCaster.Skill1.GetCooldownPercentage());
            TriggerGameEvent(GameEvent.OnPlayerSkillCoolDownUpdate, 2, _playerCharacterSkillsCaster.Skill2.GetCooldownPercentage());
            TriggerGameEvent(GameEvent.OnPlayerSkillCoolDownUpdate, 3, _playerCharacterSkillsCaster.Skill3.GetCooldownPercentage());
            TriggerGameEvent(GameEvent.OnPlayerSkillCoolDownUpdate, 4, _playerCharacterSkillsCaster.Skill4.GetCooldownPercentage());
            TriggerGameEvent(GameEvent.OnPlayerSkillCoolDownUpdate, 5, _playerCharacterSkillsCaster.Dash.GetCooldownPercentage());
        }

        protected override void Deinitialize()
        {
        }
    }
}
