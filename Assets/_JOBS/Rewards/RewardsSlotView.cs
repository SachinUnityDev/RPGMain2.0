using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

namespace Town
{
    public class RewardsSlotView : MonoBehaviour
    {
        WoodGameData woodGameData;
        WoodGameController1 woodController;
        WoodGameView1 woodGameView; 
        List<ItemDataWithQty> allItemDataQty = new List<ItemDataWithQty>();

        public void Init(WoodGameData woodGameData, WoodGameController1 woodController
                                                            , WoodGameView1 woodGameView)
        {   
            woodGameView.OnRewardQuickSell -= ClearSlots;
            woodGameView.OnRewardQuickSell += ClearSlots;
            this.woodGameData= woodGameData;
            this.woodController = woodController; 
            this.woodGameView= woodGameView;
            FillSlot(); 
        }
        void ClearSlots()
        {
            // clear Slots 
            for (int i = 0; i < 3; i++)
            {
                Transform slotTran = transform.GetChild(i);
                slotTran.GetComponent<Image>().sprite
                                            = InvService.Instance.InvSO.emptySlot; 
                slotTran.GetChild(0).gameObject.SetActive(false);
                RefreshSlotTxt(0, i);
            }
        }
        void FillSlot()
        {
            WoodGameSO woodGameSO = woodController.woodGameSO;
            allItemDataQty = woodGameSO.GetRewardItems(woodGameData.gameSeq, woodGameData.woodGameRank);

            for (int i = 0; i < 3; i++)
            {
                if (i < allItemDataQty.Count)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    RefreshImg(allItemDataQty[i], i);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
               
            }
        }

        void RefreshImg(ItemDataWithQty itemQty, int index)
        {
            if(itemQty != null)
            {
                Sprite sprite = InvService.Instance.InvSO.GetSprite(itemQty.itemData.itemName
                                                                , itemQty.itemData.itemType);
                Image img = transform.GetChild(index).GetChild(0).GetComponentInChildren<Image>();
                img.sprite = sprite;
                transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
                transform.GetChild(index).GetComponent<Image>().sprite 
                                            = InvService.Instance.InvSO.filledSlot;
                RefreshSlotTxt(itemQty.quantity, index);
            }
        }
        void RefreshSlotTxt(int qty, int index)
        {
            Transform txttrans = transform.GetChild(index).GetChild(1).GetChild(0);

            if (qty > 1)
            {
                txttrans.parent.gameObject.SetActive(true);
                txttrans.gameObject.SetActive(true);
                txttrans.GetComponent<TextMeshProUGUI>().text = qty.ToString();
            }
            else
            {
                txttrans.gameObject.SetActive(false);
                txttrans.parent.gameObject.SetActive(false);
            }
        }
    }
}