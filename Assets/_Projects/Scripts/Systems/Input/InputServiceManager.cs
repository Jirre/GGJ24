using JvLib.Services;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Project.Systems.Input
{
    [ServiceInterface(Name = "Input"), 
     RequireComponent(
         typeof(PlayerInputManager))]
    public class InputServiceManager : MonoBehaviour, IService
    {
        [SerializeField] private PlayerInput[] _Inputs;
        private PlayerInputData _PlayerOne;
        private PlayerInputData _PlayerTwo;

        [SerializeField] private InputActionReference _CheckRightAction;
        public bool IsServiceReady { get; private set; }
        
        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);

            foreach (PlayerInput input in _Inputs)
            {
                if (input.actions[_CheckRightAction.name].ReadValue<float>() > 0)
                    _PlayerTwo = new PlayerInputData(input);
                else _PlayerOne = new PlayerInputData(input);
            }
            IsServiceReady = true;
            ServiceLocator.Instance.ReportInstanceReady(this);
        }

        public PlayerInputData FindPlayer(int pIndex)
        {
            return pIndex == 0 ? _PlayerOne : _PlayerTwo;
        }
    }

    public class PlayerInputData
    {
        public PlayerInput Input { get; private set; }
        public InputSystemUIInputModule UIInputModule { get; private set; }
        public EventSystem Events { get; private set; }

        public PlayerInputData(PlayerInput pInput)
        {
            Input = pInput;
            UIInputModule = Input.GetComponent<InputSystemUIInputModule>();
            Events = Input.GetComponent<EventSystem>();
        }
    }
}
