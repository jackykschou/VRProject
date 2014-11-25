using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constants;
using Assets.Scripts.Utility;
using UnityEngine;

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
        private List<GameValueChanger.NonStackableType> _nonStackableTypeCache;

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
            _nonStackableTypeCache = new List<GameValueChanger.NonStackableType>();
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
                return amount + amount  *PositiveChangeEmphasizePercentage;
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
                                switch (gameValueChanger.OneTimeDurationType)
                                {
                                    case GameValueChanger.OneTimeChangeDurationType.Permanent:
                                        float amount = gameValueChanger.Amount;
                                        amount = GetEmphasizedChange(amount);
                                        Value += amount;
                                        TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, gameValueChanger.LastAmountCrited);
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.Nondeterministic:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger, gameValueChanger.Amount, gameValueChanger.LastAmountCrited));
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger, gameValueChanger.Amount, gameValueChanger.ChangeDuration, gameValueChanger.LastAmountCrited));
                                        break;
                                }
                                break;
                            case GameValueChanger.CurrentValueChangeType.CurrentPercentage:
                                switch (gameValueChanger.OneTimeDurationType)
                                {
                                    case GameValueChanger.OneTimeChangeDurationType.Permanent:
                                        float amount = gameValueChanger.Amount;
                                        amount = GetEmphasizedChange(amount);
                                        amount = Value * amount;
                                        Value += amount;
                                        TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, gameValueChanger.LastAmountCrited);
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.Nondeterministic:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger, Value * gameValueChanger.Amount, gameValueChanger.LastAmountCrited));
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger, Value * gameValueChanger.Amount, gameValueChanger.ChangeDuration, gameValueChanger.LastAmountCrited));
                                        break;
                                }
                                break;
                            case GameValueChanger.CurrentValueChangeType.MaxPercentage:
                                switch (gameValueChanger.OneTimeDurationType)
                                {
                                    case GameValueChanger.OneTimeChangeDurationType.Permanent:
                                        float amount = gameValueChanger.Amount;
                                        amount = GetEmphasizedChange(amount);
                                        amount = amount * Max;
                                        Value += amount;
                                        TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, gameValueChanger.LastAmountCrited);
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.Nondeterministic:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger, Max * gameValueChanger.Amount, gameValueChanger.LastAmountCrited));
                                        break;
                                    case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                        StartCoroutine(TempChangeCurrentValue(gameValueChanger, Max * gameValueChanger.Amount, gameValueChanger.ChangeDuration, gameValueChanger.LastAmountCrited));
                                        break;
                                }
                                break;
                            case GameValueChanger.CurrentValueChangeType.FixedAmountByInterval:
                                switch (gameValueChanger.IntervalDurationType)
                                {
                                    case GameValueChanger.ByIntervalChangeDurationType.FixedTime:
                                        StartCoroutine(ChangeHealthFixedAmountByInterval(gameValueChanger, gameValueChanger.RawAmount,
                                            gameValueChanger.ChangeDuration, gameValueChanger.Stackable,
                                            gameValueChanger.NonStackableLabel,
                                            gameValueChanger.CriticalChance, gameValueChanger.CriticalPercentage,
                                            gameValueChanger.ChangeInterval,
                                            gameValueChanger.AmountVariantPercentage));
                                        break;
                                    case GameValueChanger.ByIntervalChangeDurationType.Nondeterministic:
                                        StartCoroutine(ChangeHealthFixedAmountByInterval(gameValueChanger));
                                        break;
                                }
                                break;
                            case GameValueChanger.CurrentValueChangeType.CurrentPercentageByInterval:
                                switch (gameValueChanger.IntervalDurationType)
                                {
                                    case GameValueChanger.ByIntervalChangeDurationType.FixedTime:
                                        StartCoroutine(ChangeHealthCurrentPercentageByInterval(gameValueChanger, 
                                            gameValueChanger.RawAmount,
                                            gameValueChanger.ChangeDuration, gameValueChanger.Stackable,
                                            gameValueChanger.NonStackableLabel,
                                            gameValueChanger.CriticalChance, gameValueChanger.CriticalPercentage,
                                            gameValueChanger.ChangeInterval,
                                            gameValueChanger.AmountVariantPercentage));
                                        break;
                                    case GameValueChanger.ByIntervalChangeDurationType.Nondeterministic:
                                        StartCoroutine(ChangeHealthCurrentPercentagetByInterval(gameValueChanger));
                                        break;
                                }
                                break;
                            case GameValueChanger.CurrentValueChangeType.MaxPercentageByInterval:
                                switch (gameValueChanger.IntervalDurationType)
                                {
                                    case GameValueChanger.ByIntervalChangeDurationType.FixedTime:
                                        StartCoroutine(ChangeHealthMaxPercentageByInterval(gameValueChanger, 
                                            gameValueChanger.RawAmount,
                                            gameValueChanger.ChangeDuration, gameValueChanger.Stackable,
                                            gameValueChanger.NonStackableLabel,
                                            gameValueChanger.CriticalChance, gameValueChanger.CriticalPercentage,
                                            gameValueChanger.ChangeInterval,
                                            gameValueChanger.AmountVariantPercentage));
                                        break;
                                    case GameValueChanger.ByIntervalChangeDurationType.Nondeterministic:
                                        StartCoroutine(ChangeHealthMaxPercentageByInterval(gameValueChanger));
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
                                StartCoroutine(TempChangeMin(gameValueChanger, gameValueChanger.Amount));
                                break;
                            case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                StartCoroutine(TempChangeMin(gameValueChanger.Amount, gameValueChanger.ChangeDuration));
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
                                StartCoroutine(TempChangeMax(gameValueChanger, gameValueChanger.Amount));
                                break;
                            case GameValueChanger.OneTimeChangeDurationType.TempFixedTime:
                                StartCoroutine(TempChangeMax(gameValueChanger.Amount, gameValueChanger.ChangeDuration));
                                break;
                        }
                        break;
            }
        }

        public void UnchangeGameValue(GameValueChanger gameValueChanger)
        {
            if (!_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger does not exist");
                return;
            }
            _valueTempChangeValueCache.Remove(gameValueChanger);
        }

        private IEnumerator TempChangeCurrentValue(GameValueChanger gameValueChanger, float amount, float time, bool crited)
        {
            amount = GetEmphasizedChange(amount);
            Value += amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, crited);
            yield return new WaitForSeconds(time);
            Value -= amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, -amount, crited);
        }

        private IEnumerator TempChangeMax(float amount, float time)
        {
            Max += amount;
            yield return new WaitForSeconds(time);
            Max -= amount;
        }

        private IEnumerator TempChangeMin(float amount, float time)
        {
            Min += amount;
            yield return new WaitForSeconds(time);
            Min -= amount;
        }

        private IEnumerator TempChangeMin(GameValueChanger gameValueChanger, float amount)
        {
            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger already exists");
                yield break;
            }
            Min += amount;
            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield return new WaitForSeconds(0.1f);
            }
            Min -= amount;
        }

        private IEnumerator TempChangeMax(GameValueChanger gameValueChanger, float amount)
        {
            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger already exists");
                yield break;
            }
            Max += amount;
            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield return new WaitForSeconds(0.1f);
            }
            Max -= amount;
        }

        private IEnumerator TempChangeCurrentValue(GameValueChanger gameValueChanger, float amount, bool crited)
        {
            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger already exists");
                yield break;
            }
            amount = GetEmphasizedChange(amount);
            Value += amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, amount, crited);
            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                yield return new WaitForSeconds(0.1f);
            }
            Value -= amount;
            TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, -amount, crited);
        }

        private IEnumerator ChangeHealthFixedAmountByInterval(GameValueChanger gameValueChanger, float amount, float duration, bool stackable, GameValueChanger.NonStackableType nonStackableType,
            float critChance, float critPercentage, float interval, float variantPercentage)
        {
            if (!stackable && _nonStackableTypeCache.Contains(nonStackableType))
            {
                Debug.Log("Change not stackable");
                yield break;
            }

            _nonStackableTypeCache.Add(nonStackableType);

            while (duration >= 0)
            {
                bool crited = RollCrit(critChance);
                float changedAmount = GetCriticalAmount(amount, crited, critPercentage);
                changedAmount = changedAmount + Random.Range(-changedAmount * variantPercentage, changedAmount * variantPercentage);
                changedAmount = GetEmphasizedChange(changedAmount);
                Value += changedAmount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, changedAmount, crited);
                yield return new WaitForSeconds(interval);
                duration -= interval;
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        private IEnumerator ChangeHealthFixedAmountByInterval(GameValueChanger gameValueChanger)
        {
            if (!gameValueChanger.Stackable && _nonStackableTypeCache.Contains(gameValueChanger.NonStackableLabel))
            {
                Debug.Log("Change not stackable");
                yield break;
            }

            _nonStackableTypeCache.Add(gameValueChanger.NonStackableLabel);

            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger already exists");
                yield break;
            }

            GameValueChanger.NonStackableType nonStackableType = gameValueChanger.NonStackableLabel;

            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                float changedAmount = gameValueChanger.Amount;
                changedAmount = GetEmphasizedChange(changedAmount);
                Value += changedAmount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, changedAmount, gameValueChanger.LastAmountCrited);
                yield return new WaitForSeconds(gameValueChanger.ChangeInterval);
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        private IEnumerator ChangeHealthCurrentPercentageByInterval(GameValueChanger gameValueChanger, float percentage, float duration, bool stackable, GameValueChanger.NonStackableType nonStackableType,
            float critChance, float critPercentage, float interval, float variantPercentage)
        {
            if (!stackable && _nonStackableTypeCache.Contains(nonStackableType))
            {
                Debug.Log("Change not stackable");
                yield break;
            }

            _nonStackableTypeCache.Add(nonStackableType);

            while (duration >= 0)
            {
                bool crited = RollCrit(critChance);
                float changedAmount = Value * GetCriticalAmount(percentage, crited, critPercentage);
                changedAmount = changedAmount + Random.Range(-changedAmount * variantPercentage, changedAmount * variantPercentage);
                changedAmount = GetEmphasizedChange(changedAmount);
                Value += changedAmount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, changedAmount, crited);
                yield return new WaitForSeconds(interval);
                duration -= interval;
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        private IEnumerator ChangeHealthCurrentPercentagetByInterval(GameValueChanger gameValueChanger)
        {
            if (!gameValueChanger.Stackable && _nonStackableTypeCache.Contains(gameValueChanger.NonStackableLabel))
            {
                Debug.Log("Change not stackable");
                yield break;
            }

            _nonStackableTypeCache.Add(gameValueChanger.NonStackableLabel);

            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger already exists");
                yield break;
            }

            GameValueChanger.NonStackableType nonStackableType = gameValueChanger.NonStackableLabel;

            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger) )
            {
                float changedAmount = Value * gameValueChanger.Amount;
                changedAmount = GetEmphasizedChange(changedAmount);
                Value += changedAmount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, changedAmount, gameValueChanger.LastAmountCrited);
                yield return new WaitForSeconds(gameValueChanger.ChangeInterval);
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        private IEnumerator ChangeHealthMaxPercentageByInterval(GameValueChanger gameValueChanger, float percentage, float duration, bool stackable, GameValueChanger.NonStackableType nonStackableType,
            float critChance, float critPercentage, float interval, float variantPercentage)
        {
            if (!stackable && _nonStackableTypeCache.Contains(nonStackableType))
            {
                Debug.Log("Change not stackable");
                yield break;
            }

            _nonStackableTypeCache.Add(nonStackableType);

            while (duration >= 0)
            {
                bool crited = RollCrit(critChance);
                float changedAmount = Max * GetCriticalAmount(percentage, crited, critPercentage);
                changedAmount = changedAmount + Random.Range(-changedAmount * variantPercentage, changedAmount * variantPercentage);
                changedAmount = GetEmphasizedChange(changedAmount);
                Value += changedAmount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, changedAmount, crited);
                yield return new WaitForSeconds(interval);
                duration -= interval;
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        private IEnumerator ChangeHealthMaxPercentageByInterval(GameValueChanger gameValueChanger)
        {
            if (!gameValueChanger.Stackable && _nonStackableTypeCache.Contains(gameValueChanger.NonStackableLabel))
            {
                Debug.Log("Change not stackable");
                yield break;
            }

            _nonStackableTypeCache.Add(gameValueChanger.NonStackableLabel);

            if (_valueTempChangeValueCache.Contains(gameValueChanger))
            {
                Debug.Log("GameValueChanger already exists");
                yield break;
            }

            GameValueChanger.NonStackableType nonStackableType = gameValueChanger.NonStackableLabel;

            _valueTempChangeValueCache.Add(gameValueChanger);
            while (gameValueChanger != null && _valueTempChangeValueCache.Contains(gameValueChanger))
            {
                float changedAmount = Max * gameValueChanger.Amount;
                changedAmount = GetEmphasizedChange(changedAmount);
                Value += changedAmount;
                TriggerGameScriptEvent(GameScriptEvent.OnGameValueCurrentValueChanged, this, gameValueChanger, changedAmount, gameValueChanger.LastAmountCrited);
                yield return new WaitForSeconds(gameValueChanger.ChangeInterval);
            }
            _nonStackableTypeCache.Remove(nonStackableType);
        }

        private bool RollCrit(float chance)
        {
            return UtilityFunctions.RollChance(chance);
        }

        private float GetCriticalAmount(float amount, bool crit, float percentage)
        {
            return amount * (crit ? percentage : 1.0f);
        }

        public override void EditorUpdate()
        {
            InitialMinValue = Mathf.Clamp(InitialMinValue, float.MinValue, InitialMaxValue);
            InitialMaxValue = Mathf.Clamp(InitialMaxValue, InitialMinValue, float.MaxValue);
            InitialValue = Mathf.Clamp(InitialValue, InitialMinValue, InitialMaxValue);
        }
    }
}
