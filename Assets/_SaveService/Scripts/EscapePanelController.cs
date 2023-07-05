using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace Common
{
    public class EscapePanelController : MonoBehaviour, IPanel
    {
        [SerializeField] Button return2GameBtn;
        [SerializeField] Button settingsBtn;      
        [SerializeField] Button saveGameBtn;
        [SerializeField] Button loadGameBtn;
        [SerializeField] Button codexBtn;
        [SerializeField] Button exit2MainMenuBtn;


        

        void Start()
        {
            return2GameBtn.onClick.AddListener(OnReturn2GamePressed);
            settingsBtn.onClick.AddListener(OnSettingsBtnPressed);
            codexBtn.onClick.AddListener(OnCodexBtnPressed);
            saveGameBtn.onClick.AddListener(OnSaveGameBtnPressed);
            loadGameBtn.onClick.AddListener(OnLoadGameBtnPressed);
            exit2MainMenuBtn.onClick.AddListener(OnExit2MainMenuPressed);

        }

        void OnReturn2GamePressed()
        {
            UnLoad(); 
        }
        void OnSettingsBtnPressed()
        {
            UnLoad(); 
            SettingService.Instance.OpenSettingPanel(); 
        }
        void OnCodexBtnPressed()
        {
            CodexService.Instance.OpenCodexPanel();
        }

        void OnSaveGameBtnPressed()
        {            
            SaveService.Instance.ShowSavePanel();
            UnLoad(); 
        }
        void OnLoadGameBtnPressed()
        {
            SaveService.Instance.ShowLoadPanel();
            UnLoad();
            
        }

        void OnExit2MainMenuPressed()
        {
            
            // go to intro scene and Main menu 
            // use a coroutine to trigger unload 
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, true);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(this.gameObject, false);
        }

        public void Init()
        {
           
        }
    }
}

