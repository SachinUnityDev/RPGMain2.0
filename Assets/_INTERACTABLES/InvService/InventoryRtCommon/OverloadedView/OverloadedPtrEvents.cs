using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Common
{
    public class OverloadedPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        OverloadedView overloadedView;
        
        
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;

        [SerializeField] Image img;
        public void Init(OverloadedView overloadedView)
        {
            this.overloadedView= overloadedView;  
            descTxt = GetComponentInChildren<TextMeshProUGUI>();
            if (descTxt == null) return; 
            descTxt.gameObject.SetActive(false);
            img = GetComponent<Image>();
            img.sprite = spriteN;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            int overloadCount = InvService.Instance.overLoadCount;
            descTxt.text = $" Clear last {overloadCount} slots"; 
            descTxt.gameObject.SetActive(overloadCount > 0);    
            img.sprite= spriteHL;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            descTxt.gameObject.SetActive(false);
            img.sprite = spriteN; 
        }
    }
}