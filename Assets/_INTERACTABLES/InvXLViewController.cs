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
        [Header("TBR")]
        [SerializeField] GameObject ToggleBtnParent;

        [SerializeField] GameObject bestiaryPanel;
        [SerializeField] GameObject skillPanel;
        [SerializeField] GameObject loreParentPanel;
        [SerializeField] GameObject invPanel;
        [SerializeField] GameObject BtmCharPanel; 


        [Header("NTBR")]
        [SerializeField] Button beastiaryBtn;
        [SerializeField] Button skillBtn;
        [SerializeField] Button loreBtn;
        [SerializeField] Button invBtn;

        [SerializeField] Button invXLClose;

        void OnEnable()
        {

            bestiaryPanel.SetActive(true);
            skillPanel.SetActive(true);
            loreParentPanel.SetActive(true);
            invPanel.SetActive(true);

            beastiaryBtn =  ToggleBtnParent.transform.GetChild(0).GetComponent<Button>();
            skillBtn = ToggleBtnParent.transform.GetChild(1).GetComponent<Button>();
            loreBtn = ToggleBtnParent.transform.GetChild(2).GetComponent<Button>();
            invBtn = ToggleBtnParent.transform.GetChild(3).GetComponent<Button>();

            beastiaryBtn.onClick.AddListener(OnBeastiaryPressed);
            skillBtn.onClick.AddListener(OnSkillBtnPressed);
            loreBtn.onClick.AddListener(OnLoreBtnPressed);
            invBtn.onClick.AddListener(OnInvBtnPressed);
            invXLClose.onClick.AddListener(UnLoad);
            
            LoreService.Instance.loreViewController = transform.GetComponentInChildren<LoreViewController>();         
        }

        
        public void Load()
        {    
            InvService.Instance.On_ToggleInvXLView(true);
        }

        public void UnLoad()
        {
            if (InvService.Instance.overLoadCount > 0)
                return;
            InvService.Instance.On_ToggleInvXLView(false);
            UIControlServiceGeneral.Instance.TogglePanel(gameObject, false);
            InvService.Instance.isInvPanelOpen= false;  
        }

        public void Init()
        {       
            UIControlServiceGeneral.Instance.TogglePanelOnInGrp(invPanel, true);

            bestiaryPanel.GetComponent<IPanel>().Init();
            skillPanel.GetComponent<IPanel>().Init();
            loreParentPanel.GetComponent<IPanel>().Init();
            BestiaryService.Instance.bestiaryViewController 
                        = bestiaryPanel.GetComponent<BestiaryViewController>(); 


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

        private void Update()
        {
           if(InvService.Instance.isInvPanelOpen) 
                if(Input.GetKeyDown(KeyCode.Escape))
            {
                UnLoad();
              //  Input.GetKeyDown(KeyCode.LeftControl)
            }     
        }

  
    }


}

