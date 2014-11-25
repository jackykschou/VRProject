using Assets.Scripts.Utility;
using UnityEditor;

namespace Assets.Scripts.Managers.Editor
{
    [CustomEditor(typeof(ChanceBasedEventManager))]
    public class ChanceBasedEventManagerInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            ChanceBasedEventManager manager = (ChanceBasedEventManager)target;

            if (manager.EventBaseChances == null)
            {
                return;
            }

            for (int i = 0; i < manager.ChanceBasedEvents.Count; ++i)
            {
                EditorGUILayout.TextField("Base Chances");
                manager.EventBaseChances[i] = EditorGUILayout.FloatField(manager.ChanceBasedEvents[i], manager.EventBaseChances[i]);
                EditorGUILayout.TextField("Chance Change Amounts On Roll Success");
                manager.EventChanceChangeAmountsOnRollSuccess[i] = EditorGUILayout.FloatField(manager.ChanceBasedEvents[i], manager.EventChanceChangeAmountsOnRollSuccess[i]);
                EditorGUILayout.TextField("Chance Change Amounts On Roll Fail");
                manager.EventChanceChangeAmountsOnRollFail[i] = EditorGUILayout.FloatField(manager.ChanceBasedEvents[i], manager.EventChanceChangeAmountsOnRollFail[i]);
                EditorGUILayout.TextField("Cooldowns");
                manager.EventCooldowns[i] = EditorGUILayout.FloatField(manager.ChanceBasedEvents[i], manager.EventCooldowns[i]);
            }
        }
    }
}
