using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactables
{
    public class RightClickOpts : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        ItemSlotController itemSlotController; 
        public bool isHovered = false; 
        public void Init(ItemSlotController itemSlotController)
        {
            this.itemSlotController = itemSlotController;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true; 
            gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
            itemSlotController.CloseRightClickOpts();            
        }
    }
}