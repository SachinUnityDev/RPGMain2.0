using Common;
using DG.Tweening;
using Spine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Interactables
{/// <summary>
///  Parent to Active inv Slots
/// </summary>
    public class PotionViewControllerParent : MonoBehaviour
    {
        [Header("Canvas Not to be ref")]
        [SerializeField] Canvas canvas;

     //   [SerializeField] CharNames charSelect;
        [SerializeField] int slotNum; 
        // get and set items
        public List<Iitems> allPotionActiveInvList = new List<Iitems>();

        public Transform rightClickOpts;

        [Header("Slot results")]        
        [SerializeField] bool slot1result;
        [SerializeField] bool slot2result;


        void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            InvService.Instance.OnDragResult += OnDragResult2PotionActiveInv;
            InvService.Instance.OnCharSelectInvPanel += LoadActiveInvSlots; 

            slotNum = transform.GetSiblingIndex();
        }

        void LoadActiveInvSlots(CharModel charModel)
        {
            
            ClearInv();

            CharController charController = InvService.Instance.charSelectController;
            if (charController == null) return;
            ActiveInvData activeInvData = InvService.Instance.invMainModel
                                            .GetActiveInvData(charController.charModel.charName);
            if (activeInvData == null) return; 
            for (int i = 0; i < activeInvData.potionActivInv.Count; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.GetComponent<iSlotable>().LoadSlot(activeInvData.potionActivInv[i]);
            }
        }

        #region EQUIP TO VIEW 

        public bool Equip2PotionSlot(Iitems item, bool isload = false)
        {
            // try equip to slot 1 then 2 
            // and if both fails then remove from slot 1 
            if (!isload)
            {
                slot1result = false; slot2result = false;
                slot1result = AddItemtoActiveSlotView(item, 0);
                if (!slot1result) // try slot 2 
                {
                    slot2result = AddItemtoActiveSlotView(item, 1);
                    if (slot2result)
                        return true;
                    else
                        //swap here with first
                        return false;
                }
                return true;
            }
            return true;

        }
        #endregion

        public bool AddItemtoActiveSlotView(Iitems item,int slotID)  // NEW ITEM ADDED
        {
            Transform child = transform.GetChild(slotID);
            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
            if (iSlotable.ItemsInSlot.Count == 0)
            {
                iSlotable.AddItem(item); 
                return true; 
            }
                return false;
        }

        
        void ClearInv()
        {
            allPotionActiveInvList.Clear();// local list
            for (int i = 0; i<3; i++)
            {
                Transform child = transform.GetChild(i); 
                child.gameObject.GetComponent<iSlotable>().ClearSlot();
            }
        }



        public void OnDragResult2PotionActiveInv(bool result, ItemsDragDrop itemsDragDrop)
        {
            if (!result && itemsDragDrop.itemDragged.invSlotType == SlotType.PotionsActiveInv)
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

        #region INIT LOAD UNLOAD
        public void Init()
        {
           
        }

        public void Load()
        {
           

        }

        public void UnLoad()
        {
        }
        #endregion 
        public void ShowRightClickList(ItemSlotController itemSlotController)
        {

            int i = 0;
            foreach (ItemActions itemAction in itemSlotController.rightClickActions)
            {
                Transform btn = rightClickOpts.GetChild(i);
                btn.GetComponentInChildren<TextMeshProUGUI>().text
                    = InvService.Instance.InvSO.GetItemActionStrings(itemAction);
                // this will have a diff class 
                // btn.GetComponent<ItemActionPtrController>().Init(itemAction);
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
    }


}

