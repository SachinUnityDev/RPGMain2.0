using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{


    public class ContinueBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] bool isQuickSellDone;
        List<ItemDataWithQty> allItemDataQty = new List<ItemDataWithQty>();
        WoodGameView1 woodGameView;
        WoodGameController1 woodController;
        WoodGameData woodGameData; 
        private void Start()
        {
            isQuickSellDone = false;
        }

        public void Init(WoodGameData woodGameData, WoodGameController1 woodController, WoodGameView1 woodGameView)
        {
            this.woodGameData= woodGameData;
            this.woodGameView= woodGameView;
            this.woodController = woodController;   
            woodGameView.OnRewardQuickSell -= OnQuickSell;
            woodGameView.OnRewardQuickSell += OnQuickSell;

            WoodGameSO woodGameSO = woodController.woodGameSO;
            allItemDataQty = woodGameSO.GetRewardItems(woodGameData.gameSeq, woodGameData.woodGameRank);
        }

        void OnQuickSell()
        {
            isQuickSellDone= true;
            allItemDataQty.Clear();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
           if(!isQuickSellDone)
            {
              List<Iitems> allItems = new List<Iitems>();
                foreach (ItemDataWithQty itemQty in allItemDataQty)
                {
                    for (int i = 0; i < itemQty.quantity; i++)
                    {
                        Iitems item =
                            ItemService.Instance.GetNewItem(itemQty.itemData);
                        allItems.Add(item); 
                    }
                }
                foreach (Iitems itm in allItems)
                {
                    InvService.Instance.invMainModel.AddItem2CommInv(itm); 
                }
            }
            woodController.ExitGame(woodGameData);
        }
    }
}