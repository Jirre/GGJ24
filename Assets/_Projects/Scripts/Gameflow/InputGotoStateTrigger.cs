using JvLib.Services;
using Project.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.GameFlow
{
    public class InputGotoStateTrigger : MonoBehaviour
    {
        [SerializeField] private InputActionReference _InputAction;
        [SerializeField] private GameStates _State;

        private void OnEnable()
        {
            _InputAction.action.AddListeners(Trigger);
        }

        private void OnDisable()
        {
            _InputAction.action.RemoveListeners(Trigger);
        }

        private void Trigger(InputAction.CallbackContext pContext)
        {
            Svc.GameStateMachine.TransitionTo(_State);
        }
    }
}