using DG.Tweening;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 


namespace Town
{
    public class RecipeSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler    
    {
        
        [SerializeField] ItemDataWithQty itemDataWithQty;
        CraftView craftView; 
        CraftRecipeView craftRecipeView;

        [Header("Bg Sprite")]
        [SerializeField] Sprite BGSpriteN;
        [SerializeField] Sprite BGSpriteNA;
        
        [Header("N/NA Sprite Color")]
        [SerializeField] Color spriteColorN;
        [SerializeField] Color spriteColorNA;

        [SerializeField] Transform OnHoverTrans;
        private void Start()
        {
         
        }
        public void InitSlot(CraftView craftView, CraftRecipeView craftRecipeView,  ItemDataWithQty itemDataWithQty)
        {
            OnHoverTrans = transform.GetChild(2);
            this.craftView = craftView;
            this.craftRecipeView = craftRecipeView; 
            this.itemDataWithQty = itemDataWithQty;
           
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
            if (InvService.Instance.invMainModel.HasItemInQtyComm(itemDataWithQty))
            {
                RefreshImg(true);
            }
            else
            {
                RefreshImg(false);
                craftRecipeView.hasAllItems = false;
            }
            RefreshSlotTxt(itemDataWithQty);
            transform.GetChild(0).gameObject.SetActive(true);
            OnHoverTrans.GetComponentInChildren<TextMeshProUGUI>().text = 
                        InvService.Instance.InvSO.GetItemName(itemDataWithQty.itemData);
        }
        void RefreshImg(bool hasQty)
        {
            // get sprite in SO
            Sprite sprite = InvService.Instance.InvSO.GetSprite(itemDataWithQty.itemData.itemName
                                                                    , itemDataWithQty.itemData.itemType);
            
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
    }
}

