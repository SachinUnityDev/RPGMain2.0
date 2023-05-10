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
        //public Transform slotParent;
       // public CharScrollSlotController charSlotController;

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
            //parentSlot = transform.parent.GetComponent<iRosterSlot>();
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

        void CreateCharPortClone(CharNames charName) // used to replace the empty state
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
        }


        public void OnDrag(PointerEventData eventData)
        {
          
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
           
            Debug.Log("Hello end drag" + eventData.pointerEnter.name);
            GameObject droppedOn = eventData.pointerEnter;
            if (CharService.Instance.isPartyLocked)
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


//CharacterSO charSO;
//public CharModel charModel;
//[SerializeField] Sprite BGUnClicked;
//[SerializeField] Sprite BGClicked;

//[Header("Not to be ref")]
//[SerializeField] Transform nameContainer;
//[SerializeField] TextMeshProUGUI scrollName;

//if(charSlotController != null)
//{
//if (!rosterSlot.isSlotFull())
//{


//}
//RosterService.Instance.rosterViewController.CharPortraitGO = clone;



//if (charSlotController.cloneCount <= 0)
//// A clone is created and added to the slot
//{
//    CreateEmptyClone();                   
//    charSlotController.cloneCount++;
//}
//else
//{
//    charSlotController.cloneCount--;
//    if(charSlotController.cloneCount == 0)
//    {
//        CreateEmptyClone();
//    }
//        Debug.Log("Has Clone");
//    //clone = gameObject.GetComponent<PortraitDragNDrop>().gameObject; 
//}

//}
// update UI and State