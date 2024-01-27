using JvLib.Services;
using UnityEngine.InputSystem;

namespace Project.StateMachines
{
    public partial class InitGameState : GameState
    {
        protected override void OnEnter(GameStates pPrevious)
        {
            base.OnEnter(pPrevious);
            Svc.Input.SetGameplayActionMap();
        }
    }
}
