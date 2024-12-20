using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
namespace Interactables
{
    public class ActiveInvViewController : MonoBehaviour
    {
        [Header("Toggle Buttons for Active Inv")]
        [SerializeField] Button gewGawBtn;
        [SerializeField] Button potionBtn;
        [SerializeField] Button armorBtn;
        [SerializeField] Button weaponBtn;

        ActiveIntBtnPtrEvents gewgawBtnPtrEvents; 
        ActiveIntBtnPtrEvents potionBtnPtrEvents;   
        ActiveIntBtnPtrEvents armorBtnPtrEvents;
        ActiveIntBtnPtrEvents weaponBtnPtrEvents;


        [SerializeField] GameObject potionActivePanel;
        [SerializeField] GameObject gewgawsActivePanel;
        [SerializeField] GameObject weaponPanel;
        [SerializeField] GameObject armorPanel;
        [SerializeField] List<GameObject> panels = new List<GameObject>(); 


        void Start()
        {
            gewGawBtn.onClick.AddListener(OnGewgawBtnPressed);
            potionBtn.onClick.AddListener(OnPotionBtnPressed);
            armorBtn.onClick.AddListener(OnArmorBtnPressed);
            weaponBtn.onClick.AddListener(OnWeaponBtnPressed);
            panels = new List<GameObject>() 
                    { potionActivePanel, gewgawsActivePanel, weaponPanel, armorPanel };
            
            gewgawBtnPtrEvents = gewGawBtn.GetComponent<ActiveIntBtnPtrEvents>();   
            potionBtnPtrEvents = potionBtn.GetComponent<ActiveIntBtnPtrEvents>();
            armorBtnPtrEvents = armorBtn.GetComponent<ActiveIntBtnPtrEvents>(); 
            weaponBtnPtrEvents = weaponBtn.GetComponent<ActiveIntBtnPtrEvents>();
        }

        void OnPotionBtnPressed()
        {
            potionBtnPtrEvents.isClicked = !potionBtnPtrEvents.isClicked;
            if ( potionActivePanel.activeInHierarchy == true)
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(potionActivePanel.gameObject, false);
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(potionActivePanel.gameObject, true);                
                ToggleBtn(PanelNameActInv.PotionPanel);               
            }
        }
        void OnGewgawBtnPressed()
        {
            gewgawBtnPtrEvents.isClicked = !gewgawBtnPtrEvents.isClicked;       

            if (gewgawsActivePanel.activeInHierarchy == true)
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(gewgawsActivePanel.gameObject, false);
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(gewgawsActivePanel.gameObject, true);
                ToggleBtn(PanelNameActInv.gewgawPanel);          
            }
        }

        void OnArmorBtnPressed()
        {
            armorBtnPtrEvents.isClicked = !armorBtnPtrEvents.isClicked;
            if (armorPanel.activeInHierarchy == true)
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(armorPanel.gameObject, false);             
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(armorPanel.gameObject, true);
                ToggleBtn(PanelNameActInv.ArmorPanel);              
            }
        }
        void OnWeaponBtnPressed()
        {
            weaponBtnPtrEvents.isClicked = !weaponBtnPtrEvents.isClicked;
            if (weaponPanel.activeInHierarchy == true)
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(weaponPanel.gameObject, false);              
            }
            else
            {
                UIControlServiceGeneral.Instance.TogglePanelOnInGrp(weaponPanel.gameObject, true);
                ToggleBtn(PanelNameActInv.weaponPanel);
            }
        }
        void ToggleBtn(PanelNameActInv panelName)
        {
            foreach (Transform child in gewGawBtn.gameObject.transform.parent)
            {
                if(child.GetSiblingIndex() == (int)panelName)
                {
                    child.GetComponent<ActiveIntBtnPtrEvents>().ClickState();
                }
                else
                {
                    child.GetComponent<ActiveIntBtnPtrEvents>().UnClickState();
                }
            }
        }
    
  

        void TogglePanel(GameObject PanelOn)
        {
            Transform parent = PanelOn.transform.parent;
            int sibIndex = PanelOn.transform.GetSiblingIndex();

            if(sibIndex < 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject panel = parent.GetChild(i).gameObject; 
                    if (panel.name != PanelOn.name)
                    {
                        panel.SetActive(false);
                        panel.GetComponent<CanvasGroup>().interactable = false;
                    }
                    else
                    {
                        panel.SetActive(true);                
                        panel.GetComponent<CanvasGroup>().interactable = true;
                        panel.GetComponent<IPanel>().Load();
                    }
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject panel = parent.GetChild(i).gameObject; // should not make panel disappear                                       
                    panel.GetComponent<CanvasGroup>().interactable = false;                    
                }
                for (int i = 2; i < 4; i++)
                {
                    GameObject panel = parent.GetChild(i).gameObject;
                    if (panel.name != PanelOn.name)
                    {
                        panel.SetActive(false);
                        panel.GetComponent<CanvasGroup>().interactable = false;
                    }
                    else
                    {
                        panel.SetActive(true);                 
                        panel.GetComponent<CanvasGroup>().interactable = true;
                        panel.GetComponent<IPanel>().Load();
                    }
                }
            }

        }
        public enum PanelNameActInv
        {
            
            gewgawPanel, 
            PotionPanel,
            ArmorPanel,
            weaponPanel, 

        }
      
    }
}
