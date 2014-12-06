using System;
using System.Collections.Generic;
using Assets.Scripts.GameScripts.GameLogic.Misc;
using Assets.Scripts.GameScripts.GameLogic.Skills;
using UnityEngine;
using UnityEngine.UI;

using GameEvent = Assets.Scripts.Constants.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Slider))]
    public class SkillCoolDownSlider : GameLogic
    {
        [Range(0, 5)] // 0 is movement skill
        public int SkillId;
        public Slider CooldownBar;
        public Transform Fill;
        public Transform Icon;
        public Transform Highlight;
        public FixTimeDispatcher SkillCooldownFixTimeDispatcher;
        public Text SkillTimerText; 

        private Image _buttonIconImage;
        private Image _buttonIconImageHighlight;

        protected override void Initialize()
        {
            base.Initialize();
            CooldownBar = GetComponent<Slider>();
            if(Icon)
                _buttonIconImage = Icon.GetComponent<Image>();
            if(Highlight)
                _buttonIconImageHighlight = Highlight.GetComponent<Image>();
        }

        protected override void Deinitialize()
        {
            DisableHighlight(SkillId);
        }

        [Attributes.GameEvent(GameEvent.OnPlayerSkillCoolDownUpdate)]
        public void UpdateSkillCoolDown(int id, float percentage)
        {
            if (id == SkillId)
            {
                CooldownBar.value = percentage;
                if (SkillCooldownFixTimeDispatcher == null)
                    return;
                if (SkillCooldownFixTimeDispatcher.CanDispatch() || percentage >= .99f)
                    SkillTimerText.text = "";
                else
                    SkillTimerText.text = (SkillCooldownFixTimeDispatcher.DispatchInterval - (SkillCooldownFixTimeDispatcher.DispatchInterval * CooldownBar.value)).ToString("#.##") + "s";
            }
        }

        [Attributes.GameEvent(GameEvent.EnableHighlightSkill)]
        public void EnableHighlight(int id, float origHighlightDuration)
        {
            if (id == SkillId)
            {
                if (_buttonIconImageHighlight != null && _buttonIconImage != null)
                {

                    _buttonIconImageHighlight.color = new Color(_buttonIconImageHighlight.color.r,
                                                                _buttonIconImageHighlight.color.g,
                                                                _buttonIconImageHighlight.color.b, 1.0f);
                    _buttonIconImage.color = new Color(_buttonIconImage.color.r,
                                                       _buttonIconImage.color.g,
                                                       _buttonIconImage.color.b, 1.0f);
                }
            }
        }

        [Attributes.GameEvent(GameEvent.DisableHighlightSkill)]
        public void DisableHighlight(int id)
        {
            if (id == SkillId)
            {
                if (_buttonIconImageHighlight != null && _buttonIconImage != null)
                {
                    _buttonIconImageHighlight.color = new Color(_buttonIconImageHighlight.color.r, 
                                                                _buttonIconImageHighlight.color.g,
                                                                _buttonIconImageHighlight.color.b, 0.0f);
                    _buttonIconImage.color = new Color(_buttonIconImage.color.r, 
                                                       _buttonIconImage.color.g, 
                                                       _buttonIconImage.color.b, 0.0f);
                }
            }
        }
    }
}
