using JvLib.Services;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Systems.Input
{
    [ServiceInterface(Name = "Input"), 
     RequireComponent(
         typeof(PlayerInputManager))]
    public class InputServiceManager : MonoBehaviour, IService
    {
        [SerializeField] private PlayerInput[] _Inputs;
        private PlayerInput _PlayerOne;
        private PlayerInput _PlayerTwo;

        [SerializeField] private InputActionReference _CheckRightAction;
        public bool IsServiceReady { get; private set; }
        private PlayerInputManager _inputManager;
        private Gamepad _gamepad;

        private bool _IsPlayerRight;
        //private const string MAP_KEY = "Player";
        
        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
            ServiceLocator.Instance.Register(this);
        }

        private IEnumerator Start()
        {

            yield return new WaitForSeconds(0.5f);

            foreach (PlayerInput input in _Inputs)
            {

                ArcadeGamepad gamepad = input.devices.First() as ArcadeGamepad;
                if (input.actions[_CheckRightAction.name].ReadValue<float>() > 0)
                    _PlayerTwo = input;
                else _PlayerOne = input;
            }
            IsServiceReady = true;
            ServiceLocator.Instance.ReportInstanceReady(this);
        }

        public PlayerInput FindPlayer(int pIndex)
        {
            return pIndex == 0 ? _PlayerOne : _PlayerTwo;   
        }
        


    }
}
