using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;


namespace Common
{
    public class PartyPortraitSlotController : MonoBehaviour, IDropHandler
                                                ,IPointerClickHandler, iRosterSlot
    {
        GameObject draggedGO;
        PortraitDragNDrop portraitDragNDrop;
        public int slotID => transform.GetSiblingIndex();

        public RosterSlotType slotType => RosterSlotType.PartySetSlot;
        public CharNames charInSlot { get; set; }

        public void OnDrop(PointerEventData eventData)
        {
            draggedGO = eventData.pointerDrag;
            Debug.Log("I drop handler triggered" + draggedGO);

            portraitDragNDrop = draggedGO.GetComponent<PortraitDragNDrop>();            
            if (portraitDragNDrop != null)
            {
                bool isDropSuccess = RosterService.Instance
                                        .AddChar2Party(portraitDragNDrop.charDragged); 
                if (isDropSuccess)
                {
                   
                    draggedGO.transform.SetParent(gameObject.transform);
                    draggedGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    CharModel charModel = RosterService.Instance.scrollSelectCharModel; 
                    charModel.availOfChar = AvailOfChar.UnAvailable_InParty;
                    RosterService.Instance.rosterViewController.PopulatePortrait();
                    RosterService.Instance.On_ScrollSelectCharModel(charModel);
                    // RosterService.Instance.On_PortraitDragResult(isDropSuccess);
                    SetIPortraitValues();
                    portraitDragNDrop.parentTransform = transform;
                }                  
                else
                {
                    RosterService.Instance.On_PortraitDragResult(isDropSuccess);                
                }
            }
        }
        public void SetIPortraitValues()
        {
            iPortrait IPortrait = draggedGO.GetComponent<iPortrait>();
            if (IPortrait != null)
            {
                charInSlot = IPortrait.IRosterSlot.charInSlot; 
                IPortrait.IRosterSlot = this;               
                //Debug.Log("PRINT CHAR IN SLOT"+IPortrait.IRosterSlot.charInSlot);                
            }
            else
            {
                Debug.Log("IPortrait Not found");
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {

        }

        void Start()
        {

        }

        public bool isSlotFull()
        {
           //if(charInSlot != CharNames.None)            
           //     return true;            
           // else            
                return false;             
        }

        public bool AddChar2UnlockedList(GameObject go)
        {
           return false;
        }

        public void RemoveCharFrmUnlockedList()
        {
           
        }

        public void RightClickOpts()
        {
          
        }
    }
}