using Assets.Scripts.Attributes;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.GameScripts.GameLogic.GameValue
{
    [AddComponentMenu("Misc/GameValueChanger")]
    public class GameValueChanger : GameLogic
    {
        public class GameValueChangerData
        {
            public ChangeTargetValueType TargetValueType;
            public OneTimeChangeDurationType OneTimeDurationType;
            public ByIntervalChangeDurationType IntervalDurationType;
            public CurrentValueChangeType ChangeType;
            public bool Stackable;
            public NonStackableType NonStackableLabel;

            public float _amount;
            public float AmountVariantPercentage;
            public float Amount
            {
                get
                {
                    LastAmountCrited = UtilityFunctions.RollChance(CriticalChance);
                    float amount = _amount * (LastAmountCrited ? CriticalPercentage : 1.0f);
                    return amount + Random.Range(-amount * AmountVariantPercentage, amount * AmountVariantPercentage);
                }
            }

            public bool LastAmountCrited
            {
                get;
                private set;
            }

            public float RawAmount
            {
                get { return _amount; }
                set { _amount = value; }
            }

            public float ChangeDuration;
            public float ChangeInterval = 1.0f;

            public float CriticalChance = 0f;
            public float CriticalPercentage = 2.0f;
        }

        public GameValueChangerData CreateGameValueChangerData()
        {
            GameValueChangerData gameValueChanger = new GameValueChangerData
            {
                TargetValueType = TargetValueType,
                OneTimeDurationType = OneTimeDurationType,
                IntervalDurationType = IntervalDurationType,
                ChangeType = ChangeType,
                Stackable = Stackable,
                NonStackableLabel = NonStackableLabel,
                _amount = _amount,
                AmountVariantPercentage = AmountVariantPercentage,
                ChangeDuration = ChangeDuration,
                ChangeInterval = ChangeInterval,
                CriticalChance = CriticalChance,
                CriticalPercentage = CriticalPercentage
            };

            return gameValueChanger;
        }

        public enum NonStackableType
        {
            Haha
        }

        public enum ChangeTargetValueType
        {
            CurrentValue,
            Max,
            Min
        }

        public enum OneTimeChangeDurationType
        {
            Permanent,
            TempFixedTime,
            Nondeterministic
        }

        public enum ByIntervalChangeDurationType
        {
            FixedTime,
            Nondeterministic
        }

        public enum CurrentValueChangeType
        {
            FixedAmount,
            CurrentPercentage,
            MaxPercentage,
            FixedAmountByInterval,
            CurrentPercentageByInterval,
            MaxPercentageByInterval
        }

        public GameObject Owner;

        public ChangeTargetValueType TargetValueType;
        public OneTimeChangeDurationType OneTimeDurationType;
        public ByIntervalChangeDurationType IntervalDurationType;
        public CurrentValueChangeType ChangeType;
        public bool Stackable;
        public NonStackableType NonStackableLabel;

        public float _amount;
        public float AmountVariantPercentage;
        public float Amount
        {
            get
            {
                LastAmountCrited = UtilityFunctions.RollChance(CriticalChance);
                float amount = _amount * (LastAmountCrited ? CriticalPercentage : 1.0f);
                return amount + Random.Range(-amount * AmountVariantPercentage, amount * AmountVariantPercentage);
            }
        }

        public bool LastAmountCrited
        {
            get; 
            private set; 
        }

        public float RawAmount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public float ChangeDuration;
        public float ChangeInterval = 1.0f;

        public float CriticalChance = 0f;
        public float CriticalPercentage = 2.0f;

        public float InitialChangeDuration;
        public float InitialChangeInterval;
        public float InitialCriticalChance;
        public float InitialCriticalPercentage;
        public float InitialAmount;

        [GameScriptEvent(Constants.GameScriptEvent.UpdateGameValueChangerOwner)]
        public void UpdateGameValueChangerOwner(GameObject owner)
        {
            Owner = owner;
        }

        protected override void FirstTimeInitialize()
        {
            base.FirstTimeInitialize();
            if (Owner == null)
            {
                Owner = gameObject;
            }
            InitialChangeDuration = ChangeDuration;
            InitialChangeInterval = ChangeInterval;
            InitialCriticalChance = CriticalChance;
            InitialCriticalPercentage = CriticalPercentage;
            InitialAmount = _amount;
        }

        protected override void Initialize()
        {
            base.Initialize();
            ChangeDuration = InitialChangeDuration;
            ChangeInterval = InitialChangeInterval;
            CriticalChance = InitialCriticalChance;
            CriticalPercentage = InitialCriticalPercentage;
            _amount = InitialAmount;
        }

        protected override void Deinitialize()
        {
        }
    }
}