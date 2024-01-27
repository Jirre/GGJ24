using System;
using System.Collections.Generic;
using System.Linq;
using JvLib.Services;
using JvLib.UI;
using JvLib.Windows;
using Project.Gameplay;
using Project.StateMachines;
using Project.Systems.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Project.UI.Windows
{
    public class CharacterSelectWindow : Window, IOnWindowShow, IOnWindowHide
    {
        [Serializable]
        public class UIElement
        {
            public AmmunitionConfig Config;
            public UIButton Button;
            public Image Image;
        }

        [SerializeField] private InputActionReference _SubmitAction;
        [SerializeField] private InputActionReference _CancelAction;

        [SerializeField] private GameObject _PlayerOneRoot;
        [SerializeField] private GameObject _PlayerTwoRoot;

        [SerializeField] private List<UIElement> _PlayerOneUI;
        [SerializeField] private List<UIElement> _PlayerTwoUI;

        [SerializeField] private TMP_Text _PlayerOneTitle;
        [SerializeField] private TMP_Text _PlayerTwoTitle;
        private bool _PlayerOneReady;
        private bool _PlayerTwoReady;
        
        public void OnWindowShow(object context)
        {
            SetSelection(0, _PlayerOneUI, _PlayerOneRoot);
            SetSelection(1, _PlayerTwoUI, _PlayerTwoRoot);

            AddListeners();

            _PlayerOneReady = false;
            _PlayerTwoReady = false;
        }
        
        public void OnWindowHide()
        {
            SaveSelection(0, _PlayerOneUI);
            SaveSelection(1, _PlayerTwoUI);
            
            RemoveListeners();
        }

        private void Update()
        {
            string oneTitle = _PlayerOneReady ? "Ready!" : "Waiting";
            if (!_PlayerOneReady)
            {
                for (float i = 0; i < Time.time % 3f; i++)
                {
                    oneTitle += ".";
                }
            }
            _PlayerOneTitle.SetText(oneTitle);
            
            string twoTitle = _PlayerTwoReady ? "Ready!" : "Waiting";
            if (!_PlayerTwoReady)
            {
                for (float i = 0; i < Time.time % 3f; i++)
                {
                    twoTitle += ".";
                }
            }
            _PlayerTwoTitle.SetText(twoTitle);
            
            if (_PlayerOneReady && _PlayerTwoReady)
            {
                Svc.GameStateMachine.TransitionTo(GameStates.InitGame);
            }
        }

        private void AddListeners()
        {
            Svc.Input.FindPlayer(0).Input.actions[_SubmitAction.name].AddListeners(OnSubmitPlayerOne);
            Svc.Input.FindPlayer(0).Input.actions[_CancelAction.name].AddListeners(OnCancelPlayerOne);
            Svc.Input.FindPlayer(1).Input.actions[_SubmitAction.name].AddListeners(OnSubmitPlayerTwo);
            Svc.Input.FindPlayer(1).Input.actions[_CancelAction.name].AddListeners(OnCancelPlayerTwo);
        }

        private void RemoveListeners()
        {
            Svc.Input.FindPlayer(0).Input.actions[_SubmitAction.name].RemoveListeners(OnSubmitPlayerOne);
            Svc.Input.FindPlayer(0).Input.actions[_CancelAction.name].RemoveListeners(OnCancelPlayerOne);
            Svc.Input.FindPlayer(1).Input.actions[_SubmitAction.name].RemoveListeners(OnSubmitPlayerTwo);
            Svc.Input.FindPlayer(1).Input.actions[_CancelAction.name].RemoveListeners(OnCancelPlayerTwo);
        }

        private void OnSubmitPlayerOne(InputAction.CallbackContext pContext)
        {
            if (!pContext.started)
                return;
            Debug.Log("SUBMIT");
            _PlayerOneReady = true;
        }

        private void OnSubmitPlayerTwo(InputAction.CallbackContext pContext)
        {
            if (!pContext.started)
                return;
            Debug.Log("SUBMIT");
            _PlayerTwoReady = true;
        }

        private void OnCancelPlayerOne(InputAction.CallbackContext pContext)
        {
            if (!pContext.started)
                return;
            Debug.Log("CANCEL");
            
            if (_PlayerOneReady) _PlayerOneReady = false;
            else Svc.GameStateMachine.TransitionTo(GameStates.MainMenu);
        }
        private void OnCancelPlayerTwo(InputAction.CallbackContext pContext)
        {
            if (!pContext.started)
                return;
            Debug.Log("CANCEL");
            if (_PlayerTwoReady) _PlayerTwoReady = false;
            else Svc.GameStateMachine.TransitionTo(GameStates.MainMenu);
        }

        private void SetSelection(int pIndex, List<UIElement> pUI, GameObject pRoot)
        {
            PlayerData data = Svc.Gameplay.GetPlayerData(pIndex);
            PlayerInputData input = Svc.Input.FindPlayer(pIndex);
            input.Events.playerRoot = pRoot;

            foreach (UIElement e in pUI)
            {
                e.Image.sprite = e.Config.UIImage;
                if (e.Config.Name == data.Ammunition.Name)
                {
                    Debug.Log(e.Config.Name);
                    input.Events.SetSelectedGameObject(e.Button.gameObject);
                }
            }
        }

        private static void SaveSelection(int pIndex, IEnumerable<UIElement> pUI)
        {
            PlayerInputData input = Svc.Input.FindPlayer(pIndex);
            UIElement element = pUI.First(e => e.Button.gameObject == input.Events.currentSelectedGameObject);
            Svc.Gameplay.GetPlayerData(pIndex).Ammunition = element.Config;
        }
    }
}