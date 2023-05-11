using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

namespace Common
{
    public interface iRosterSlot
    {
        int slotID { get; }
        RosterSlotType slotType { get;  }
        bool isSlotFull();
        CharNames charInSlot { get; set; }

        bool AddChar2UnlockedList(GameObject go);        
        void RemoveCharFrmUnlockedList();
        void RightClickOpts(); 
    }

    public class CharScrollSlotController : MonoBehaviour, IDropHandler, iRosterSlot
    {
        public int cloneCount = 0;
        public CharModel charModel;
        [SerializeField] Sprite BGUnClicked;
        [SerializeField] Sprite BGClicked;

        [Header("not to be ref")]
        [SerializeField] Transform nameContainer;
        [SerializeField] TextMeshProUGUI scrollName;
        [Header("To be ref")]
        public Transform portTransGO; 
        public int slotID => -1;
        public RosterSlotType slotType => RosterSlotType.CharScrollSlot;
        public CharNames charInSlot { get; set; }
        void start() { }
        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedGO = eventData.pointerDrag;
            Debug.Log("Entered Slot Controller handler triggered" + draggedGO);

            PortraitDragNDrop portraitDragNDrop 
                = draggedGO.GetComponent<PortraitDragNDrop>();
            if (portraitDragNDrop != null && draggedGO == RosterService.Instance.draggedGO
                && !CharService.Instance.isPartyLocked)
            {
                draggedGO.transform.SetParent(gameObject.transform);
                draggedGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                iPortrait IPortrait = draggedGO.GetComponent<iPortrait>();
                CharNames draggedChar = IPortrait.IRosterSlot.charInSlot; 

                //SetIPortraitValues();
                portraitDragNDrop.parentTransform = transform;

                CharController charController = CharService.Instance.GetCharCtrlWithName(draggedChar);                

                CharModel charModel = charController.charModel;
                charModel.availOfChar = AvailOfChar.Available;
                RosterViewController rosterViewController = RosterService.Instance.rosterViewController;
                rosterViewController.PopulatePortrait2_Char(draggedChar);
              
                CharService.Instance.allCharsInPartyLocked.Remove(charController);
                Destroy(draggedGO.gameObject);

            }
            else
            {
                Debug.Log("CHAR SLOT REJECTION TRIGGER"); 
            }
        }

        public void SetIPortraitValues()
        {
            iPortrait IPortrait = portTransGO.GetComponent<iPortrait>(); 
            if(IPortrait != null)
            {
                IPortrait.IRosterSlot = this;
                portTransGO.GetComponent<PortraitDragNDrop>().charDragged = IPortrait.IRosterSlot.charInSlot; 


                Debug.Log("PRINT SLOT ID CHAR SLOT" + IPortrait.IRosterSlot.charInSlot); 
            }
            else
            {
                Debug.Log("IPortrait Not found");
            }
        }
        public void PopulatePortrait()
        {
            charModel = RosterService.Instance.scrollSelectCharModel;
            charInSlot = charModel.charName; 
            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            nameContainer = transform.parent.GetChild(1);
            scrollName = nameContainer.GetChild(2).GetComponent<TextMeshProUGUI>();
            string charNameStr = RosterService.Instance.scrollSelectCharModel.charNameStr;
            scrollName.text = charNameStr.CreateSpace();

            if (charModel.availOfChar == AvailOfChar.Available)
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

                portTransGO.GetChild(1).GetComponent<Image>().sprite
                                                                = charSO.bpPortraitUnLocked;
                portTransGO.GetChild(2).GetComponent<Image>().sprite
                                               = charCompSO.frameAvail;
                portTransGO.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                    = CharService.Instance.charComplimentarySO.lvlBarAvail;

                BGUnClicked = CharService.Instance.charComplimentarySO.BGAvailUnClicked;
                BGClicked = CharService.Instance.charComplimentarySO.BGAvailClicked;
                portTransGO.GetChild(0).gameObject.SetActive(true);
                portTransGO.GetChild(0).GetComponent<Image>().sprite
                                                            = BGUnClicked;

                transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else
            {
                portTransGO.GetChild(0).gameObject.SetActive(false);
                portTransGO.GetChild(1).GetComponent<Image>().sprite
                                                               = charSO.bpPortraitUnAvail;
                portTransGO.GetChild(2).GetComponent<Image>().sprite
                                              = charCompSO.frameUnavail;
                // SIDE BARS LVL
                portTransGO.GetChild(2).GetChild(0).GetComponent<Image>().sprite
                            = CharService.Instance.charComplimentarySO.lvlbarUnAvail;
                transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            portTransGO.GetChild(3).GetComponent<TextMeshProUGUI>().text
                            = RosterService.Instance.scrollSelectCharModel.classType.ToString().CreateSpace();
            
            SetIPortraitValues();
        }

        public bool isSlotFull()
        {
            if (charInSlot != CharNames.None)
               return true; 
            else
                return false;
        }

        public void RemoveCharFrmUnlockedList()
        {

        }
        public bool AddChar2UnlockedList(GameObject go)
        {
            portTransGO = go.transform;
            return true;
        }

        public void RightClickOpts()
        {
   
        }

   
    }



}
