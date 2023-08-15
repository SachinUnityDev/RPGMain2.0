using Interactables;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
namespace Town
{
    public class ContinueBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Sprites")]

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        [SerializeField] Sprite spriteClicked;

        [Header("Global Var")]
        [SerializeField] bool isQuickSellDone;
        [SerializeField] bool isExpConvertDone;
        List<ItemDataWithQty> allItemDataQty = new List<ItemDataWithQty>();
        WoodGameView1 woodGameView;
        WoodGameController1 woodController;
        WoodGameData woodGameData;
        RewardsView rewardView;
        Image img; 

        private void Start()
        {
            isQuickSellDone = false;
            isExpConvertDone= false;
        }

        public void Init(WoodGameData woodGameData, WoodGameController1 woodController
                                , WoodGameView1 woodGameView, RewardsView rewardView)
        {
            this.woodGameData= woodGameData;
            this.woodGameView= woodGameView;
            this.woodController = woodController;   
            this.rewardView = rewardView;
            img = GetComponent<Image>();
            img.sprite = spriteN;
            
            woodGameView.OnRewardQuickSell -= OnQuickSell;
            woodGameView.OnRewardQuickSell += OnQuickSell;
            woodGameView.OnExpConvert -= OnExpConvert;
            woodGameView.OnExpConvert += OnExpConvert;

            WoodGameSO woodGameSO = woodController.woodGameSO;
            allItemDataQty = woodGameSO.GetRewardItems(woodGameData.gameSeq, woodGameData.woodGameRank);
        }

        void OnQuickSell()
        {
            isQuickSellDone= true;
            allItemDataQty.Clear();
        }
        void OnExpConvert()
        {
            isExpConvertDone = true;
            woodGameData.lastGameExp = 0; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            rewardView.ContinueSeq(); 
             ContinueActions();
            img.sprite = spriteClicked; 
        }

        void ContinueActions()
        {
            if (!isQuickSellDone)
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
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.sprite = spriteHL; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN; 
        }
    }
}