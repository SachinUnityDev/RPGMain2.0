using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{
    public class DisposeItemSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] GameObject draggedGO;
        [SerializeField] ItemsDragDrop itemsDragDrop;
        public void OnDrop(PointerEventData eventData)
        {
            draggedGO = eventData.pointerDrag;
            itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
            if (itemsDragDrop != null)
            {
                iSlotable islot = itemsDragDrop.iSlotable;

                if (islot != null
                     && (islot.slotType == SlotType.StashInv) && islot.ItemsInSlot.Count > 0)
                {                  
                    islot.RemoveAllItems();

                }
                Destroy(draggedGO);
            }
        }
   
    }
}