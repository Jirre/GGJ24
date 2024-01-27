using JvLib.Services;
using JvLib.Windows;
using Project.Gameplay;
using Project.StateMachines;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.UI.Windows
{
    public class MainMenuWindow : Window, IOnWindowShow, IOnWindowHide
    {
        [SerializeField] private InputActionReference _InputAction;
        [SerializeField] private GameStates _State;
        
        [SerializeField] private TMP_Text[] _DailyHighscores;
        [SerializeField] private TMP_Text[] _AllTimeHighscores;

        private const string EMPTY_SCORE_ENTRY = "... - ";
        
        public void OnWindowShow(object context)
        {
            Svc.Gameplay.RefreshHighscores();
            for (int i = 0; i < _DailyHighscores.Length; i++)
            {
                _DailyHighscores[i].SetText(Svc.Gameplay.DailyHighscores.Count <= i
                    ? $"{(i + 1).ToString()}. {EMPTY_SCORE_ENTRY}"
                    : ParseHighscoreToString(i, Svc.Gameplay.DailyHighscores[i]));
            }
            
            for (int i = 0; i < _AllTimeHighscores.Length; i++)
            {
                _AllTimeHighscores[i].SetText(Svc.Gameplay.AllTimeHighscores.Count <= i
                    ? $"{(i + 1).ToString()}. {EMPTY_SCORE_ENTRY}"
                    : ParseHighscoreToString(i + 1, Svc.Gameplay.AllTimeHighscores[i]));
            }
            
            _InputAction.action.AddListeners(Trigger);
        }
        
        public void OnWindowHide()
        {
            _InputAction.action.RemoveListeners(Trigger);
        }

        private static string ParseHighscoreToString(int pPlace, HighscoreEntry pEntry) =>
            $"{pPlace.ToString()}. {pEntry.Name} - {pEntry.Score.ToString()}";

        private void Trigger(InputAction.CallbackContext pContext)
        {
            Svc.GameStateMachine.TransitionTo(_State);
        }
    }
}