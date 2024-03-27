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
    //public class ItemSlotData
    //{
    //    public Iitems item;
    //    public int currentStackSize;
    //    public int slotNum;
    //    //public ItemSlotData(Iitems item, int currentStackSize, int slotNum)
    //    //{
    //    //    this.item = item;
    //    //    this.currentStackSize = currentStackSize;
    //    //    this.slotNum = slotNum;
    //    //}
    //}

    //public class InvModelView
    //{
    //    public List<ItemSlotData> allItemSlotData = new List<ItemSlotData>();
    //    public int currInvSize; 
    //}

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
        public GameObject potionActiveInvPanel;
        public GameObject gewgawsActiveInvPanel;

        [Header("Inv Ls Displayed/ global var")]
        public InvSortingView invSortingView;

        [Header("stash Inv transfer Box")]
        public StashInvTransferBox stashInvTransferBox;


        void Awake()
        {
      
        }
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
           // InitExcessInv();           
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

        public void ResizeInv(int size)
        {
            // each slot to be prefab add n remove slots here
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
