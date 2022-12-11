using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Interactables
{
    public class ActivInvPotionViewController : MonoBehaviour
    {

        // 2 belts slots and provision slots to be populated as per charsselect In Main inv 
        // on drop fail eveent to be linked here 


        [Header("Canvas Not to be ref")]
        [SerializeField] Canvas canvas;

        [SerializeField] CharNames charSelect;

        // get and set items
        public List<Iitems> allPotionActiveInvList = new List<Iitems>();

        public Transform rightClickOpts;

        void Start()
        {
            canvas = FindObjectOfType<Canvas>();
            InvService.Instance.OnDragResult += OnDragResult2PotionActiveInv;
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
                btn.GetComponent<ItemActionPtrController>().Init(itemAction);
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
           // 
        }
        
        public void InitActiveInvPotions()
        {
            ClearInv();

            // go to each of the belts as per the char Select
            charSelect = InvService.Instance.charSelect;
            CharModel charModel = CharService.Instance.GetAllyCharModel(charSelect);           
            InitPotion2ActiveInvSlot(charModel.beltSlot1, 0);
            InitPotion2ActiveInvSlot(charModel.beltSlot2, 1);
            InitPotion2ActiveInvSlot(charModel.provisionSlot, 2);
        }

        public bool InitPotion2ActiveInvSlot(ItemData itemData, int slotID)  // ACTUAL ADDITION 
        {
            Transform child = transform.GetChild(slotID);
            iSlotable iSlotable = child.gameObject.GetComponent<iSlotable>();
            if (iSlotable.ItemsInSlot.Count > 0)
            {
                Debug.Log("Slot already has item");  // swap
                return false;
            }
            else
            {

                PotionsBase potionbase = PotionService.Instance
                                        .GetPotionBase((PotionNames)(int)itemData.ItemName); 
                
                Iitems item = potionbase as Iitems;
                InvData invData = new InvData(charSelect, item);
                iSlotable.AddItem(item);                
                return true;
            }
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

