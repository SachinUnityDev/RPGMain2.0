using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using Combat;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Quest;

namespace Interactables
{
    

    // to be attached to inventory Panel... 
    public class InvRightViewController : MonoBehaviour
    {
        // once operation complete update Actual COMMON INVENTORY LIST .. thru Common Inv Controller 
        // ADD and SUBStract rows on Party Lock like Darkest Dungeon 
        // ADD AND SUBTRACT Items as Instcuted by Controller 
        // Populate items in the slots as per Inventory Model
        public int MAX_ABBAS_INVSLOT_SIZE = 18;
        public int MAX_CHAR_INVSLOT_SIZE = 12;
        public int currCommonInvSize;

        

        [Header("Canvas Not To Be Ref")]
        [SerializeField] Canvas canvas; 
        [Header("Inv Panel Ref")]
        public GameObject invContainer;                

        public Transform rightClickOpts;
        [Header("Active Inv")]
        [SerializeField] OverloadedView overloadedView; 

        [Header("Active Inv")]
        public GameObject potionActiveInvPanel;
        public GameObject gewgawsActiveInvPanel;

        [Header("Inv Ls Displayed/ global var")]
        public InvSortingView invSortingView;

        [Header("stash Inv transfer Box")]
        public StashInvTransferBox stashInvTransferBox;

        private void OnEnable()
        {
            invContainer = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            InvService.Instance.OnDragResult += OnDragResult;
            gewgawsActiveInvPanel.SetActive(false);
            canvas = FindObjectOfType<Canvas>();
        }
        private void OnDisable()
        {
            invContainer = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            InvService.Instance.OnDragResult -= OnDragResult;            
        }

        public void Init()
        {
            InitCommonInv();
            ChkOverloadCount();
            // InitExcessInv();           
        }
        public void ChkOverloadCount()
        {
            
            if (GameService.Instance.currGameModel.gameScene == GameScene.TOWN)
            {
                int charLocked = CharService.Instance.allCharsInPartyLocked.Count;
                int maxSlotCount = InvService.Instance.invMainModel.GetCommInvSize();
                int filledSlots = GetActiveSlotCount();
                if(filledSlots > maxSlotCount) 
                {
                    if(currCommonInvSize > filledSlots)                    
                        SetInv2MaxCount(currCommonInvSize);
                    //else
                    //    currCommonInvSize = filledSlots;
                                            
                }
                else if (maxSlotCount == filledSlots) // on inv overload
                {                   
                    // reset ....
                    if(InvService.Instance.overLoadCount > 0)
                        invSortingView.InvSortingGrpInit(this);
                    RemoveNMake_InActive_EmptySlots();
                    filledSlots = GetActiveSlotCount();
                    // open and lock inv 
                    SetInv2MaxCount(filledSlots);
                    currCommonInvSize = filledSlots;
                }
                else
                {
                    currCommonInvSize = maxSlotCount; 
                    SetInv2MaxCount(maxSlotCount);
                }
                InvService.Instance.overLoadCount = filledSlots - maxSlotCount;
                if (InvService.Instance.overLoadCount > 0)
                {
                    overloadedView.Init(); 
                }
            }
        }
        void SetInv2MaxCount(int maxCount)
        {
            for (int i = 0; i < invContainer.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if(child.GetSiblingIndex() < maxCount)
                {
                    if (iSlotable.slotState == SlotState.ActiveNEmpty || iSlotable.slotState == SlotState.ActiveNFull
                        || iSlotable.slotState == SlotState.ActiveNHasSpace)
                    {                        
                        child.gameObject.SetActive(true);
                    }
                    else
                    {
                        iSlotable.slotState = SlotState.ActiveNEmpty;
                        child.gameObject.SetActive(true);
                    }
                }                    
                else // greater than max count
                {
                    iSlotable.slotState = SlotState.InActive;
                    child.gameObject.SetActive(false);
                }
            }
        }
        void RemoveNMake_InActive_EmptySlots()
        {            
            for (int i = 0; i < invContainer.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();

                if (iSlotable.slotState == SlotState.ActiveNEmpty || iSlotable.slotState == SlotState.None)
                {
                    iSlotable.slotState = SlotState.InActive; 
                    child.gameObject.SetActive(false);
                }                    
            }            
        }
        int GetActiveSlotCount()
        {
            int count = 0; 
            for (int i = 0; i < invContainer.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();

                if(iSlotable.slotState== SlotState.ActiveNFull ||
                    iSlotable.slotState == SlotState.ActiveNHasSpace)
                    //|| iSlotable.slotState == SlotState.ActiveNEmpty)
                    count++;
            }
            return count; 
        }

