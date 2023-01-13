using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using System.Linq;
using System;
namespace Interactables
{
    public class InvXLViewController : MonoBehaviour, IPanel
    {

        public event Action<CharModel> OnCharSelected;

        [Header("To be ref")]
        [SerializeField] GameObject ToggleBtnParent;

        [SerializeField] GameObject bestiaryPanel;
        [SerializeField] GameObject skillPanel;
        [SerializeField] GameObject loreParentPanel;
        [SerializeField] GameObject invPanel;
        [SerializeField] GameObject BtmCharPanel; 


        [Header("Not to be ref")]
        [SerializeField] Button beastiaryBtn;
        [SerializeField] Button skillBtn;
        [SerializeField] Button loreBtn;
        [SerializeField] Button invBtn;

        [SerializeField] Button invXLClose;


        private void Awake()
        {
             bestiaryPanel.SetActive(true); 
             skillPanel.SetActive(true);
             loreParentPanel.SetActive(true);
             invPanel.SetActive(true);
        }
        void Start()
        {
            beastiaryBtn =  ToggleBtnParent.transform.GetChild(0).GetComponent<Button>();
            skillBtn = ToggleBtnParent.transform.GetChild(1).GetComponent<Button>();
            loreBtn = ToggleBtnParent.transform.GetChild(2).GetComponent<Button>();
            invBtn = ToggleBtnParent.transform.GetChild(3).GetComponent<Button>();

            beastiaryBtn.onClick.AddListener(OnBeastiaryPressed);
            skillBtn.onClick.AddListener(OnSkillBtnPressed);
            loreBtn.onClick.AddListener(OnLoreBtnPressed);
            invBtn.onClick.AddListener(OnInvBtnPressed);
            invXLClose.onClick.AddListener(UnLoad);
          //  Init();
        }

        
        public void Load()
        {
            UIControlServiceGeneral.Instance.TogglePanel(invPanel, true);

        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);

        }

        public void Init()
        {
            //start with the inv panel display
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(invPanel, true);

            // init all four panels and their subpanels here 
            bestiaryPanel.GetComponent<IPanel>().Init();
            skillPanel.GetComponent<IPanel>().Init();
            loreParentPanel.GetComponent<IPanel>().Init();
            foreach (IPanel panel in invPanel.GetComponentsInChildren<IPanel>())
            {
                panel.Init();
            }
        }
        void OnBeastiaryPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(bestiaryPanel, true);
        }
        void OnSkillBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(skillPanel, true);
        }
        void OnLoreBtnPressed()
        {            
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(loreParentPanel, true);
        }
        void OnInvBtnPressed()
        {
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(invPanel, true);
        }
    }


}

