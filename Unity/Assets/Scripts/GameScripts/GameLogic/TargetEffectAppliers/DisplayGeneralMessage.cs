using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.TargetEffectAppliers
{
    [AddComponentMenu("TargetEffectApplier/DisplayGeneralMessage")]
    public class DisplayGeneralMessage : TargetEffectApplier
    {
        public string Message;

        protected override void ApplyEffect(GameObject target)
        {
            MessageManager.Instance.DisplayMessage(Message, Vector3.up);
        }
    }
}
