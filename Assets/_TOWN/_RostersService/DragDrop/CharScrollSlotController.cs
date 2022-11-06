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
        void RemoveCharFrmUnlockedList();
        bool AddChar2RosterUnLockedList(GameObject go);
        void CloseRightClickOpts();
    }

    public class CharScrollSlotController : MonoBehaviour, IDropHandler, iRosterSlot
    {
        public int cloneCount = 0;
        public CharModel charModel;
        [SerializeField] Sprite BGUnClicked;
        [SerializeField] Sprite BGClicked;
       
        [Header("To be ref")]
        public Transform portTransGO; 

        //[Header("Not to be ref")]
        //[SerializeField] Transform nameContainer;
        //[SerializeField] TextMeshProUGUI scrollName;

        public int slotID => 1;
        public RosterSlotType slotType => RosterSlotType.CharScrollSlot;
        public CharNames charInSlot { get; set; }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedGO = eventData.pointerDrag;
            Debug.Log("Entered Slot Controller handler triggered" + draggedGO);

            PortraitDragNDrop portraitDragNDrop 
                = draggedGO.GetComponent<PortraitDragNDrop>();
            if (portraitDragNDrop != null && draggedGO == RosterService.Instance.draggedGO)
            {
                draggedGO.transform.SetParent(gameObject.transform);
                draggedGO.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                iPortrait IPortrait = portTransGO.GetComponent<iPortrait>();
                SetIPortraitValues();
                portraitDragNDrop.parentTransform = transform;
            }
        }

        void Start()
        {
          
        }

        public void SetIPortraitValues()
        {
            iPortrait IPortrait = portTransGO.GetComponent<iPortrait>(); 
            if(IPortrait != null)
            {
                IPortrait.IRosterSlot = this;               
            }
            else
            {
                Debug.Log("IPortrait Not found");
            }
        }
        public void PopulatePortrait(CharModel charModel)
        {
            charInSlot = charModel.charName; 
          
            CharacterSO charSO = CharService.Instance.GetCharSO(charModel);
            CharComplimentarySO charCompSO = CharService.Instance.charComplimentarySO;

            string charNameStr = RosterService.Instance.scrollSelectCharModel.charNameStr;
           // scrollName.text = charNameStr.CreateSpace();

            if (RosterService.Instance.scrollSelectCharModel.availOfChar == AvailOfChar.Available)
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

        public bool AddChar2RosterUnLockedList(GameObject go)
        {
            portTransGO = go.transform;
            return true;
        }

        public void CloseRightClickOpts()
        {
            
        }
    }



}
