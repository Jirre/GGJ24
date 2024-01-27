using JvLib.Services;

namespace Project.StateMachines
{
    public partial class GameplayState : GameState
    {
        protected override void OnExit(GameStates nextState)
        {
            base.OnExit(nextState);
            Svc.Input.SetUIActionMap();
        }
    }
}
