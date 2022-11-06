using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

namespace Interactables
{
    public class GewgawActiveInvSlotController : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            // on drop if slot is empty populate else
            Debug.Log("ON DROP on Gew Gaw Slot");
            GameObject draggedGO = eventData.pointerDrag; 
            if(draggedGO != null)
            {
                if(draggedGO.GetComponent<ItemsDragDrop>().itemDragged.itemType == ItemType.GenGewgaws)
                {
                    Debug.Log("true slot found");
                    draggedGO.GetComponent<RectTransform>().anchoredPosition
                        = gameObject.GetComponent<RectTransform>().anchoredPosition;
                }



                


            }


        }

       
        void Start()
        {

        }


    }



}
