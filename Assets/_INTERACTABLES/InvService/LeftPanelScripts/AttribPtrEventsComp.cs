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
        [SerializeField] AttribName attribName;
        [SerializeField] bool isOnLeft = false;

        [SerializeField] GameObject desc;
        [SerializeField] AttribData attribData;
        AttribPanelViewComp attribPanelViewComp;
        BtmCharViewController btmCharViewController; 

        void Awake()
        {
            desc = transform.GetChild(3).gameObject;
            desc.SetActive(false);
            InvService.Instance.OnCharSelectInvPanel += PopulateData;
            //attribPanelViewComp =
            //        transform.GetComponentInParent<AttribPanelViewComp>();

            //btmCharViewController =
            //    attribPanelViewComp.transform.parent.parent
            //                    .GetChild(2).GetComponent<BtmCharViewController>();
        }
        private void Start()
        {
            CharService.Instance.allCharsInPartyLocked.ForEach(t => t.OnAttribCurrValSet
               += (AttribModData charModData) => PopulateData(CharService.Instance.GetCharCtrlWithCharID
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
                attribData = charModel.attribList.Find(t => t.AttribName == attribName);
                PopulateDesc();
                if (attribName == AttribName.armor || attribName == AttribName.damage)
                {

                    str = attribData.minRange + "-" + attribData.maxRange;
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
            desc.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                            = attribData.AttribName.ToString().CreateSpace();
            desc.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                            = attribData.desc;
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
