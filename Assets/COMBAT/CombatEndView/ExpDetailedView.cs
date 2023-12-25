using Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Combat
{
    public class ExpDetailedView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("TBR")]
        [SerializeField] TextMeshProUGUI expTxt;
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;
        [SerializeField] Transform OnHoverExp; 

        [Header("Global var")]
        
        [SerializeField] CharModel charModel; 
        public void InitExp(CharModel charModel, int sharedExp)
        {
           this.charModel= charModel;
           string signStr = sharedExp > 0 ? "+" : "";
            expTxt.text = $"{signStr} {sharedExp}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            expTxt.color= colorN;
            OnHoverExp.gameObject.SetActive(true);  
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            expTxt.color = colorHL;
            OnHoverExp.gameObject.SetActive(false);
        }

        void Start()
        {
            expTxt.color = colorN;
        }
    }
}