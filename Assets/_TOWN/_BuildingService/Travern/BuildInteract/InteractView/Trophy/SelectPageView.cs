using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;

namespace Town
{

    public class SelectPageView : MonoBehaviour  // trophy and pelt page 
    {

        [SerializeField] Button trophyBtn;
        [SerializeField] Button peltBtn;

        List<Iitems> allTrophies = new List<Iitems>();
        List<Iitems> allPelts = new List<Iitems>();

        List<Iitems> allTGs = new List<Iitems>();   
        TrophyView trophyView;
        public TrophySelectSlotController trophyslot;
        public TrophySelectSlotController peltSlot; 

        void Start()
        {
            trophyBtn.onClick.AddListener(OnTrophyBtnPressed);
            peltBtn.onClick.AddListener(OnPeltBtnPressed);
            //BuildingIntService.Instance.OnItemWalled += (Iitems item, TavernSlotType tavernSlotType)
            //                            => FillSelectSlot(tavernSlotType);
            peltSlot = peltBtn.GetComponent<TrophySelectSlotController>();
            trophyslot = trophyBtn.GetComponent<TrophySelectSlotController>();

        }

        void OnTrophyBtnPressed()
        { 
            allTGs.Clear(); 
            allTGs = 
                InvService.Instance.invMainModel.GetAllItemsInCommOrStash(ItemType.TradeGoods);

            allTrophies.Clear();
            foreach (Iitems item in allTGs)
            {
                ITrophyable iTrophy = item as ITrophyable;
                if (iTrophy == null) continue;
                if (iTrophy.tavernSlotType == TavernSlotType.Trophy)
                {
                    if (trophyslot.ItemsInSlot.Count == 0)
                    {
                        allTrophies.Add(item);
                    }
                    else if (trophyslot.ItemsInSlot[0].itemName != item.itemName)
                    {
                        allTrophies.Add(item);
                    }
                }
            }
            if(allTrophies.Count > 0) 
            LoadScrollPage(allTrophies, TavernSlotType.Trophy); 
        }        
        void OnPeltBtnPressed()
        {
            allTGs.Clear();
            allTGs =
                InvService.Instance.invMainModel.GetAllItemsInCommOrStash(ItemType.TradeGoods);
            allPelts.Clear();
            foreach (Iitems item in allTGs)
            {
                ITrophyable iTrophy = item as ITrophyable;
                if (iTrophy == null) continue; 
                if (iTrophy.tavernSlotType == TavernSlotType.Pelt)
                {
                    if (peltSlot.ItemsInSlot.Count == 0)  
                    {
                        allPelts.Add(item);                     
                    }
                    else if (peltSlot.ItemsInSlot[0].itemName != item.itemName)
                    {
                        allPelts.Add(item);
                    }
                }
            }
            if(allPelts.Count >0)
            LoadScrollPage(allPelts, TavernSlotType.Pelt);
        }
        void LoadScrollPage(List<Iitems> slotItems, TavernSlotType tavernSlotType)
        {
          //  ItemService.Instance.itemCardGO.SetActive(false);  // to eliminate item card bug
            trophyView.scrollPageTrans.GetComponent<TrophyScrollView>()
                .InitScrollPage(trophyView,tavernSlotType, slotItems); 
            trophyView.DisplayScrollPage();
        }
        public void InitSelectPage(TrophyView trophyView)
        {
            this.trophyView = trophyView;
            FillTrophySlot();
            FillPeltSlot();
        }
        //void FillSelectSlot(TavernSlotType tavernSlotType)
        //{
        //    if(tavernSlotType == TavernSlotType.Trophy)
        //    {
        //        FillTrophySlot(); 
        //    }
        //    if(tavernSlotType == TavernSlotType.Pelt)
        //    {
        //        FillPeltSlot();
        //    }
        //}

        //void LoadTrophySlot()
        //{
        //    Iitems itemTrophy =
        //           BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall;
        //    trophyslot = trophyBtn.GetComponent<TrophySelectSlotController>();

        //    if (itemTrophy != null)
        //    {
        //        trophyslot.AddItem(itemTrophy, true);
        //    }
        //    else
        //    {
        //        trophyslot.ClearSlot();
        //    }
        //}
        //void LoadPeltSlot()
        //{
        //    Iitems itemTrophy =
        //           BuildingIntService.Instance.tavernController.tavernModel.peltOnWall;
        //    trophyslot = peltBtn.GetComponent<TrophySelectSlotController>();

        //    if (itemTrophy != null)
        //    {
        //        trophyslot.AddItem(itemTrophy, true);
        //    }
        //    else
        //    {
        //        trophyslot.ClearSlot();
        //    }
        //}
        void FillTrophySlot()
        {
            Iitems itemTrophy =
                    BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall;
            trophyslot = trophyBtn.GetComponent<TrophySelectSlotController>();

            if (itemTrophy != null)
            {
                if (trophyslot.ItemsInSlot.Count == 0)
                {
                    trophyslot.AddItem(itemTrophy, true);
                }
                else if (trophyslot.ItemsInSlot[0].itemName != itemTrophy.itemName)
                {
                    trophyslot.AddItem(itemTrophy, true);
                }
            }
            else if (itemTrophy == null)// trophy slot is empty
            {
                trophyslot.ClearSlot();
            }
        }
        void FillPeltSlot()
        {
            Iitems itemPelt =
                     BuildingIntService.Instance.tavernController.tavernModel.peltOnWall;          
            peltSlot = peltBtn.GetComponent<TrophySelectSlotController>();
            
            if (itemPelt != null)
            {
                if (peltSlot.ItemsInSlot.Count == 0)
                {
                    peltSlot.AddItem(itemPelt, true);
                }
                else if (peltSlot.ItemsInSlot[0].itemName != itemPelt.itemName)
                {
                    peltSlot.AddItem(itemPelt, true);
                }
            }
            else if (itemPelt == null)
            {
                peltSlot.ClearSlot();
            }
        }
    }
}