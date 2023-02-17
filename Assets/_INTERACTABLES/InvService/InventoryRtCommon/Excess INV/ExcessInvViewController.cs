using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using TMPro;
using System.Linq;

namespace Interactables
{
    public class ExcessInvViewController : MonoBehaviour, IPanel
    {

        [Header("Inv Sell & Dispose Buttons")]

        [SerializeField] Button disposeAllBtn;
        [SerializeField] Button sellAllBtn; 

        [Header("Canvas NTBR")]
        [SerializeField] Canvas canvas;

        [Header("Excess Panel Ref")]                    
        public int currExcessInvSize;

       // public List<InvData> allExcessInvList = new List<InvData>();

        public Transform rightClickOpts;

        void Start()
        {
            InvService.Instance.OnDragResult += OnDragResult2Excess;
            disposeAllBtn.onClick.AddListener(OnDisposeAllPressed); 
            sellAllBtn.onClick.AddListener(OnSellAllPressed);   

            Init();
        }


        void OnDisposeAllPressed()
        {
            // remove all 
           // ClearInv();
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
                child.gameObject.GetComponent<ExcessItemSlotController>().RemoveAllItems(); 
            }
            InvService.Instance.invMainModel.excessInvItems.Clear(); 
        }

        void OnSellAllPressed()
        {
            // iitem get SO or directly get price
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
                iSlotable iSlot = child.gameObject.GetComponent<iSlotable>(); 
                if (!child.gameObject.GetComponent<ExcessItemSlotController>().IsEmpty())
                {
                    int count = iSlot.ItemsInSlot.Count;
                    Iitems item = iSlot.ItemsInSlot[0]; 
                    if (count > 0)
                    {
                        CostData costData = 
                        ItemService.Instance.GetCostData(item.itemType, item.itemName); 
                    //    int silver =                   
                       // costData.cost.silver
                        
                    }

                }

            }

            //EcoServices.Instance.PayMoney2Player(); 


        }

        #region SLOT RELATED 
        public void ShowRightClickList(ItemSlotController itemSlotController)
        {
          
            int i = 0;
            foreach (ItemActions itemAction in itemSlotController.rightClickActions)
            {
                Transform btn = rightClickOpts.GetChild(i);
                btn.GetComponentInChildren<TextMeshProUGUI>().text
                    = InvService.Instance.InvSO.GetItemActionStrings(itemAction);
                //btn.GetComponent<ItemActionPtrController>().Init(itemAction);
                i++;
            }
            for (int j = i; j < rightClickOpts.childCount; j++)
            {
                rightClickOpts.GetChild(j).gameObject.SetActive(false);
            }
            Transform slotTrans = itemSlotController.gameObject.transform;
            Vector3 offset = new Vector3(100 / 2f, 130 / 2f, 0f) * canvas.scaleFactor;

            rightClickOpts.DOMove(slotTrans.position + offset, 0.01f);
        }

        public void OpenRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(true);
        }
        public void CloseRightClickOpts()
        {
            rightClickOpts.gameObject.SetActive(false);
        }
        public void OnDragResult2Excess(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.ExcessInv)
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

        #endregion

        #region TO_INV_FILL
        public bool AddItem2InVView(Iitems item, bool onDrop = true)  // ACTUAL ADDITION 
        {
            bool slotFound = false;
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (iSlotable.ItemsInSlot.Count > 0)
                {
                    if (iSlotable.ItemsInSlot[0].itemName == item.itemName)
                    {
                        if (iSlotable.AddItem(item))
                        {
                            slotFound = true;
                            break;
                        }
                    }
                }
            }
            if (!slotFound)
            {
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    Transform child = transform.GetChild(0).GetChild(i);
                    iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                    if (iSlotable.AddItem(item))
                    {
                        slotFound = true;
                        break;
                    }
                }
            }
          

            return slotFound;
        }

        public bool IsExcessInvFull()
        {
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);
                iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
                if (!iSlotable.isSlotFull())
                {
                    return false;
                }
            }
            return true;
        }
        public void InitExcessInv()
        {
            ClearInv();
  
            //foreach (var item in InvService.Instance.invMainModel.excessInvItems.ToList())
            //{
            //    //AddItem2InVView(item);
            //}
        }

        void ClearInv()
        {           
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
            {
                Transform child = transform.GetChild(0).GetChild(i);  // go
                child.gameObject.GetComponent<ExcessItemSlotController>().ClearSlot();
            }
        }
        #endregion

     
        #region INIT, LOAD, and UNLOAD
        public void Init()
        {
            ClearInv();
            InitExcessInv();
        }
        public void Load()
        {
           
        }

        public void UnLoad()
        {
        }

        #endregion
    }

}

