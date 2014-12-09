using Assets.Scripts.Attributes;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.GameScripts.GameLogic.Skills.SkillCasters;
using Assets.Scripts.Utility;
using UnityEngine;
using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.Controller
{
    [RequireComponent(typeof(PlayerCharacterSkillsCaster))]
    public class PlayerController : GameLogic
    {
        [SerializeField]
        private AxisOnHold HorizontalAxis;

        [SerializeField] 
        private AxisOnHold VerticalAxis;

        [SerializeField] 
        private ButtonOnPressed Attack1;

        [SerializeField]
        private ButtonOnPressed Attack2;

        [SerializeField]
        private ButtonOnPressed Attack3;

        [SerializeField]
        private ButtonOnPressed Attack4;

        [SerializeField] 
        private ButtonOnPressed Dash;

        public bool ControllerEnabled;

        //This dependency is injected for the sake of better runtime performance
        private PlayerCharacterSkillsCaster _playerCharacterSkillsCaster;

        private bool _skill1Enabled;
        private bool _skill2Enabled;
        private bool _skill3Enabled;
        private bool _skill4Enabled;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _playerCharacterSkillsCaster = GetComponent<PlayerCharacterSkillsCaster>();
            ControllerEnabled = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _skill1Enabled = true;
            _skill2Enabled = true;
            _skill3Enabled = true;
            _skill4Enabled = true;
        }

        protected override void Deinitialize()
        {
        }

        protected override void FixedUpdate()
        {
            if (!ControllerEnabled)
            {
                return;
            }

            if (gameObject.HitPointAtZero())
            {
                return;
            }

            if ((HorizontalAxis.Detect() || VerticalAxis.Detect()))
            {
                Vector2 direction = new Vector2(HorizontalAxis.GetAxisValue(), VerticalAxis.GetAxisValue());

                _playerCharacterSkillsCaster.Direction = direction;
                _playerCharacterSkillsCaster.ActivateMovement(direction);
            }
        }

        protected override void Update()
        {
            if (!ControllerEnabled)
            {
                return;
            }
            base.Update();
            if (gameObject.HitPointAtZero())
            {
                return;
            }

            if (Attack1.Detect() && _skill1Enabled)
            {
                _playerCharacterSkillsCaster.ActivateSkillOne();
            }
            else if (Attack2.Detect() && _skill2Enabled)
            {
                _playerCharacterSkillsCaster.ActivateSkillTwo();
            }
            else if (Attack3.Detect() && _skill3Enabled)
            {
                _playerCharacterSkillsCaster.ActivateSkillThree();
            }
            else if (Attack4.Detect() && _skill4Enabled)
            {
                _playerCharacterSkillsCaster.ActivateSkillFour();
            }
            else if (Dash.Detect())
            {
                _playerCharacterSkillsCaster.ActivateDash();
                TriggerGameEvent(GameEvent.OnPlayerDashButtonPressed);
            }
        }

        [GameEvent(GameEvent.EnableAbility)]
        public void EnableAbility(int id)
        {
            switch (id)
            {
                case 1:
                    _skill1Enabled = true;
                    break;
                case 2:
                    _skill2Enabled = true;
                    break;
                case 3:
                    _skill3Enabled = true;
                    break;
                case 4:
                    _skill4Enabled = true;
                    break;
            }
        }

        [GameEvent(GameEvent.DisableAbility)]
        public void DisableAbility(int id)
        {
            switch (id)
            {
                case 1:
                    _skill1Enabled = false;
                    break;
                case 2:
                    _skill2Enabled = false;
                    break;
                case 3:
                    _skill3Enabled = false;
                    break;
                case 4:
                    _skill4Enabled = false;
                    break;
            }
        }

        [Attributes.GameEvent(GameEvent.EnablePlayerCharacter)]
        public void EnablePlayerCharacter()
        {
            ControllerEnabled = true;
            GetComponent<Collider2D>().enabled = true;
        }

        [GameEvent(GameEvent.DisablePlayerCharacter)]
        public void DisablePlayerCharacter()
        {
            ControllerEnabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
