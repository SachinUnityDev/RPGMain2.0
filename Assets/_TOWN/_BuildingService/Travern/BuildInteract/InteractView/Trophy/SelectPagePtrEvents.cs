using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;

namespace Town
{

    public class SelectPagePtrEvents : MonoBehaviour  // trophy and pelt page 
    {

        [SerializeField] Button trophyBtn;
        [SerializeField] Button peltBtn;

        List<Iitems> allTrophies = new List<Iitems>();
        List<Iitems> allPelts = new List<Iitems>();

        List<Iitems> allTGs = new List<Iitems>();   
        TrophyView trophyView;


        TrophySelectSlotController trophyslot;
        TrophySelectSlotController peltSlot; 

        void Start()
        {
            trophyBtn.onClick.AddListener(OnTrophyBtnPressed);
            peltBtn.onClick.AddListener(OnPeltBtnPressed); 
        }

        void OnTrophyBtnPressed()
        { 
            allTGs.Clear(); 
            allTGs = 
                InvService.Instance.invMainModel.GetAllItemsInCommOfType(ItemType.TradeGoods);
            allTrophies.Clear();
            foreach (Iitems item in allTGs)
            {
                ITrophyable iTrophy = item as ITrophyable; 
                if(iTrophy.tavernSlotType == TavernSlotType.Trophy)
                {
                    if (trophyslot.ItemsInSlot[0].itemName != item.itemName)    
                    allTrophies.Add(item);  
                }
            }
            LoadScrollPage(allTrophies); 
        }
        
        void OnPeltBtnPressed()
        {
            allTGs.Clear();
            allTGs =
                InvService.Instance.invMainModel.GetAllItemsInCommOfType(ItemType.TradeGoods);
            allPelts.Clear();
            foreach (Iitems item in allTGs)
            {
                ITrophyable iTrophy = item as ITrophyable;
                if (iTrophy.tavernSlotType == TavernSlotType.Pelt)
                {
                    // same item dont populate in the scroll
                    if (peltSlot.ItemsInSlot[0].itemName != item.itemName)                    
                    allPelts.Add(item);
                }
            }
            LoadScrollPage(allPelts);
        }
        void LoadScrollPage(List<Iitems> allItems)
        {
           // trophyView.scrollPageTrans.GetComponent<TrophyScrollPagePtrEvents>().; 
        }

        public void InitSelectPage(TrophyView trophyView)
        {
            this.trophyView = trophyView;
            Iitems itemTrophy =
                     BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall;
            Iitems itemPelt =
                     BuildingIntService.Instance.tavernController.tavernModel.peltOnWall;

           trophyslot = trophyBtn.GetComponent<TrophySelectSlotController>();
           peltSlot = peltBtn.GetComponent<TrophySelectSlotController>();   

            if (itemTrophy != null && trophyslot.ItemsInSlot[0].itemName != itemTrophy.itemName)
            {
                trophyslot.AddItem(itemTrophy, true);                
            }
            else if (itemTrophy == null)// trophy slot is emplty
            {
                trophyslot.ClearSlot();
            }
            if (itemPelt != null && peltSlot.ItemsInSlot[0].itemName != itemTrophy.itemName)
            {
                peltSlot.AddItem(itemPelt, true);
            }
            else if(itemPelt == null)
            {
                peltSlot.ClearSlot();
            }
        }
    }
}