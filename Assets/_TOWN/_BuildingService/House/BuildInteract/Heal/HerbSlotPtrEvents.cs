using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Common;

namespace Town
{
    public class HerbSlotPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] HerbNQuantity herbNQty;
        [SerializeField] ItemDataWithQty itemDataQty; 
        HealView healView;
        HerbsSlotView herbsSlotView;

        [Header("Slot Number")]
        [SerializeField] int slotNum; 


        [Header("Bg Sprite")]
        [SerializeField] Sprite BGSpriteN;
        [SerializeField] Sprite BGSpriteNA;

        [Header("N/NA Sprite Color")]
        [SerializeField] Color spriteColorN;
        [SerializeField] Color spriteColorNA;

        [SerializeField] Transform OnHoverTrans;

        [SerializeField] Transform clickedFrame; 

        [Header(" Global Var")]
        [SerializeField] bool hasItem = false; 
        private void Start()
        {

        }
        public void InitHealSlot(HealView healView, HerbsSlotView herbsSlotView, HerbNQuantity herbNQty)
        {
            OnHoverTrans = transform.GetChild(2);

            this.healView = healView;
            this.herbsSlotView = herbsSlotView;
            this.herbNQty = herbNQty;

            ItemData itemData = new ItemData(ItemType.Herbs, (int)herbNQty.herbName);
            itemDataQty = new ItemDataWithQty(itemData, herbNQty.qty); 
            FillSlot();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverTrans.gameObject.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverTrans.gameObject.SetActive(false);
        }

        void FillSlot()
        {
            hasItem = InvService.Instance.invMainModel.HasItemInQtyComm(itemDataQty); 
            
            RefreshImg(hasItem);
           
            herbsSlotView.OnSlotSelect(slotNum, hasItem);
            SetHasQtyChk(hasItem);
            RefreshSlotTxt(itemDataQty);
            transform.GetChild(0).gameObject.SetActive(true);
            OnHoverTrans.GetComponentInChildren<TextMeshProUGUI>().text =
                        InvService.Instance.InvSO.GetItemName(itemDataQty.itemData);
        }

        void SetHasQtyChk(bool hasQtyChk)
        {
            if (slotNum == 1)
                herbsSlotView.hasAllItemsSlot1 = hasQtyChk;
            if (slotNum == 2)
                herbsSlotView.hasAllItemsSlot2 = hasQtyChk;
        }

        void RefreshImg(bool hasQty)
        {
            // get sprite in SO
            Sprite sprite = InvService.Instance.InvSO.GetSprite(itemDataQty.itemData.itemName
                                                                    , itemDataQty.itemData.itemType);

            Image img = transform.GetChild(0).GetComponent<Image>();
            img.sprite = sprite;
            if (hasQty)
            {
                transform.GetComponent<Image>().sprite = InvService.Instance.InvSO.filledSlot;
                img.DOColor(spriteColorN, 0.1f);              
            }
            else
            {
                transform.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;
                img.DOColor(spriteColorNA, 0.1f);              
            }
        }
        void RefreshSlotTxt(ItemDataWithQty itemDataWithQty)
        {
            Transform txttrans = transform.GetChild(1).GetChild(0);

            if (itemDataWithQty.quantity > 1)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                txttrans.gameObject.SetActive(true);
                txttrans.GetComponent<TextMeshProUGUI>().text = itemDataWithQty.quantity.ToString();
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(false);
                txttrans.gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           if(hasItem)
           {   
               herbsSlotView.OnSlotSelect(slotNum, hasItem);
           }
        }

        public void SlotSelect()
        {
            clickedFrame.gameObject.SetActive(true);
            
        }
        public void SlotDeSelect()
        {
            clickedFrame.gameObject.SetActive(false);          
        }

    }
}