using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Common
{
    public class SettingService : MonoSingletonGeneric<SettingService>
    {
        public KeyBindingSO keyBindingSO; 
        [SerializeField] GameObject settingsPanel;
        public GameState gameStateInScroll; 
        private void Start()
        {
            
        }


        public void OpenSettingPanel()
        {
            UIControlServiceGeneral.Instance.TogglePanel(settingsPanel, true); 
        }

        public void CloseSettingsPanel()
        {
            UIControlServiceGeneral.Instance.TogglePanel(settingsPanel, false);

        }
    }
}

