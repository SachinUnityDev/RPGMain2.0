using Common;
using Interactables;
using System;
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
        public CharController charController; 
        Transform PanelTrans; 


        void Awake()
        {
            desc = transform.GetChild(2).gameObject;
            desc.SetActive(false);
            PanelTrans = transform.parent;            
        }
        private void Start()
        {        
            CharService.Instance.allCharsInPartyLocked.ForEach(t => t.OnAttribCurrValSet
             += (AttribModData charModData) => PopulateData(CharService.Instance.GetCharCtrlWithCharID
             (charModData.effectedCharNameID).charModel));
        }
        private void OnEnable()
        {
            InvService.Instance.OnCharSelectInvPanel += PopulateData;
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= PopulateData;
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
                this.charController = CharService.Instance.GetCharCtrlWithCharID(charModel.charID); 
                if(AttribName != AttribName.None)
                {
                    attribData = charController.GetAttrib(AttribName);
                    PopulateDesc();
                    if (AttribName.IsAttribDamage())
                    {
                        float dmgMin = charController.GetAttrib(AttribName.dmgMin).currValue;
                        float dmgMax = charController.GetAttrib(AttribName.dmgMax).currValue;

                        str = dmgMin + "-" + dmgMax;
                    }
                    else if (AttribName.IsAttribArmor())
                    {
                        float armorMin = charController.GetAttrib(AttribName.armorMin).currValue;
                        float armorMax = charController.GetAttrib(AttribName.armorMax).currValue;

                        str = armorMin + "-" + armorMax;

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
            PanelTrans.SetAsLastSibling();
            desc.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            desc.SetActive(false);
        }
    }
}
