using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 


namespace Town
{
    public class RecipeSlotController : MonoBehaviour
    {
        Iitems mainItem; 
        ItemDataWithQty recipeItemWithQty;   
        public void InitSlot(Iitems mainItem, ItemDataWithQty recipeItemWithQty)
        {
            this.mainItem = mainItem;
            this.recipeItemWithQty = recipeItemWithQty;               
            FillSlot(); 
        }
        void FillSlot()
        {           
            if (InvService.Instance.invMainModel.HasItemInQtyCommOrStash(recipeItemWithQty))
            {             
                RefreshImg(recipeItemWithQty.ItemData, true);
            }
            else
            {               
                RefreshImg(recipeItemWithQty.ItemData, false);
            }
            RefreshSlotTxt(recipeItemWithQty); 
        }
        void RefreshImg(ItemData itemData, bool hasQty)
        {
            Transform ImgTrans = gameObject.transform.GetChild(0);
            if (hasQty)
            {
                transform.GetComponent<Image>().sprite = InvService.Instance.InvSO.filledSlot;

               
                ImgTrans.GetComponent<Image>().sprite = InvService.Instance
                                                            .InvSO.GetSprite(itemData.itemName, itemData.itemType);
                ImgTrans.gameObject.SetActive(true);
            }
            else
            {
                transform.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;

                ImgTrans.GetComponent<Image>().sprite = InvService.Instance
                                                            .InvSO.GetSprite(itemData.itemName, itemData.itemType);
                ImgTrans.gameObject.SetActive(true);
            }
        }
        void RefreshSlotTxt(ItemDataWithQty itemDataWithQty)
        {
            Transform txttrans = gameObject.transform.GetChild(1);

            if (itemDataWithQty.quantity > 1)
            {
                txttrans.gameObject.SetActive(true);
                txttrans.GetComponentInChildren<TextMeshProUGUI>().text = itemDataWithQty.quantity.ToString();
            }
            else
            {
                txttrans.gameObject.SetActive(false);
            }
        }
    }
}