using JvLib.Services;
using JvLib.Windows;

namespace Project.StateMachines
{
    public partial class MainMenuState : GameState
    {
        protected override void OnEnter(GameStates pPrevious)
        {
            base.OnEnter(pPrevious);
            Svc.Window.Open(WindowID.MainMenu);
            Svc.Input.SetUIActionMap();
        }

        protected override void OnExit(GameStates nextState)
        {
            base.OnExit(nextState);
            Svc.Window.Close(WindowID.MainMenu);
        }
    }
}
