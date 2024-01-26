using JvLib.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Systems.Input
{
    [ServiceInterface(Name = "Input"), 
     RequireComponent(
         typeof(PlayerInputManager),
         typeof(PlayerInput))]
    public class InputServiceManager : MonoBehaviour, IService
    {
        public bool IsServiceReady { get; private set; }
        private PlayerInputManager _inputManager;
        public PlayerInput PlayerInput { get; private set; }
        private Gamepad _gamepad;
        
        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
            PlayerInput = GetComponent<PlayerInput>();
            ServiceLocator.Instance.Register(this);
        }

        private void Start()
        {
            IsServiceReady = true;
            ServiceLocator.Instance.ReportInstanceReady(this);
        }

    }
}