        public void ShowRightClickList(ItemSlotController itemSlotController)
        {
            int i = 0;
           ItemCardView itemCardView = FindObjectOfType<ItemCardView>(true);
            if(itemCardView != null)
            {
                itemCardView.OnRightClickOptsDsply(true); 
            }
            rightClickOpts.GetComponent<RightClickOpts>().Init(itemSlotController); 
            foreach (ItemActions itemAction in itemSlotController.rightClickActions)
            {
                Transform btn = rightClickOpts.GetChild(i); 
                btn.GetComponentInChildren<TextMeshProUGUI>().text
                    = InvService.Instance.InvSO.GetItemActionStrings(itemAction);
                btn.GetComponent<ItemActionPtrController>().Init(itemAction, itemSlotController);
                i++;
            }        
            foreach (Transform child in rightClickOpts)
            {
                if(child.GetComponent<ItemActionPtrController>().itemActions == ItemActions.None)
                {
                    child.gameObject.SetActive(false);
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
                
            }
            Transform slotTrans = itemSlotController.gameObject.transform;
            Vector3 offset = new Vector3(100/2f, 130/2f, 0f)* canvas.scaleFactor;  

            rightClickOpts.DOMove(slotTrans.position+offset, 0.00f);
            rightClickOpts.gameObject.SetActive(true);

        }

        public void OpenRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(true); 
        }
        public void CloseRightClickOpts()
        {
            if(GameService.Instance.currGameModel.gameScene == GameScene.COMBAT)
            {
                return; 
            }

            if (rightClickOpts.GetComponent<RightClickOpts>().isHovered)
                return; 

            //foreach (Transform child in rightClickOpts)
            //{

            //    //if (child.GetComponent<ItemActionPtrController>().isHovered)
            //    //{
            //    //    return;
            //    //}
            //}
            // Item Card Dsply
            ItemCardView itemCardView = FindObjectOfType<ItemCardView>(true);
            if (itemCardView != null)
            {
                itemCardView.OnRightClickOptsDsply(false);
            }
            rightClickOpts.gameObject.SetActive(false);

            foreach (Transform child in rightClickOpts)
            {
                child.GetComponent<ItemActionPtrController>().ResetItemAction();
            }
        }

        public bool Add2ProvisionSlot(Iitems item)
        {
            // find the provisionslotcontroller under this and add item
            ProvisionSlotController provisionSlotController =
                        potionActiveInvPanel.GetComponentInChildren<ProvisionSlotController>();
            bool isAdded =  provisionSlotController.AddItem(item); 
            return isAdded;
            
        }


#region TO_INV_FILL       
        public bool AddItem2InVView(Iitems item, bool onDrop = true) 
         {
            bool slotFound = false;
            for (int i = 0; i < invContainer.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>(); 
                if(iSlotable.ItemsInSlot.Count > 0)
                {
                    if (iSlotable.ItemsInSlot[0].itemName == item.itemName
                        && iSlotable.ItemsInSlot[0].itemType == item.itemType)
                    {
                        if (iSlotable.AddItem(item, onDrop))
                        {
                            slotFound = true;
                            return slotFound;
                        }                    
                    }
                }                    
            }
            if (!slotFound)   // no other filled slot has same item 
            {
                for (int i = 0; i < invContainer.transform.childCount; i++)
                {
                    Transform child = invContainer.transform.GetChild(i);
                    iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                    if (iSlotable.AddItem(item, onDrop))
                    {
                        slotFound = true;
                        return slotFound;
                    }
                }
            }       
            return slotFound; 
         }
        public void UpdateCommInvDB()
        {
            InvService.Instance.invController.itemlsComm.Clear();
            for (int i = 0; i < invContainer.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
               
                if (iSlotable.ItemsInSlot.Count > 0)
                {
                    InvSlotDataBase invSlot = new InvSlotDataBase(iSlotable.ItemsInSlot[0]
                                                                    , iSlotable.ItemsInSlot.Count);
                    InvService.Instance.invController.itemlsComm.Add(invSlot);                    

                }
            }
        }   

        void InitCommonInv()
        {       
            ClearInv();           
            foreach (Iitems item in InvService.Instance.invMainModel.commonInvItems)
            {
                AddItem2InVView(item, false);
            }
            invSortingView.InvSortingGrpInit(this);
            overloadedView.Init(); 
        }
        void InitExcessInv()
        {
            transform.GetChild(1).GetComponent<ExcessInvViewController>().Init();
        }

        void ClearPotionActiveInv()
        {
            for (int i = 0; i < potionActiveInvPanel.transform.childCount; i++)
            {
                Transform child = potionActiveInvPanel.transform.GetChild(i);
                child.gameObject.GetComponent<PotionSlotViewController>().ClearSlot();
            }
        }

        void ClearGewgawActiveInv()
        {         
            for (int i = 0; i < gewgawsActiveInvPanel.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                child.gameObject.GetComponent<GewgawSlotController>().ClearSlot();
            }
        }
        void ClearInv()
        {          
            for (int i = 0; i < invContainer.transform.childCount; i++)
            {
                Transform child = invContainer.transform.GetChild(i);
                child.gameObject.GetComponent<ItemSlotController>().ClearSlot();
            }
        }
#endregion

        public void OnDragResult(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.CommonInv)
            {
                Debug.Log(result + " Drag fail result Invoked Comm Inv");
                Transform slotParent = itemsDragDrop.slotParent;
                itemsDragDrop.transform.DOMove(slotParent.position, 0.1f);

                itemsDragDrop.transform.SetParent(slotParent);
                RectTransform cloneRect = itemsDragDrop.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector3.zero;
                cloneRect.localScale = Vector3.one;

                itemsDragDrop.iSlotable.AddItem(itemsDragDrop.itemDragged);
               
            }            
        }
    }
}
