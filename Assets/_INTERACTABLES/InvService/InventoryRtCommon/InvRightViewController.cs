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

namespace Interactables
{
    public class ItemSlotData
    {
        public Iitems item;
        public int currentStackSize;
        public int slotNum;
        public ItemSlotData(Iitems item, int currentStackSize, int slotNum)
        {
            this.item = item;
            this.currentStackSize = currentStackSize;
            this.slotNum = slotNum;
        }
    }

    public class InvModelView
    {
        public List<ItemSlotData> allItemSlotData = new List<ItemSlotData>();
        public int currInvSize; 
    }

    // to be attached to inventory Panel... 
    public class InvRightViewController : MonoBehaviour
    {
        // once operation complete update Actual COMMON INVENTORY LIST .. thru Common Inv Controller 
        // ADD and SUBStract rows on Party Lock like Darkest Dungeon 
        // ADD AND SUBTRACT Items as Instcuted by Controller 
        // Populate items in the slots as per Inventory Model
        public int MAX_ABBAS_INVSLOT_SIZE = 18;
        public int MAX_CHAR_INVSLOT_SIZE = 12;

        [Header("Canvas NTBR")]
        [SerializeField] Canvas canvas; 
        [Header("Inv Panel Ref")]
        [SerializeField] GameObject InvPanel;
        //public InvModel currInvModel;
        public int currCommonInvSize;
        public List<InvData> allCommonInvList = new List<InvData>();
        public Transform rightClickOpts;

        [Header("Active Inv")]
        public GameObject potionActiveInvPanel;
        public GameObject gewgawsActiveInvPanel;


       

        void Start()
        {
            InvPanel = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            InvService.Instance.OnDragResult += OnDragResult;
            gewgawsActiveInvPanel.SetActive(false);
        }

        public void Init()
        {           
            InitCommonInv();
           // InitPotionActiveInv(); 
        }



        public void ShowRightClickList(ItemSlotController itemSlotController)
        {
            int i = 0; 
            foreach (ItemActions itemAction in itemSlotController.rightClickActions)
            {
                Transform btn = rightClickOpts.GetChild(i); 
                btn.GetComponentInChildren<TextMeshProUGUI>().text
                    = InvService.Instance.InvSO.GetItemActionStrings(itemAction);
                btn.GetComponent<ItemActionPtrController>().Init(itemAction);
                i++;
            }
            for (int j = i; j < rightClickOpts.childCount; j++)
            {
                rightClickOpts.GetChild(j).gameObject.SetActive(false);
            }
            Transform slotTrans = itemSlotController.gameObject.transform;
            Vector3 offset = new Vector3(100/2f, 130/2f, 0f)* canvas.scaleFactor;  

            rightClickOpts.DOMove(slotTrans.position+offset, 0.01f);
        }

