using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValue.Editor
{
    [CustomEditor(typeof(GameValueChanger))]
    public class GameValueChangerInspector : UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            GameValueChanger changer = (GameValueChanger)target;

            changer.LabelName = EditorGUILayout.TextField("Label Name", changer.LabelName);

            changer.TargetValueType = (GameValueChanger.ChangeTargetValueType)EditorGUILayout.EnumPopup("Target Value", changer.TargetValueType);

            if (changer.TargetValueType == GameValueChanger.ChangeTargetValueType.CurrentValue)
            {
                changer.ChangeType = (GameValueChanger.CurrentValueChangeType)EditorGUILayout.EnumPopup("Change Type", changer.ChangeType);
                switch (changer.ChangeType)
                {
                    case GameValueChanger.CurrentValueChangeType.FixedAmount:
                    case GameValueChanger.CurrentValueChangeType.FixedAmountByInterval:
                        changer._amount = EditorGUILayout.FloatField("Amount", changer._amount);
                        break;
                    case GameValueChanger.CurrentValueChangeType.CurrentPercentage:
                    case GameValueChanger.CurrentValueChangeType.CurrentPercentageByInterval:
                        changer._amount = EditorGUILayout.FloatField("Percentage", changer._amount);
                        break;
                    case GameValueChanger.CurrentValueChangeType.MaxPercentage:
                    case GameValueChanger.CurrentValueChangeType.MaxPercentageByInterval:
                        changer._amount = EditorGUILayout.FloatField("Max Percentage", changer._amount);
                        break;
                }
                switch (changer.ChangeType)
                {
                    case GameValueChanger.CurrentValueChangeType.FixedAmount:
                    case GameValueChanger.CurrentValueChangeType.CurrentPercentage:
                    case GameValueChanger.CurrentValueChangeType.MaxPercentage:
                        changer.OneTimeDurationType = (GameValueChanger.OneTimeChangeDurationType)EditorGUILayout.EnumPopup("Duration Type", changer.OneTimeDurationType);
                        if (changer.OneTimeDurationType == GameValueChanger.OneTimeChangeDurationType.TempFixedTime)
                        {
                            changer.ChangeDuration = EditorGUILayout.FloatField("Change Duration", changer.ChangeDuration);
                            changer.ChangeDuration = Mathf.Clamp(changer.ChangeDuration, 0, float.MaxValue);
                        }
                        break;
                    case GameValueChanger.CurrentValueChangeType.FixedAmountByInterval:
                    case GameValueChanger.CurrentValueChangeType.CurrentPercentageByInterval:
                    case GameValueChanger.CurrentValueChangeType.MaxPercentageByInterval:
                        changer.ChangeInterval = EditorGUILayout.FloatField("Change Interval", changer.ChangeInterval);
                        changer.ChangeInterval = Mathf.Clamp(changer.ChangeInterval, 0f, 100.0f);
                        changer.IntervalDurationType = (GameValueChanger.ByIntervalChangeDurationType)EditorGUILayout.EnumPopup("Duration Type", changer.IntervalDurationType);
                        if (changer.IntervalDurationType == GameValueChanger.ByIntervalChangeDurationType.FixedTime)
                        {
                            changer.ChangeDuration = EditorGUILayout.FloatField("Change Duration", changer.ChangeDuration);
                            changer.ChangeDuration = Mathf.Clamp(changer.ChangeDuration, 0, float.MaxValue);
                        }
                        changer.Stackable = EditorGUILayout.Toggle("Change Stackable", changer.Stackable);
                        if (!changer.Stackable)
                        {
                            changer.NonStackableLabel = (GameValueChanger.NonStackableType)EditorGUILayout.EnumPopup("Non Stackable Type", changer.NonStackableLabel);
                        }
                        break;

                }

            }
            else
            {
                changer._amount = EditorGUILayout.FloatField("Amount", changer._amount);
                changer.OneTimeDurationType = (GameValueChanger.OneTimeChangeDurationType)EditorGUILayout.EnumPopup("Duration Type", changer.OneTimeDurationType);
                if (changer.OneTimeDurationType == (GameValueChanger.OneTimeChangeDurationType.TempFixedTime))
                {
                    changer.ChangeDuration = EditorGUILayout.FloatField("Change Duration", changer.ChangeDuration);
                    changer.ChangeDuration = Mathf.Clamp(changer.ChangeDuration, 0, float.MaxValue);
                }
            }
            changer.CriticalChance = EditorGUILayout.FloatField("Critical Chance", changer.CriticalChance);
            changer.CriticalChance = Mathf.Clamp(changer.CriticalChance, 0f, 1.0f);
            changer.CriticalPercentage = EditorGUILayout.FloatField("Critical Percentage", changer.CriticalPercentage);
            changer.CriticalPercentage = Mathf.Clamp(changer.CriticalPercentage, 0f, 100.0f);
            changer.AmountVariantPercentage = EditorGUILayout.FloatField("Change Amount Variant Percentage", changer.AmountVariantPercentage);
            changer.AmountVariantPercentage = Mathf.Clamp(changer.AmountVariantPercentage, 0f, 100.0f);

            #pragma warning disable 618
                changer.Owner = EditorGUILayout.ObjectField("Owner", changer.Owner, typeof(GameObject)) as GameObject;
            #pragma warning restore 618
        }
    }
}
