using UnityEditor;

namespace Assets.Scripts.GameScripts.GameLogic.TargetFinders.Editor
{
    [CustomEditor(typeof(OnCollisionStayFinder))]
    public class OnCollisionStayFinderInspector : TargetFinderInspector 
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