        public void OpenRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(true); 
        }
        public void CloseRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(false); 
        }

        #region TO_INV_FILL
        // ACTUAL ADDITION // will return false if inv is FULL
        public bool AddItem2InVView(InvData invData) 
         {
            bool slotFound = false;
            for (int i = 0; i < InvPanel.transform.childCount; i++)
            {
                Transform child = InvPanel.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>(); 
                if(iSlotable.ItemsInSlot.Count > 0)
                {
                    if (iSlotable.ItemsInSlot[0].item.itemName == invData.item.itemName)
                    {
                        if (iSlotable.AddItem(invData.item))
                        {
                            slotFound = true;
                            break;
                        }                    
                    }
                }                    
            }
            if (!slotFound)   // no other filled slot has same item 
            {
                for (int i = 0; i < InvPanel.transform.childCount; i++)
                {
                    Transform child = InvPanel.transform.GetChild(i);
                    iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                    if (iSlotable.AddItem(invData.item))
                    {
                        slotFound = true;
                        break;
                    }
                }
            }
            if (slotFound)
            {
                allCommonInvList.Add(invData); // local list 
               
            }
            return slotFound; 
         }

        public bool IsCommInvFull()
        {
            for (int i = 0; i < InvPanel.transform.childCount; i++)
            {
                Transform child = InvPanel.transform.GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (!iSlotable.isSlotFull())
                {
                    return false; 
                }
            }
            return true; 
        }

        void InitCommonInv()
        {
            ClearInv();
            //allCommonInvList.AddRange(InvService.Instance.invMainModel.commonInvItems);  // local list
            foreach (InvData invData in InvService.Instance.invMainModel.commonInvItems)
            {
                AddItem2InVView(invData);
            }
        }

        void InitPotionActiveInv()
        {
            ClearPotionActiveInv();
            CharNames charSelect = InvService.Instance.charSelect;
            CharModel charModel = CharService.Instance.GetAllyCharModel(charSelect);

            for (int i = 0; i < potionActiveInvPanel.transform.childCount; i++)
            {
                Transform child = potionActiveInvPanel.transform.GetChild(i);
                // get item in belt slot 1  
                ItemData itemData; 
                switch (i)
                {
                    case 0:
                       itemData = charModel.beltSlot1;
                        break;
                    case 1:
                        itemData = charModel.beltSlot2;
                        break;
                    case 2:
                        itemData = charModel.provisionSlot;
                        break;
                    default:
                        break;
                }
               
                //child.gameObject.GetComponent<PotionSlotController>().AddItem()
            }

            // clearinv 
            // get data from charService belt values 
            // populate data 
            // set potion slot on active 


        }
        void ClearPotionActiveInv()
        {
            for (int i = 0; i < potionActiveInvPanel.transform.childCount; i++)
            {
                Transform child = potionActiveInvPanel.transform.GetChild(i);
                child.gameObject.GetComponent<PotionSlotController>().ClearSlot();
            }
        }

        void ClearGewgawActiveInv()
        {         
            for (int i = 0; i < gewgawsActiveInvPanel.transform.childCount; i++)
            {
                Transform child = InvPanel.transform.GetChild(i);
                child.gameObject.GetComponent<GewgawSlotController>().ClearSlot();
            }
        }
        void ClearInv()
        {
            allCommonInvList.Clear();
            for (int i = 0; i < InvPanel.transform.childCount; i++)
            {
                Transform child = InvPanel.transform.GetChild(i);
                child.gameObject.GetComponent<ItemSlotController>().ClearSlot();
            }
        }
#endregion

        public void OnDragResult(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invType == SlotType.CommonInv)
            {
                Debug.Log(result + " Drag fail result Invoked ");
                Transform slotParent = itemsDragDrop.slotParent;
                itemsDragDrop.transform.DOMove(slotParent.position, 0.1f);

                itemsDragDrop.transform.SetParent(slotParent);
                RectTransform cloneRect = itemsDragDrop.GetComponent<RectTransform>();
                cloneRect.anchoredPosition = Vector3.zero;
                cloneRect.localScale = Vector3.one;

                itemsDragDrop.iSlotable.AddItem(itemsDragDrop.itemDragged);
               
            }            
        }

       
        // this can be done here 
        void PopulateActiveInv()
        {

        }

        // get iitems from InvModel of the given Char 
        // inv controller of the char 
        // inv Chars all list in InvService

        // loop thur all the item slots // get hold of the parent GO and loop 
        // add items with itemUIController that has ref to iItems on itemSlots 
        // item slot controller to again have ref to iItems and drop functionalities asscociated 

    }
}
//public void AddItem2View(InvData invData)
//{
//    // return AddItem2InVView(invData);
//}


//bool AddItem2Slot(Iitems item, int slotID)
//{

//        Transform child = InvPanel.transform.GetChild(slotID);
//        iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
//    if (!iSlotable.isSlotFull())
//        return true;
//    else
//        return false; 

//}
//currInvModel = invModel;
//currCommonInvSize = currInvModel.GetInvSize();             
//PopulateCommonInv();
//PopulateActiveInv();
//PopulateStashInv();


//void InitCommonInvPanel()
//{

//    foreach (Transform child in InvPanel.transform)
//    {
//       if(child.GetComponent<ItemSlotController>() == null)
//       {
//            child.gameObject.AddComponent<ItemSlotController>(); 
//       }
//       // empty slot all inv slot are grey 

//    }
//}


//void PopulateCommonInv()
//{
//    ClearInv();
//    // get Inventory model of all charinPLay.. 
//    //foreach (GameObject charGO in CharService.Instance.allyInPlay)
//    //{
//       // InvController invController = charGO.GetComponent<InvController>();
//        allCommonInvList.AddRange(InvService.Instance.invMainModel.commonInvItems);
//    //foreach (InvData invData in InventoryService.Instance.invMainModel.commonInvItems)
//    //{
//    //    //   if(allCommonInvList.Any(t=>t.invData)

//    //}

//    //}
//    //foreach (var item in allCommonInvList)
//    //{
//    //    AddItem2InVView(item); 
//    //}
//}
