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
        }

        void OnPotionBtnPressed()
        {          
            TogglePanel(potionActivePanel); 

        }
        void OnGewgawBtnPressed()
        {          
            TogglePanel(gewgawsActivePanel);             
        }
        
        void OnArmorBtnPressed()
        {          
            TogglePanel(armorPanel);
            ArmorService.Instance.OpenArmorPanel();
        }
        void OnWeaponBtnPressed()
        {           
            TogglePanel(weaponPanel);
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
                    }
                }
            }

        }
      
    }
}
