using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.PowerUp
{
    [AddComponentMenu("PowerUp/Berserk")]
    public class Berserk : SkillPowerUp
    {
        public Color BerserkColor;

        protected override void Apply()
        {
            base.Apply();
            if (AppliedCounter == 1)
            {
                Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeSpriteViewColor, BerserkColor);
            }
        }

        protected override void UnApply()
        {
            Owner.TriggerGameScriptEvent(GameScriptEvent.ChangeSpriteViewToOriginalColor);
            base.UnApply();
        }
    }
}
