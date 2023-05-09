using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactables
{

    public class StatsPtrEventsComp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] AttribName AttribName;
        [SerializeField] StatName statName; 
        [SerializeField] bool isOnLeft = false;

        [SerializeField] GameObject desc;
        [SerializeField] AttribData attribData;
        [SerializeField] StatData statData; 
        public CharModel charModel;
        Transform PanelTrans; 


        void Awake()
        {
            desc = transform.GetChild(2).gameObject;
            desc.SetActive(false);
            PanelTrans = transform.parent;
            InvService.Instance.OnCharSelectInvPanel += PopulateData;
        }
        private void Start()
        {        
            CharService.Instance.allCharsInPartyLocked.ForEach(t => t.OnAttribCurrValSet
             += (AttribModData charModData) => PopulateData(CharService.Instance.GetCharCtrlWithCharID
             (charModData.effectedCharNameID).charModel));
        }
        public void PopulateData(CharModel charModel)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                                    = GetStatAttribStr(charModel);
        }

        string GetStatAttribStr(CharModel charModel)
        {
            string str = "";

            if (charModel == null)
            {
                return str;
            }
            else
            {
                this.charModel = charModel;

                if(AttribName != AttribName.None)
                {

                    attribData = charModel.attribList.Find(t => t.AttribName == AttribName);
                    PopulateDesc();
                    if (AttribName == AttribName.armor || AttribName == AttribName.damage)
                    {
                        str = attribData.minRange + "-" + attribData.maxRange;
                    }
                    else
                    {
                        str = attribData.currValue.ToString();
                    }
                }
                if (statName != StatName.None)
                {
                    statData = charModel.statList.Find(t => t.statName == statName);
                    PopulateDesc();                    
                    str = attribData.currValue.ToString();                    
                }
            }
            return str;
        }

        void PopulateDesc()
        {          
            if(AttribName != AttribName.None)
            {
                desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                            = attribData.AttribName.ToString().CreateSpace();
                desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                = attribData.desc;
            }
            if (statName != StatName.None)
            {
                desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                            = statData.statName.ToString().CreateSpace();
                desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                = statData.desc;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // attribPanelViewComp.ToggleLRPanel(isOnLeft);
            PanelTrans.SetAsLastSibling();
            desc.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            desc.SetActive(false);
        }
    }
}
