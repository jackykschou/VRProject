using UnityEngine;

namespace Assets.Scripts.GameScripts.GameViews
{
    [AddComponentMenu("GameView/StaticAnimatedSpriteGameView")]
    [RequireComponent(typeof(Animator))]
    public class StaticAnimatedSpriteGameView : StaticSpriteGameView
    {
        protected Animator Animator;

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            Animator = GetComponent<Animator>();
        }
    }
}
