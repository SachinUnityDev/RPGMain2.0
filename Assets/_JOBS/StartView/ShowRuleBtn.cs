using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Town
{
    public class ShowRuleBtn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] Transform ruleTrans;
        
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL; 

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ruleTrans.gameObject.activeInHierarchy)
            {
                ruleTrans.gameObject.SetActive(false);
            }
            else
            {
                ruleTrans.gameObject.SetActive(true);
            } 
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetComponent<Image>().sprite= spriteHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetComponent<Image>().sprite = spriteN;
        }
    }
}