using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Interactables;
using TMPro;

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

        [Header("Fame yield Txt")]
        [SerializeField] TextMeshProUGUI fameYieldTxt;

        [Header("Trophy and Pelt yield Txt")]
        [SerializeField] TextMeshProUGUI trophyBuffTxt;
        [SerializeField] TextMeshProUGUI peltBuffTxt;
        
        [SerializeField] int netFameYield;
        [SerializeField] string trophyStr ="";
        [SerializeField] string peltStr=""; 

        void Start()
        {
            trophyBtn.onClick.AddListener(OnTrophyBtnPressed);
            peltBtn.onClick.AddListener(OnPeltBtnPressed);        
            peltSlot = peltBtn.GetComponent<TrophySelectSlotController>();
            trophyslot = trophyBtn.GetComponent<TrophySelectSlotController>();
            BuildingIntService.Instance.OnItemWalled +=
                        (Iitems item, TavernSlotType t) => DsplyBuffNYield();
            
            BuildingIntService.Instance.OnItemWalledRemoved +=
                        (Iitems item, TavernSlotType t) => DsplyBuffNYield();
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
            DsplyBuffNYield(); 
        }      
        void DsplyBuffNYield()
        {
            netFameYield = 0; peltStr = string.Empty; trophyStr = string.Empty;
            DsplyPeltBuff();
            DsplyTrophyBuff();
            DsplyFameYield();

        }
        void FillTrophySlot()
        {
            Iitems itemTrophy =
                    BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall;
            trophyslot = trophyBtn.GetComponent<TrophySelectSlotController>();
            ITrophyable itrophy = itemTrophy as ITrophyable;
            TGBase tgBase = itemTrophy as TGBase; 
            if (itemTrophy != null)
            {
                if (trophyslot.ItemsInSlot.Count == 0)
                {
                    trophyslot.AddItem(itemTrophy, true);
                    netFameYield += itrophy.fameYield;
                    trophyStr = tgBase.allDisplayStr[0]; 
                }
                else if (trophyslot.ItemsInSlot[0].itemName != itemTrophy.itemName)
                {
                    trophyslot.AddItem(itemTrophy, true);
                    netFameYield += itrophy.fameYield;
                    trophyStr = tgBase.allDisplayStr[0];
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
            ITrophyable itrophy = itemPelt as ITrophyable;
            TGBase tgBase = itemPelt as TGBase;
            if (itemPelt != null)
            {
                if (peltSlot.ItemsInSlot.Count == 0)
                {
                    peltSlot.AddItem(itemPelt, true);
                    netFameYield += itrophy.fameYield;
                    peltStr = tgBase.allDisplayStr[0];
                }
                else if (peltSlot.ItemsInSlot[0].itemName != itemPelt.itemName)
                {
                    peltSlot.AddItem(itemPelt, true);
                    netFameYield += itrophy.fameYield;
                    peltStr = tgBase.allDisplayStr[0];
                }
            }
            else if (itemPelt == null)
            {
                peltSlot.ClearSlot();
            }
        }

        void DsplyFameYield()
        {
            if(netFameYield != 0)
                fameYieldTxt.text = netFameYield.ToString();
            else
                fameYieldTxt.text = 0.ToString();
        }
        void DsplyTrophyBuff()
        {
            Iitems itemTrophy =
                 BuildingIntService.Instance.tavernController.tavernModel.trophyOnWall;
            ITrophyable itrophy = itemTrophy as ITrophyable;
            TGBase tgBase = itemTrophy as TGBase;

            if (itemTrophy != null)
            {  
                netFameYield += itrophy.fameYield;
                trophyStr = tgBase.allDisplayStr[0];               
            }
            else if (itemTrophy == null)
            {
                trophyStr = "";
            }            
            trophyBuffTxt.text = trophyStr.ToString();  
        }

        void DsplyPeltBuff()
        {
            Iitems itemPelt =
                 BuildingIntService.Instance.tavernController.tavernModel.peltOnWall;
            ITrophyable itrophy = itemPelt as ITrophyable;
            TGBase tgBase = itemPelt as TGBase;

            if (itemPelt != null)
            { 
                netFameYield += itrophy.fameYield;
                peltStr = tgBase.allDisplayStr[0];                
            }
            else if (itemPelt == null)
            {
                peltStr = "";
            }
            peltBuffTxt.text = peltStr.ToString();
        }
    }
}