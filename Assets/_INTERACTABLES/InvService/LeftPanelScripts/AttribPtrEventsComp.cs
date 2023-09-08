using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

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

        void Awake()
        {
            desc = transform.GetChild(3).gameObject;
            desc.SetActive(false);
                //attribPanelViewComp =
            //        transform.GetComponentInParent<AttribPanelViewComp>();

            //btmCharViewController =
            //    attribPanelViewComp.transform.parent.parent
            //                    .GetChild(2).GetComponent<BtmCharViewController>();
        }
        private void OnEnable()
        {
            InvService.Instance.OnCharSelectInvPanel += PopulateData;
        }
        private void OnDisable()
        {
            InvService.Instance.OnCharSelectInvPanel -= PopulateData;

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
