using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using UnityEngine.UI;
using DG.Tweening; 

namespace Interactables
{
    public class ItemsDragDrop : MonoBehaviour,  IBeginDragHandler
                                , IEndDragHandler, IDragHandler        
    {

        [Header("Pointer related")]
        RectTransform rectTransform;
        [SerializeField] Canvas canvas;
        [SerializeField] CanvasGroup canvasGroup;

        [Header("Reference for the Public")]
        public iSlotable iSlotable; // talk to daddy
        public Iitems itemDragged;
        public Transform slotParent;

        GameObject clone; 
        void Start()
        {
            iSlotable = transform.parent.parent.GetComponent<iSlotable>();
            if (iSlotable == null)
                Debug.Log("ERROR Item Slot Controller Not found");
            if (gameObject.GetComponent<CanvasGroup>() == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            else
                canvasGroup = gameObject.GetComponent<CanvasGroup>();

            canvas = GetComponentInParent<Canvas>();
        }


        #region POINTER METHODS 
        void ClearSlotParent()
        {
            foreach (Transform child in slotParent)
            {

            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {          
            Debug.Log("BEGIN DRAG");
            iSlotable.CloseRightClickOpts();
            rectTransform = GetComponent<RectTransform>();
            slotParent = transform.parent;
            transform.SetParent(slotParent.parent.parent.parent.parent.parent);  // to keep draged object on top 
            
            if (iSlotable.ItemsInSlot.Count <= 0)
            // A clone is created and added to the slot
            {
                CreateEmptyClone(); 
                
            }else
            {
                CreateClone();
            }
            itemDragged = iSlotable.ItemsInSlot[0];
            iSlotable.RemoveItem();
           
            canvasGroup.blocksRaycasts = false;
        }

       void CreateEmptyClone()
        {
            GameObject cloneGO = InvService.Instance.InvSO.itemImgPrefab;
            clone = Instantiate(cloneGO);
            clone.transform.SetParent(slotParent);
            RectTransform cloneRect = clone.GetComponent<RectTransform>();
            cloneRect.anchoredPosition = Vector3.zero;
            cloneRect.localScale = Vector3.one;
            //clone.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
        void CreateClone() // clone should have raycast block while transfer
        {
            clone = Instantiate(this.gameObject);
            clone.transform.SetParent(slotParent);
            RectTransform cloneRect = clone.GetComponent<RectTransform>();
            cloneRect.anchoredPosition = Vector3.zero;
            cloneRect.localScale = Vector3.one;
            //clone.GetComponent<CanvasGroup>().blocksRaycasts = false; 
        }
       
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;            
            GameObject draggedGO = eventData.pointerDrag;

            if (draggedGO.transform.parent.GetComponent<IDropHandler>() == null
                && draggedGO.transform.parent.parent.GetComponent<IDropHandler>() == null)
            {
                Debug.Log("I drop handler NOT FOUND" + draggedGO.transform.parent.parent.name);
                InvService.Instance.On_DragResult(false, this);
            }
            else
            {
                if(clone.GetComponent<CanvasGroup>() != null)  // check for empty slot 
                    clone.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }

        #endregion
    }



}

