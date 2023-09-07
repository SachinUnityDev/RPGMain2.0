using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest
{
    public class InvBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField]TextMeshProUGUI heading; 
        public void OnPointerClick(PointerEventData eventData)
        {
            InvService.Instance.ShowInvXLPanel();
            //GameObject invPanelXL =
            //                InvService.Instance.invXLGO; 

            //UIControlServiceGeneral.Instance.TogglePanelNCloseOthers(invPanelXL, true);
            //invPanelXL.GetComponent<IPanel>().Init();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTxt(); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Hidetxt();  
        }

        void ShowTxt()
        {
            string str = "Inventory";
            heading.alignment = TextAlignmentOptions.Left; 
            heading.text = str;
        }
        void Hidetxt()
        {
            heading.text = "";
        }

        void Start()
        {
            heading = transform.parent.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }
}