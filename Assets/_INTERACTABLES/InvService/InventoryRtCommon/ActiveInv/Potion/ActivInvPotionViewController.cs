using Common;
using DG.Tweening;
using Spine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Interactables
{
    public class ActivInvPotionViewController : MonoBehaviour
    {
        [Header("Canvas Not to be ref")]
        [SerializeField] Canvas canvas;

        [SerializeField] CharNames charSelect;
        [SerializeField] int slotNum; 
        // get and set items
        public List<Iitems> allPotionActiveInvList = new List<Iitems>();

        public Transform rightClickOpts;

        void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            InvService.Instance.OnDragResult += OnDragResult2PotionActiveInv;
            slotNum = transform.GetSiblingIndex();
        }

        public void Init()
        {
            InitActiveInvPotions();
        }

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

        public void LoadActiveInvPotions()
        {
           
        }
        
        public void InitActiveInvPotions()
        {
            ClearInv();            
            charSelect = InvService.Instance.charSelect;
            CharModel charModel = CharService.Instance.GetAllyCharModel(charSelect);
            ItemController itemController = ItemService.Instance
                                                .GetItemController(charModel.charName);
            for (int i = 0; i < itemController.itemInGewgawActiveInv.Count; i++)
            {
                InitItemtoActiveSlot(itemController.itemInGewgawActiveInv[i], i); 
            }         
        }
        bool InitItemtoActiveSlot(Iitems item,int slotID)
        {
            Transform child = transform.GetChild(slotID);
            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
            if (iSlotable.ItemsInSlot.Count == 0)
            {
                ItemService.Instance.InitItemToInv(SlotType.PotionsActiveInv
                    , item.itemType, (int)item.itemName,CauseType.Items, 2);
                return true; 
            }
                return false;
        }
        void ClearInv()
        {
            allPotionActiveInvList.Clear();// local list
            for (int i = 0; i<=3; i++)
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

        public void Load()
        {
            Init();

        }

        public void UnLoad()
        {
        }

    }


}

