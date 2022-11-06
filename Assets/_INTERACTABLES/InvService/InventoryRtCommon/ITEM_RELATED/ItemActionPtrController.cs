using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


namespace Interactables
{
    public class ItemActionPtrController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public ItemActions itemActions;
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;
        [SerializeField] Color colorUnClickable;

        public bool isClickable = true; 
        public void Init(ItemActions itemActions)
        {
            this.itemActions = itemActions;
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isClickable)
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                            = colorHL; 
            else
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                           = colorUnClickable;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                            = colorN;
            else
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
                           = colorUnClickable;
        }

        void Start()
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color
             = colorN;
        }
    }
}
