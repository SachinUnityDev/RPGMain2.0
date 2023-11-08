using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Common
{
    public class SettingViewController : MonoBehaviour, IPanel
    {

        [Header("Corner Control buttons")]
        [SerializeField] Button DisplayBtn;
        [SerializeField] Button GamePlayBtn;
        [SerializeField] Button SoundBtn;
        [SerializeField] Button ControlBtn;

        [SerializeField] GameObject DisplayPanel;
        [SerializeField] GameObject GameplayPanel;
        [SerializeField] GameObject SoundPanel;
        [SerializeField] GameObject ControlPanel;

        List<GameObject> allPanels = new List<GameObject>(); 
        void Start()
        {
            allPanels = new List<GameObject>() { DisplayPanel, GameplayPanel, SoundPanel, ControlPanel };

            DisplayBtn.onClick.AddListener(OnDisplayBtnPressed);
            GamePlayBtn.onClick.AddListener(OnGamePlayBtnPressed);
            SoundBtn.onClick.AddListener(SoundBtnPressed);
            ControlBtn.onClick.AddListener(ControlBtnPressed);
        }

        void OnDisplayBtnPressed()
        {
            TogglePanel(DisplayPanel); 
        }
        void OnGamePlayBtnPressed()
        {
            TogglePanel(GameplayPanel);
        }
        void SoundBtnPressed()
        {
            TogglePanel(SoundPanel);

        }
        void ControlBtnPressed()
        {
            TogglePanel(ControlPanel);
            ControlPanel.GetComponent<KeyBindingsController>().SetDefaultKeys();

        }

        void TogglePanel(GameObject panelOn)
        {
            for (int i = 0; i < allPanels.Count; i++)
            {
                CanvasGroup canvasGroup = allPanels[i].GetComponent<CanvasGroup>();

                if (allPanels[i].name != panelOn.name)
                {
                    allPanels[i].SetActive(false);
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                }
                else
                {
                    allPanels[i].SetActive(true);
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true; 
                }
            }
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

