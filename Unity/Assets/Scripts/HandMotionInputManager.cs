using System.Linq;
using Leap;
using UnityEngine;

namespace Assets.Scripts
{
    public class HandMotionInputManager : MonoBehaviour
    {
        public float HandSize { get; set; }

        public GameObject Cube1;
        public GameObject Cube2;
        public GameObject Follower;

        private Controller _controller;
        private ContollerListerner _contollerListerner;

        private static HandMotionInputManager _instance;
        public static HandMotionInputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<HandMotionInputManager>();
                }
                return _instance;
            }
        }

        public class HandMotionInput
        {
            public enum HandMotionInputGesture
            {
                None,
                HoldTightDownwards,
                FlatDownwards,
                FlatFowards
            }

            private const float UpdateValidTime = 0.5f;

            public float CurrentTime { get; set; }
            public HandMotionInputGesture Gesture { get; set; }
            public Vector3 HandVelocity { get; private set; }
            public Vector3 HandPosition { get; private set; }
            public int Id { get; private set; }
            public bool IsIdValid { get; set; }
            public bool IsValid
            {
                get
                {
                    return (Time.time - _lastUpdateTime < UpdateValidTime) &&
                                IsIdValid; }
            }
            private float _lastUpdateTime;

            public HandMotionInput()
            {
                Gesture = HandMotionInputGesture.None;
                HandVelocity = Vector3.zero;
                HandPosition = Vector3.zero;
                IsIdValid = false;
                _lastUpdateTime = 0f;
                Id = 0;
            }

            public void UpdateInput(Hand hand)
            {
                _lastUpdateTime = CurrentTime;
                Id = hand.Id;
                HandVelocity = hand.PalmVelocity.ToUnity();
                HandPosition = hand.PalmPosition.ToUnity();
                UpdateGesture(hand);
            }

            private void UpdateGesture(Hand hand)
            {
                if (InHoldTightDownwardsGesture(hand))
                {
                    Gesture = HandMotionInputGesture.HoldTightDownwards;
                    return;
                }
                if (InFlatDownwardsGesture(hand))
                {
                    Gesture = HandMotionInputGesture.FlatDownwards;
                    return;
                }
                if (InFlatFowardsGesture(hand))
                {
                    Gesture = HandMotionInputGesture.FlatFowards;
                    return;
                }
                Gesture = HandMotionInputGesture.None;

            }

            private bool InHoldTightDownwardsGesture(Hand hand)
            {
                return PalmFaceTowardsDirection(hand, Vector3.down) && HandHoldTight(hand);
            }

            private bool InFlatDownwardsGesture(Hand hand)
            {
                return PalmFaceTowardsDirection(hand, Vector3.down) && FingersPointTowards(hand, Vector3.forward);
            }

            private bool InFlatFowardsGesture(Hand hand)
            {
                return PalmFaceTowardsDirection(hand, Vector3.forward) && FingersPointTowards(hand, Vector3.up);
            }

            private bool PalmFaceTowardsDirection(Hand hand, Vector3 direction)
            {
                const float palmDirectionValidAngle = 50f;

                return Vector3.Angle(direction, hand.PalmNormal.ToUnity()) <= palmDirectionValidAngle;
            }

            private bool HandHoldTight(Hand hand)
            {
                const float fingerToPalmValidDistance = 80.0f;
                const float interProxBoneValidAngle = 20f;
                const float disInterBoneValidAngle = 10f;

                float totalDistanceFromPalm = hand.Fingers.Where(finger => (finger.Type() != Finger.FingerType.TYPE_THUMB) && 
                    (finger.Type() != Finger.FingerType.TYPE_PINKY))
                    .Sum(finger => (hand.PalmPosition.DistanceTo(finger.TipPosition)));
                float interProxBoneDifferceAngleSum = hand.Fingers.Where(finger => (finger.Type() != Finger.FingerType.TYPE_THUMB) && 
                    (finger.Type() != Finger.FingerType.TYPE_PINKY)).
                    Sum(finger => finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction.AngleTo(finger.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction));
                float disInterBoneDiffereceAngleSum = hand.Fingers.Where(finger => (finger.Type() != Finger.FingerType.TYPE_THUMB) && 
                    (finger.Type() != Finger.FingerType.TYPE_PINKY)).
                    Sum(finger => finger.Bone(Bone.BoneType.TYPE_DISTAL).Direction.AngleTo(finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Direction));
                interProxBoneDifferceAngleSum = interProxBoneDifferceAngleSum * Mathf.Rad2Deg;
                disInterBoneDiffereceAngleSum = disInterBoneDiffereceAngleSum * Mathf.Rad2Deg;

                return ((totalDistanceFromPalm / 3.0f) <= fingerToPalmValidDistance) && 
                        ((interProxBoneDifferceAngleSum / 3.0f) >= interProxBoneValidAngle) &&
                        ((disInterBoneDiffereceAngleSum / 3.0f) >= disInterBoneValidAngle);
            }

            private bool FingersPointTowards(Hand hand, Vector3 direction)
            {
                const float directionValidAngle = 25f;

                float fingerPointAngleDifferenceSum =
                    hand.Fingers.Where(
                        finger =>
                            (finger.Type() != Finger.FingerType.TYPE_THUMB) &&
                            (finger.Type() != Finger.FingerType.TYPE_PINKY)).
                            Sum(finger => Vector3.Angle(finger.Direction.ToUnity(), direction));

                return (fingerPointAngleDifferenceSum/3.0f) <= directionValidAngle;
            }
        }

        private class ContollerListerner : Listener
        {
            public float CurrentTime { get; set; }

            public HandMotionInput Input1 { get; private set; }
            public HandMotionInput Input2 { get; private set; }

            public ContollerListerner() : base()
            {
                Input1 = new HandMotionInput();
                Input2 = new HandMotionInput();
            }

            public override void OnFrame(Controller controller)
            {
                Input1.CurrentTime = CurrentTime;
                Input2.CurrentTime = CurrentTime;

                Frame frame = controller.Frame();
                if (!frame.IsValid)
                {
                    return;
                }
                Hand hand1 = frame.Hand(Input1.Id);
                Hand hand2 = frame.Hand(Input2.Id);

                if (!hand1.IsValid)
                {
                    hand1 = frame.Hands.FirstOrDefault(h => ((h.Id != hand2.Id) || !hand2.IsValid) && h.IsValid);
                    Input1.IsIdValid = hand1 != null;
                }
                if (!hand2.IsValid)
                {
                    hand2 = frame.Hands.FirstOrDefault(h => ((hand1 == null) || (h.Id != hand1.Id) || !hand1.IsValid) && h.IsValid);
                    Input2.IsIdValid = hand2 != null;
                }

                if (Input1.IsIdValid)
                {
                    Input1.UpdateInput(hand1);
                }
                if (Input2.IsIdValid)
                {
                    Input2.UpdateInput(hand2);
                }
            }
        }


        void Awake()
        {
            _controller = new Controller();
            _contollerListerner = new ContollerListerner();
            _controller.AddListener(_contollerListerner);
        }

        void Update()
        {
            _contollerListerner.CurrentTime = Time.time;
            if (_contollerListerner.Input1.IsValid)
            {
                Cube1.transform.position = _contollerListerner.Input1.HandPosition + Follower.transform.position;
            }
            Debug.Log("1: " + _contollerListerner.Input1.IsValid + "  " + _contollerListerner.Input1.Gesture);
        }
    }
}
