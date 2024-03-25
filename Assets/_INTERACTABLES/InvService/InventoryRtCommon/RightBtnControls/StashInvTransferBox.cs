using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactables
{
    public class StashInvTransferBox : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler , IPanel
    {
        [SerializeField] Color colorN;
        [SerializeField] Color colorOnHover;

        [SerializeField] TextMeshProUGUI text;


        [SerializeField] GameObject draggedGO;
        [SerializeField] ItemsDragDrop itemsDragDrop;
        public void Init()
        {
        }

        public void Load()
        {

        }

        // on drop here transfoer to stash// remove from commInv 
        public void OnDrop(PointerEventData eventData)
        {
            draggedGO = eventData.pointerDrag;
            itemsDragDrop = draggedGO.GetComponent<ItemsDragDrop>();

            iSlotable islot = itemsDragDrop?.iSlotable;
            if (islot == null) return; 
            if (islot.slotType == SlotType.ProvActiveInv) return;
            if (itemsDragDrop != null)
            {
                bool isDropSuccess = false; 
                if (itemsDragDrop.itemDragged.invSlotType == SlotType.CommonInv)
                {
                     isDropSuccess = InvService.Instance.invMainModel.AddItem2StashInv(itemsDragDrop.itemDragged);
                }
                else if (itemsDragDrop.itemDragged.invSlotType == SlotType.ExcessInv)
                {
                    isDropSuccess = InvService.Instance.invMainModel.AddItem2StashInv(itemsDragDrop.itemDragged);
                }

                if (!isDropSuccess)
                { // failed                 
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                }
                else  // success
                {
          
                    if (islot != null && 
                        (islot.slotType == SlotType.CommonInv || islot.slotType == SlotType.ExcessInv)
                                                                         && islot.ItemsInSlot.Count > 0)
                    {
                        int count = islot.ItemsInSlot.Count;
                        for (int i = 0; i < count; i++)
                        {
                            if(islot.slotType == SlotType.CommonInv)
                                if (InvService.Instance.invMainModel.AddItem2StashInv(itemsDragDrop.itemDragged)) // size of list changes with every item removal                             
                                    islot.RemoveItem();                            
                                else                            
                                    break; // as soon as you cannot add a item just break 

                            if (islot.slotType == SlotType.ExcessInv)
                                if (InvService.Instance.invMainModel.AddItem2StashInv(itemsDragDrop.itemDragged)) // size of list changes with every item removal                             
                                    islot.RemoveItem();
                                else
                                    break; // as soon as you cannot add a item just break 

                        }
                    }
                    InvService.Instance.On_DragResult(isDropSuccess, itemsDragDrop);
                    Destroy(draggedGO);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           if( eventData.pointerDrag == null)
            {
                text.gameObject.SetActive(true);
                text.color = colorOnHover;
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.gameObject.SetActive(true);
            text.color = colorN;
        }

        public void UnLoad()
        {

        }

        private void Start()
        {
            text= GetComponentInChildren<TextMeshProUGUI>();
            text.color = colorN; 
        }
    }
}