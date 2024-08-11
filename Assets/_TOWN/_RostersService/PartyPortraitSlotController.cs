using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;
using System.Runtime.InteropServices;


namespace Common
{
    public class PartyPortraitSlotController : MonoBehaviour, IDropHandler
                                                , IPointerClickHandler, iRosterSlot
    {
        RosterViewController rosterViewController;

        GameObject draggedGO;
        PortraitDragNDrop portraitDragNDrop;
        public int slotID => transform.GetSiblingIndex();
        public RosterSlotType slotType => RosterSlotType.PartySetSlot;
        public CharNames charInSlot { get; set; }

     
        

        #region DRAG and DROP   

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
                    draggedGO.transform.SetParent(gameObject.transform);
                    draggedGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    SetIPortraitValues();
                    portraitDragNDrop.parentTransform = transform;
                }
                else
                {
                    RosterService.Instance.On_PortraitDragResult(isDropSuccess);
                }
            }
        }

        #endregion

        #region POPULATE PORTRAIT
        public void PopulatePortrait(CharNames charName)
        {
            CharModel charModel = CharService.Instance.GetCharModel(charName);

            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            Transform draggedGOTrans = draggedGO.transform;

            if (charModel.availOfChar == AvailOfChar.Available)
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

                draggedGOTrans.GetChild(1).GetComponent<Image>().sprite
                                                                = charSO.bpPortraitUnLocked;
                draggedGOTrans.GetChild(2).GetComponent<Image>().sprite
                                               = charCompSO.frameAvail;
                draggedGOTrans.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                    = CharService.Instance.charComplimentarySO.lvlBarAvail;

                Sprite BGUnClicked = CharService.Instance.charComplimentarySO.BGAvailUnClicked;
                Sprite BGClicked = CharService.Instance.charComplimentarySO.BGAvailClicked;
                draggedGOTrans.GetChild(0).gameObject.SetActive(true);
                draggedGOTrans.GetChild(0).GetComponent<Image>().sprite = BGUnClicked;
                transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                draggedGOTrans.GetChild(0).gameObject.SetActive(false);
                draggedGOTrans.GetChild(1).GetComponent<Image>().sprite
                                                               = charSO.bpPortraitUnAvail;
                draggedGOTrans.GetChild(2).GetComponent<Image>().sprite
                                              = charCompSO.frameUnavail;
                // SIDE BARS LVL
                draggedGOTrans.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                            = CharService.Instance.charComplimentarySO.lvlbarUnAvail;
                transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            draggedGOTrans.GetChild(3).GetComponent<TextMeshProUGUI>().text
                            = RosterService.Instance.scrollSelectCharModel.classType.ToString().CreateSpace();

        }
         public void OnAddChar2Slot(GameObject draggedgo)
        {
            draggedGO = draggedgo;
            portraitDragNDrop = draggedgo.GetComponent<PortraitDragNDrop>();
            draggedgo.transform.SetParent(gameObject.transform);
            draggedgo.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;            
            SetIPortraitValues();
            PopulatePortrait(charInSlot);
            portraitDragNDrop.parentTransform = transform;
        }
        public void AddChar2SlotOnLoad(GameObject portTransGO, CharNames charName)
        {
            draggedGO = portTransGO;
            portraitDragNDrop = portTransGO.GetComponent<PortraitDragNDrop>();
            portTransGO.transform.SetParent(transform);
            portTransGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;            
          
            RectTransform cloneRect = portTransGO.GetComponent<RectTransform>();
            cloneRect.anchoredPosition = Vector3.zero;
            cloneRect.localScale = Vector3.one;
            iPortrait IPortrait = portTransGO.GetComponent<iPortrait>();
            if (IPortrait != null)
            {
                charInSlot = charName;
                IPortrait.IRosterSlot = this;
                IPortrait.IRosterSlot.charInSlot = charName;
                
            }
            PopulateOnLoad(charInSlot);
            portraitDragNDrop.parentTransform = transform;
        }
        public void ClearSlot()
        {
            PortraitDragNDrop portraitDragNDrop1 = transform?.GetComponentInChildren<PortraitDragNDrop>();
            if (portraitDragNDrop1 != null)
            {
                Destroy(portraitDragNDrop1.gameObject);
            }
        }

        void PopulateOnLoad(CharNames charName)
        {
            CharModel charModel = CharService.Instance.GetCharModel(charName);

            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            Transform draggedGOTrans = draggedGO.transform;

            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text
                                            = charModel.classType.ToString().CreateSpace();

            draggedGOTrans.GetChild(1).GetComponent<Image>().sprite
                                                            = charSO.bpPortraitUnLocked;
            draggedGOTrans.GetChild(2).GetComponent<Image>().sprite
                                           = charCompSO.frameAvail;
            draggedGOTrans.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                = CharService.Instance.charComplimentarySO.lvlBarAvail;

            Sprite BGUnClicked = CharService.Instance.charComplimentarySO.BGAvailUnClicked;
            Sprite BGClicked = CharService.Instance.charComplimentarySO.BGAvailClicked;
            draggedGOTrans.GetChild(0).gameObject.SetActive(true);
            draggedGOTrans.GetChild(0).GetComponent<Image>().sprite = BGUnClicked;
            transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
       
        public void SetIPortraitValues()
        {
            iPortrait IPortrait = draggedGO.GetComponent<iPortrait>();
            if (IPortrait != null)
            {
                charInSlot = IPortrait.IRosterSlot.charInSlot; 
                IPortrait.IRosterSlot = this;               
            }
            else
            {
                Debug.LogError("IPortrait Not found");
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {

        }
        #endregion

        #region MISC INTERFACE 
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

        #endregion



    }
}