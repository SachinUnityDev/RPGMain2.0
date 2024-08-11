using UnityEngine;
using Interactables;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Combat;
using System;
using TMPro;

namespace Common
{
    public interface iPortrait
    {
        iRosterSlot IRosterSlot { get; set; }

    }
    public class PortraitDragNDrop : MonoBehaviour, IDragHandler, IBeginDragHandler
                                                            , IEndDragHandler, iPortrait
    {
        [Header("Pointer related")]
        RectTransform rectTransform;
        [SerializeField] Canvas canvas;
        [SerializeField] CanvasGroup canvasGroup;

        public CharNames charDragged;
      
        [SerializeField] GameObject clone;
        public iRosterSlot IRosterSlot { get; set; }
        public Transform parentTransform;
  
        void Start()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            else
                canvasGroup = gameObject.GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();
            parentTransform = transform.parent;
            IRosterSlot = transform.parent.GetComponent<iRosterSlot>();            
        }

        #region POINTER METHODS 

        public void OnBeginDrag(PointerEventData eventData)
        {           
            rectTransform = GetComponent<RectTransform>();
            if (IRosterSlot.slotType == RosterSlotType.CharScrollSlot)
            {
                Debug.Log("CHAR NAME.... " + IRosterSlot.charInSlot); 
                CreateCharPortClone(IRosterSlot.charInSlot);
                bool hasAdded=  IRosterSlot.AddChar2UnlockedList(clone);

                GameObject viewGO = RosterService.Instance.rosterViewController.gameObject;
                transform.SetParent(viewGO.transform);  // to keep draged object on top 
                RosterService.Instance.draggedGO = this.gameObject;
                canvasGroup.blocksRaycasts = false;  // to ensure drop

            }
            else if(IRosterSlot.slotType == RosterSlotType.PartySetSlot)
            {
                GameObject viewGO = RosterService.Instance.rosterViewController.gameObject;
                transform.SetParent(viewGO.transform);  // to keep draged object on top 
                RosterService.Instance.draggedGO = gameObject;
                canvasGroup.blocksRaycasts = false;
            }
        }
        #endregion

       public GameObject CreateCharPortClone(CharNames charName) // used to replace the empty state
        {
            // here is the end 
            GameObject cloneGO = RosterService.Instance.rosterSO.charPortPreFab;
            Debug.Log("Clone Created");
            clone = Instantiate(cloneGO);          
            clone.transform.SetParent(transform.parent);
            RectTransform cloneRect = clone.GetComponent<RectTransform>();
            cloneRect.anchoredPosition = Vector3.zero;
            cloneRect.localScale = Vector3.one;
            clone.GetComponent<CanvasGroup>().blocksRaycasts = false;
            return cloneGO; 
        }
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
           
            Debug.Log("Hello end drag" + eventData.pointerEnter.name);
            GameObject droppedOn = eventData.pointerEnter;
            if (CharService.Instance.isPartyLocked
                || !FameService.Instance.fameController
                .IsFameBehaviorMatching(CharService.Instance.GetAllyController(charDragged)))
            { 
                RosterService.Instance.rosterViewController.ReverseBack(this);
                RosterService.Instance.On_PortraitDragResult(false);
                
            }else if (droppedOn.GetComponent<IDropHandler>() == null
                && droppedOn.transform.parent.GetComponent<IDropHandler>() == null )                            
             {
                Debug.Log("I drop handler NOT FOUND" + droppedOn.transform.name);
                RosterService.Instance.rosterViewController.ReverseBack(this); 
                RosterService.Instance.On_PortraitDragResult(false);
             }
            else
            {
                clone.transform.GetChild(1).gameObject.SetActive(true);
                clone.transform.GetChild(2).gameObject.SetActive(true);
                RosterService.Instance.rosterViewController.PopulatePortrait();
            } 
            canvasGroup.blocksRaycasts = true;
        }
    }

}

