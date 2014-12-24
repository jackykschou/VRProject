using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.GameScripts.GameLogic.GameValue
{
    [AddComponentMenu("Misc/GameValue")]
    public sealed class GameValue : GameLogic
    {
        public GameObject Owner;

        public float InitialMinValue;
        public float InitialMaxValue;
        public float InitialValue;

        private float _initialPositiveChangeEmphasizePercentage;
        private float _initialNegativeChangeEmphasizePercentage;

        [Range(0f, 10f)]
        public float PositiveChangeEmphasizePercentage;
        [Range(0f, 10f)]
        public float NegativeChangeEmphasizePercentage;

        private float _value;
        private List<GameValueChanger> _valueTempChangeValueCache;
        private Dictionary<GameValueChanger.NonStackableType, float> _nonStackableTypeCache;

        public float Value 
        {
            get { return _value; }
            set
            {
                _value = value;
                TrimValue();
            }
        }

        public int IntValue 
        {
            get { return (int) Value; }
        }

        private float _min;
        public float Min 
        {
            get { return _min; }
            set{ SetBound(value, Max);} 
        }
        private float _max;
        public float Max
        {
            get { return _max; }
            set { SetBound(Min, value); } 
        }

        public bool AtMin
        {
            get { return Mathf.Approximately(Value, Min); }
        }

        public bool AtMax
        {
            get { return Mathf.Approximately(Value, Max); }
        }

        public float Percentage
        {
            get { return Value/Max; }
        }

        public void SetBound(float min, float max)
        {
            _min = (min > Mathf.Max(_max, max)) ? max : min;
            _max = (max < _min) ? Min : max;
            TrimValue();
        }

        public static implicit operator float(GameValue v)
        {
            return v.Value;
        }

        private void TrimValue()
        {
            _value = Mathf.Clamp(_value, Min, Max);
        }

        [Attributes.GameScriptEvent(GameScriptEvent.UpdateGameValueOwner)]
        public void UpdateGameValueOwner(GameObject owner)
        {
            Owner = owner;
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            _initialPositiveChangeEmphasizePercentage = PositiveChangeEmphasizePercentage;
            _initialNegativeChangeEmphasizePercentage = NegativeChangeEmphasizePercentage;
            if (Owner == null)
            {
                Owner = gameObject;
            }
        }

        protected override void Initialize()
        {
            _valueTempChangeValueCache = new List<GameValueChanger>();
            _nonStackableTypeCache = new Dictionary<GameValueChanger.NonStackableType, float>();
            PositiveChangeEmphasizePercentage = _initialPositiveChangeEmphasizePercentage;
            NegativeChangeEmphasizePercentage = _initialNegativeChangeEmphasizePercentage;
            if (Mathf.Approximately(InitialMaxValue, 0) && Mathf.Approximately(InitialMinValue, 0) &&
                Mathf.Approximately(InitialValue, 0))
            {
                SetBound(float.MinValue, float.MaxValue);
                Value = 0;
            }
            else
            {
                SetBound(InitialMinValue, InitialMaxValue);
                Value = InitialValue;
                TrimValue();
            }
        }

        protected override void Deinitialize()
        {
        }

        private float GetEmphasizedChange(float amount)
        {
            if (amount >= 0f)
            {
                return amount + amount * PositiveChangeEmphasizePercentage;
            }
            else
            {
                return amount + amount * NegativeChangeEmphasizePercentage;
            }
        }

        public void ChangeGameValue(GameValueChanger gameValueChanger)
        {
            switch (gameValueChanger.TargetValueType)
            {
                    case GameValueChanger.ChangeTargetValueType.CurrentValue:
                        switch (gameValueChanger.ChangeType)
                        {
                            case GameValueChanger.CurrentValueChangeType.FixedAmount:
                            case GameValueChanger.CurrentValueChangeType.CurrentPercentage:
                            case GameValueChanger.CurrentValueChangeType.MaxPercentage:
                                switch (gameValueChanger.OneTimeDurationType)
                                {
                                    case GameValueChanger.OneTimeChangeDurationType.Permanent:
                                        PermanentChangeCurrentValue(gameValueChanger);
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.Nondeterministic:
                                        StartCoroutine(TempChangeCurrentValueNondeterministic(gameValueChanger));
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger));
                                        break;
                                }
                                break;
                            case GameValueChanger.CurrentValueChangeType.FixedAmountByInterval:
                            case GameValueChanger.CurrentValueChangeType.CurrentPercentageByInterval:
                            case GameValueChanger.CurrentValueChangeType.MaxPercentageByInterval:
                                switch (gameValueChanger.IntervalDurationType)
                                {
                                    case GameValueChanger.ByIntervalChangeDurationType.FixedTime:
                                        StartCoroutine(PermanentChangeCurrentValueByInterval(gameValueChanger));
                                        break;
                                    case GameValueChanger.ByIntervalChangeDurationType.Nondeterministic:
                                        StartCoroutine(TempChangeCurrentValueByIntervalNondeterministic(gameValueChanger));
                                        break;
                                }
                                break;
                        }
                    break;
                    case GameValueChanger.ChangeTargetValueType.Min:
                        switch (gameValueChanger.OneTimeDurationType)
                        {
                            case GameValueChanger.OneTimeChangeDurationType.Permanent:
                                Min += gameValueChanger.Amount;
                                break;
                            case GameValueChanger.OneTimeChangeDurationType.Nondeterministic:
                                StartCoroutine(TempChangeMinNondeterministic(gameValueChanger));
                                break;
                            case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                StartCoroutine(TempChangeMinFixedInterval(gameValueChanger));
                                break;
                        }
                        break;
                    case GameValueChanger.ChangeTargetValueType.Max:
                        switch (gameValueChanger.OneTimeDurationType)
                        {
                            case GameValueChanger.OneTimeChangeDurationType.Permanent:
                                Max += gameValueChanger.Amount;
                                break;
                            case GameValueChanger.OneTimeChangeDurationType.Nondeterministic:
                                StartCoroutine(TempChangeMaxNondeterministic(gameValueChanger));
                                break;
                            case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                StartCoroutine(TempChangeMaxFixedInterval(gameValueChanger));
                                break;
                        }
                        break;
            }
        }

        public void UnchangeGameValue(GameValueChanger gameValueChanger)
        {
            if (!_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                return;
            }
            _valueTempChangeValueCache.Remove(gameValueChanger);
        }

        private IEnumerator TempChangeMaxFixedInterval(GameValueChanger gameValueChanger)
        {
            float amount = gameValueChanger.Amount;
            Max += amount;
            yield return new WaitForSeconds(gameValueChanger.ChangeDuration);
            Max -= amount;
        }

        private IEnumerator TempChangeMinFixedInterval(GameValueChanger gameValueChanger)
        {
            float amount = gameValueChanger.Amount;
            Min += amount;
            yield return new WaitForSeconds(gameValueChanger.ChangeDuration);
            Min -= amount;
        }

        private IEnumerator TempChangeMinNondeterministic(GameValueChanger gameValueChanger)
        {
            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield break;
            }
            float amount = gameValueChanger.Amount;
            Min += amount;
            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield return new WaitForSeconds(0.1f);
            }
            Min -= amount;
        }

        private IEnumerator TempChangeMaxNondeterministic(GameValueChanger gameValueChanger)
        {
            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield break;
            }
            float amount = gameValueChanger.Amount;
            Max += amount;
            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield return new WaitForSeconds(0.1f);
            }
            Max -= amount;
        }

        private void PermanentChangeCurrentValue(GameValueChanger gameValueChanger)
        {
            float amount = GetChangeAmount(gameValueChanger);
            Value += amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, gameValueChanger.LastAmountCrited);
        }

        private IEnumerator TempChangeCurrentValue(GameValueChanger gameValueChanger)
        {
            float amount = GetChangeAmount(gameValueChanger);
            Value += amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, gameValueChanger.LastAmountCrited);
            yield return new WaitForSeconds(gameValueChanger.ChangeDuration);
            Value -= amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, -amount, gameValueChanger.LastAmountCrited);
        }

        private IEnumerator TempChangeCurrentValueNondeterministic(GameValueChanger gameValueChanger)
        {
            if (!gameValueChanger.Stackable && _nonStackableTypeCache.ContainsKey(gameValueChanger.NonStackableLabel))
            {
                yield break;
            }

            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield break;
            }

            _nonStackableTypeCache.Add(gameValueChanger.NonStackableLabel, 0f);

            float amount = GetChangeAmount(gameValueChanger);
            bool crited = gameValueChanger.LastAmountCrited;
            Value += amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, crited);
            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield return new WaitForSeconds(0.1f);
            }
            Value -= amount;
            _nonStackableTypeCache.Remove(gameValueChanger.NonStackableLabel);
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, -amount, crited);
        }

        private IEnumerator PermanentChangeCurrentValueByInterval(GameValueChanger gameValueChanger)
        {
            var gameValueChangerData = gameValueChanger.CreateGameValueChangerData();

            if (!gameValueChangerData.Stackable && _nonStackableTypeCache.ContainsKey(gameValueChangerData.NonStackableLabel))
            {
                _nonStackableTypeCache[gameValueChangerData.NonStackableLabel] = gameValueChangerData.ChangeDuration;
                yield break;
            }

            _nonStackableTypeCache.Add(gameValueChangerData.NonStackableLabel, gameValueChangerData.ChangeDuration);

            while (_nonStackableTypeCache[gameValueChangerData.NonStackableLabel] >= 0)
            {
                float amount = GetChangeAmount(gameValueChangerData);
                bool crited = gameValueChangerData.LastAmountCrited;
                Value += amount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, crited);
                yield return new WaitForSeconds(gameValueChangerData.ChangeDuration);
                _nonStackableTypeCache[gameValueChangerData.NonStackableLabel] = _nonStackableTypeCache[gameValueChangerData.NonStackableLabel]
                    - gameValueChangerData.ChangeDuration;
            }
            _nonStackableTypeCache.Remove(gameValueChangerData.NonStackableLabel);
        }

        private IEnumerator TempChangeCurrentValueByIntervalNondeterministic(GameValueChanger gameValueChanger)
        {
            if (!gameValueChanger.Stackable && _nonStackableTypeCache.ContainsKey(gameValueChanger.NonStackableLabel))
            {
                yield break;
            }

            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield break;
            }

            _nonStackableTypeCache.Add(gameValueChanger.NonStackableLabel, 0f);

            GameValueChanger.NonStackableType nonStackableType = gameValueChanger.NonStackableLabel;

            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                float amount = GetChangeAmount(gameValueChanger);
                Value += amount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, gameValueChanger.LastAmountCrited);
                yield return new WaitForSeconds(gameValueChanger.ChangeInterval);
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        public override void EditorUpdate()
        {
            InitialMinValue = Mathf.Clamp(InitialMinValue, float.MinValue, InitialMaxValue);
            InitialMaxValue = Mathf.Clamp(InitialMaxValue, InitialMinValue, float.MaxValue);
            InitialValue = Mathf.Clamp(InitialValue, InitialMinValue, InitialMaxValue);
        }

        private float GetChangeAmount(GameValueChanger gameValueChanger)
        {
            float amount = 0f;
            switch (gameValueChanger.ChangeType)
            {
                case GameValueChanger.CurrentValueChangeType.FixedAmount:
                    amount = GetEmphasizedChange(gameValueChanger.Amount);
                    break;
                case GameValueChanger.CurrentValueChangeType.CurrentPercentage:
                    amount = GetEmphasizedChange(gameValueChanger.Amount * Value);
                    break;
                case GameValueChanger.CurrentValueChangeType.MaxPercentage:
                    amount = GetEmphasizedChange(gameValueChanger.Amount * Max);
                    break;
            }
            return amount;
        }

        private float GetChangeAmount(GameValueChanger.GameValueChangerData gameValueChangerData)
        {
            float amount = 0f;
            switch (gameValueChangerData.ChangeType)
            {
                case GameValueChanger.CurrentValueChangeType.FixedAmount:
                    amount = GetEmphasizedChange(gameValueChangerData.Amount);
                    break;
                case GameValueChanger.CurrentValueChangeType.CurrentPercentage:
                    amount = GetEmphasizedChange(gameValueChangerData.Amount * Value);
                    break;
                case GameValueChanger.CurrentValueChangeType.MaxPercentage:
                    amount = GetEmphasizedChange(gameValueChangerData.Amount * Max);
                    break;
            }
            return amount;
        }
    }
}
