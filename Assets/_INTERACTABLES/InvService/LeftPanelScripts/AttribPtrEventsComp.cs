using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;
using UnityEngine.EventSystems;

namespace Interactables
{
    public class AttribPtrEventsComp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] StatsName statName;
        [SerializeField] bool isOnLeft = false;

        [SerializeField] GameObject desc;
        [SerializeField] StatData statData;
        AttribPanelViewComp attribPanelViewComp;
        BtmCharViewController btmCharViewController; 

        void Start()
        {
            desc = transform.GetChild(3).gameObject;
            desc.SetActive(false);
            //attribPanelViewComp =
            //        transform.GetComponentInParent<AttribPanelViewComp>();

            //btmCharViewController =
            //    attribPanelViewComp.transform.parent.parent
            //                    .GetChild(2).GetComponent<BtmCharViewController>();

            InvService.Instance.OnCharSelectInPanel += PopulateData;


            // init on town init 
            CharService.Instance.allCharsInParty.ForEach(t => t.OnStatCurrValSet
             += (CharModData charModData) => PopulateData(CharService.Instance.GetCharCtrlWithCharID
             (charModData.effectedCharNameID).charModel)); 
            
                
        }

        public void PopulateData(CharModel charModel)
        {
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text
                                                    = GetStatValue(charModel);
        }

        string GetStatValue(CharModel charModel)
        {
            string str = "";
            if (charModel == null)
            {
                return str;
            }
            else
            {
                statData = charModel.statsList.Find(t => t.statsName == statName);
                PopulateDesc();
                if (statName == StatsName.armor || statName == StatsName.damage)
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
                            = statData.statsName.ToString().CreateSpace();
            desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                            = statData.desc;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.parent.SetAsLastSibling();
            desc.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            desc.SetActive(false);
        }


    }


}
