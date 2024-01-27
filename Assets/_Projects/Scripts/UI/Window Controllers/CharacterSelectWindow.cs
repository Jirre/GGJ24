using System;
using System.Collections.Generic;
using System.Linq;
using JvLib.Pooling;
using JvLib.Services;
using JvLib.UI;
using JvLib.Windows;
using Project.Gameplay;
using Project.Systems.Input;
using UnityEngine;

namespace Project.UI.Windows
{
    public class CharacterSelectWindow : Window, IOnWindowShow, IOnWindowHide
    {
        [Serializable]
        public class UIElement
        {
            public AmmunitionConfig Config;
            public UIButton Button;
        }

        [SerializeField] private List<UIElement> _PlayerOneUI;
        [SerializeField] private List<UIElement> _PlayerTwoUI;
        
        public void OnWindowShow(object context)
        {
            SetSelection(0, _PlayerOneUI);
            SetSelection(1, _PlayerTwoUI);
        }
        
        public void OnWindowHide()
        {
            SaveSelection(0, _PlayerOneUI);
            SaveSelection(1, _PlayerTwoUI);
        }
        
        private void SetSelection(int pIndex, List<UIElement> pUI)
        {
            PlayerData data = Svc.Gameplay.GetPlayerData(pIndex);
            PlayerInputData input = Svc.Input.FindPlayer(pIndex);
            UIButton button = _PlayerOneUI.First(e => e.Config == data.Ammunition).Button;
            input.Events.SetSelectedGameObject(button.gameObject);
        }

        private void SaveSelection(int pIndex, List<UIElement> pUI)
        {
            PlayerInputData input = Svc.Input.FindPlayer(pIndex);
            AmmunitionConfig config = _PlayerOneUI.First(e => e.Button.gameObject == input.Events.currentSelectedGameObject).Config;
            Svc.Gameplay.GetPlayerData(pIndex).Ammunition = config;
        }
    }
}