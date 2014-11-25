namespace Assets.Scripts.GameScripts.GameLogic.Skills.SkillEffects.ActivateCondition
{
    public abstract class SkillEffectActivateCondition : GameLogic
    {
        public SkillEffect SkillEffect;

        public abstract bool CanActivate();

        protected override void Deinitialize()
        {
        }
    }
}
