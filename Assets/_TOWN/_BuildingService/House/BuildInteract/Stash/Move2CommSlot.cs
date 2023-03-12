using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Town
{


    public class Move2CommSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] GameObject draggedGO;
        [SerializeField] ItemsDragDrop itemsDragDrop;
        public void OnDrop(PointerEventData eventData)
        {
            draggedGO = eventData.pointerDrag;
            itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();
            if (itemsDragDrop != null)
            {
                bool isDropSuccess = InvService.Instance.invMainModel.AddItem2CommInv(itemsDragDrop.itemDragged); 
                if (!isDropSuccess) // failed 
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                else  // success
                {
                    iSlotable islot = itemsDragDrop.iSlotable;

                    if (islot != null
                         && (islot.slotType == SlotType.StashInv) && islot.ItemsInSlot.Count > 0)
                    {
                        int count = islot.ItemsInSlot.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if (InvService.Instance.invMainModel.AddItem2CommInv(itemsDragDrop.itemDragged)) // size of list changes with every item removal 
                            {
                                islot.RemoveItem();
                            }
                            else
                            {
                                break; // as soon as you cannot add a item just break 
                            }
                        }
                    }
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                    Destroy(draggedGO);
                }
            }
        }
    }
}