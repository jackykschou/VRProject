using UnityEngine;

using GameEvent = Assets.Scripts.Constants.GameEvent;
using GameEventAttribute = Assets.Scripts.Attributes.GameEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    [RequireComponent(typeof(Light))]
    public class MouseFollow : GameLogic
    {
        protected override void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit))
                light.transform.LookAt(hit.point);
        }
        protected override void Deinitialize()
        {
        }

    }
}