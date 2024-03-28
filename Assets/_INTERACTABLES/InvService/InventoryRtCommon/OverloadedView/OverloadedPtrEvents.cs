using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class OverloadedPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        OverloadedView overloadedView;
        [SerializeField] TextMeshProUGUI descTxt; 
        public void Init(OverloadedView overloadedView)
        {
            this.overloadedView= overloadedView;  
            descTxt = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            int overloadCount = InvService.Instance.overLoadCount;
            descTxt.text = $" Clear last {overloadCount} slots"; 
            descTxt.gameObject.SetActive(overloadCount > 0);    
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            descTxt.gameObject.SetActive(false);
        }
    }
}