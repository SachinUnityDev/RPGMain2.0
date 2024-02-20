using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;
using UnityEngine.EventSystems;
using Combat;
using DG.Tweening;

namespace Interactables
{
    public class AttribPtrEventsComp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] AttribName attribName;
        [SerializeField] bool isOnLeft = false;

        [SerializeField] GameObject desc;
        [SerializeField] AttribData attribData;
        [SerializeField] AttribData attribDataMax;
        AttribPanelViewComp attribPanelViewComp;
        BtmCharViewController btmCharViewController; 

   
        private void Start()
        {
            CharService.Instance.allCharsInPartyLocked.ForEach(t => t.OnAttribCurrValSet
               += PopulateData);

            InvService.Instance.OnCharSelectInvPanel += PopulateData;
         
            desc = transform.GetChild(3).gameObject;
            desc.SetActive(false);
            PopulateData(InvService.Instance.charSelectController.charModel);

        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= PopulateData;
            CharService.Instance.allCharsInPartyLocked.ForEach(t => t.OnAttribCurrValSet
              -= PopulateData);
        }
     
        public void PopulateData(CharModel charModel)
        {
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().text
                                                  = GetStatValue(charModel);
        }


        public void PopulateData(AttribModData charModData)
        {
            CharModel charModel = CharService.Instance.GetCharCtrlWithCharID
                                                        (charModData.effectedCharNameID).charModel; 
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
                attribData = charModel.attribList.Find(t => t.AttribName == attribName);
                PopulateDesc();
                if (attribName.IsAttribDamage())
                {
                    if (attribName == AttribName.dmgMin)
                        attribDataMax = charModel.attribList.Find(t => t.AttribName == AttribName.dmgMax);

                    str = attribData.currValue + "-" + attribDataMax.currValue;
                }else if(attribName.IsAttribArmor())
                {
                    if (attribName == AttribName.armorMin)
                        attribDataMax = charModel.attribList.Find(t => t.AttribName == AttribName.armorMax);
                    str = attribData.currValue + "-" + attribDataMax.currValue;
                }
                else
                {
                    str = attribData.currValue.ToString();
                }
            }
            return str;
        }

        void PopulateDesc()
        {
            if(attribData.AttribName != AttribName.dmgMin && attribData.AttribName != AttribName.armorMin)
            {
                desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                             = attribData.AttribName.ToString().CreateSpace();
                desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                                = attribData.desc;
            }
            else
            {
                if (attribData.AttribName == AttribName.armorMin)                
                    desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Armor";

                if (attribData.AttribName == AttribName.dmgMin)
                    desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Damage";
            }
            TextMeshProUGUI descTxt = desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>(); 
            descTxt.text = attribData.desc;
            descTxt.DOColor(Color.white, 0.1f); 
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
