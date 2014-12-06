using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Attributes;
using Assets.Scripts.Constants;
using Assets.Scripts.GameScripts.GameLogic.Input;
using Assets.Scripts.Managers;
using UnityEngine;
using GameScriptEventAttribute = Assets.Scripts.Attributes.GameScriptEvent;

namespace Assets.Scripts.GameScripts.GameLogic.GUI
{
    public class ButtonController : GameLogic
    {
        public Prefab StoryLevelPrefab;
        public Prefab SurvivalLevelPrefab;
        public ClipName ButtonPressClip;
        [SerializeField]
        public List<Transform> ButtonCubes;

        public Color HighlightColor;
        public float BtnScaleAmount;
        private Vector3 _popoutAmount;
        private List<bool> _popped;
        private const float RotateAmount = 30.0f;
        private int _curButton;
        [SerializeField]
        private AxisOnHold AxisOnHold;
        [SerializeField]
        private ButtonOnPressed ButtonOnPressed;

        private bool _canClick;

        protected override void Initialize()
        {
            base.Initialize();
            _popoutAmount = new Vector3(0, 0, -.75f);
            _popped = new List<bool>();
            for (int index = 0; index < ButtonCubes.Count; index++)
                _popped.Add(false);
            _canClick = false;
            _curButton = 0;
        }

        [GameEvent(GameEvent.OnLevelStarted)]
        public void AllowClicks()
        {
            StartCoroutine(AllowClicksIE());
        }

        private IEnumerator AllowClicksIE()
        {
            yield return new WaitForSeconds(0.2f);
            _canClick = true;
            ButtonChange(0);
        }

        protected override void Update()
        {
            base.Update();
            RaycastHit hit;

            if (!_canClick)
                return;
            
            // Handle Joystick
            if (AxisOnHold.Detect())
                ButtonChange(GetNextButton(AxisOnHold.GetAxisValue() > 0.01f));

            // Handle Mouse
            bool clicked = false;
            if (Physics.Raycast(UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit,
                LayerConstants.LayerMask.StaticObstacle))
            {
                for (int index = 0; index < ButtonCubes.Count; index++)
                    if (hit.collider.gameObject == ButtonCubes[index].gameObject)
                        ButtonChange(index);
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    clicked = true;
            }

            // Handle Clicks
            if (ButtonOnPressed.Detect() || clicked)
            {
                switch (_curButton)
                {
                    case 0:
                        OnStartPressed();
                        break;
                    case 1:
                        OnSurvivalPressed();
                        break;
                    case 2:
                        OnQuitPressed();
                        break;
                }
            }
        }

        private int GetNextButton(bool up)
        {
            if (up)
                _curButton--;
            else
                _curButton++;
            return _curButton %= ButtonCubes.Count;
        }

        public void ButtonChange(int buttonId)
        {
            for (int i = 0; i < ButtonCubes.Count; i++)
                if (i == buttonId)
                    OnButtonMouseOver(i);
            _curButton = buttonId;
        }

        public void OnStartPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            OnButtonMouseLeave(_curButton);
            GameManager.Instance.ChangeLevel(StoryLevelPrefab);
        }

        public void OnSurvivalPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            OnButtonMouseLeave(_curButton);
            GameManager.Instance.ChangeLevel(SurvivalLevelPrefab);
        }

        public void OnQuitPressed()
        {
            AudioManager.Instance.PlayClipImmediate(ButtonPressClip);
            Application.Quit();
        }

        public void OnButtonMouseOver(int index)
        {
            if (_popped[index])
                return;
            GameObject hitObj = ButtonCubes[index].gameObject;
            _popped[index] = true;
            hitObj.GetComponentInChildren<ParticleSystem>().Play();
            hitObj.transform.Rotate(Vector3.up, RotateAmount);
            hitObj.transform.localScale *= BtnScaleAmount;
            hitObj.transform.Translate(_popoutAmount);
            hitObj.renderer.material.SetColor("_OutlineColor", HighlightColor);
            _curButton = index;
            for (int i = 0; i < ButtonCubes.Count; i++)
                if(i != index)
                    OnButtonMouseLeave(i);
        }

        public void OnButtonMouseLeave(int index)
        {
            if (!_popped[index])
                return;
            Transform hitObj = ButtonCubes[index];
            _popped[index] = false;
            hitObj.GetComponentInChildren<ParticleSystem>().Stop();
            hitObj.Translate(-_popoutAmount);
            hitObj.localScale /= BtnScaleAmount;
            hitObj.Rotate(Vector3.up, -RotateAmount);
            hitObj.renderer.material.SetColor("_OutlineColor", Color.black);
        }

        protected override void Deinitialize()
        {
        }
    }
}