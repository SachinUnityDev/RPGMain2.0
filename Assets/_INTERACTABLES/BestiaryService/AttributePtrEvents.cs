using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;
using UnityEngine.EventSystems; 

namespace Interactables
{
    public class AttributePtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] AttribName statName;
        [SerializeField] bool isOnLeft = false; 

        [SerializeField] GameObject desc; 
        [SerializeField] AttribData statData;
        AttributeViewController attributeViewController; 
        void Start()
        {
            desc = transform.GetChild(3).gameObject;
            desc.SetActive(false);
            attributeViewController = 
                    transform.GetComponentInParent<AttributeViewController>();     

        }

        public void PopulateData()
        {
            
            CharModel charModel = 
                BestiaryService.Instance.bestiaryViewController.currBeastOnDisplay;  
         
                transform.GetChild(2).GetComponent<TextMeshProUGUI>().text
                                               = GetStatValue(charModel);
        }

        string GetStatValue(CharModel charModel)
        {
            string str = "";
            if(charModel == null)
            {
                return str;
            }
            else
            {
                statData = charModel.statsList.Find(t => t.AttribName == statName);
                PopulateDesc();
                if (statName == AttribName.armor || statName == AttribName.damage)
                {

                    str = statData.minRange + "-" + statData.maxRange;
                }
                else
                {
                    str = statData.currValue.ToString();
                }
            }

            return str;
            
        }

        void PopulateDesc()
        {
            desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                            = statData.AttribName.ToString().CreateSpace();
            desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                            = statData.desc; 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            attributeViewController.ToggleLRPanel(isOnLeft); 
            desc.SetActive(true); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            desc.SetActive(false);
        }
    }



}
