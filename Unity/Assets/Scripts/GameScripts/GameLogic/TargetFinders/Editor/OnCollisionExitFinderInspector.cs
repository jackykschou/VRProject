using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(OnCollisionExitFinder))]
    public class OnCollisionExitFinderInspector : TargetFinderInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
