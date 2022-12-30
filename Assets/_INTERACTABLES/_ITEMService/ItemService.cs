using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Web.UI.Design;

namespace Interactables
{
    public class ItemService : MonoSingletonGeneric<ItemService>
    {
        public event Action<CharController, GemNames> OnGemSocketed; 

        public List<ItemController> allItemControllers = new List<ItemController>();        
        public List<Iitems> allItemsInGame = new List<Iitems>();

        [Header("Curr CharSelected")]
        public CharController selectChar; 

        [SerializeField] ItemFactory itemFactory;    

        public ItemCardViewController cardViewController;

#region SO LIST REFERNCES 
        [Header("Generic gewgaws SO")]
        public List<GenGewgawSO> allGenGewgawSO = new List<GenGewgawSO>();

        [Header("All sagaic Gewgaws SO ")]
        public List<SagaicGewgawSO> sagaicGewgawSOs = new List<SagaicGewgawSO>();

        [Header("All Poetic Gewgaw SO")]
        public List<PoeticGewgawSO> allPoeticGewgawSO = new List<PoeticGewgawSO>(); 

        [Header("All Potions SO")]
        public List<PotionSO> allPotionSO = new List<PotionSO>();

        [Header("All Gems SO")]
        public List<GemSO> allGemsSO = new List<GemSO>();

        [Header("All Scroll SO")]
        public List<ScrollSO> allScrollSO = new List<ScrollSO>();

        [Header("Lore Scroll SO")]
        public LoreScrollSO loreScrollSO;

        [Header("All Herbs SO ")]
        public List<HerbSO> allHerbSO = new List<HerbSO>();

        [Header("All Tools SO")]
        public List<ToolsSO> allToolsSO = new List<ToolsSO>();

        [Header("All Food SO")]
        public List<FoodSO> allFoodSO = new List<FoodSO>();

        [Header("All Fruit SO")]
        public List<FruitSO> allFruitSO = new List<FruitSO>();

        [Header("All Ingredient SO")]
        public List<IngredSO> allIngredSO = new List<IngredSO>();

        [Header("All Trade goods SO")]
        public List<TGSO> allTGSO = new List<TGSO>();

        #endregion

        public List<ScrollReadData> allScrollRead = new List<ScrollReadData>();
        void Start()
        {
            itemFactory = gameObject.GetComponent<ItemFactory>();
            CalendarService.Instance.OnStartOfDay += (int day) => OnDayTickOnScroll();
        }

        // distributed 
        public void Init()
        {
            
            foreach (CharController charController in CharService.Instance.allCharsInParty)
            {
                ItemController itemController = 
                            charController.gameObject.AddComponent<ItemController>();
                itemController.Init();
                allItemControllers.Add(itemController);
                

            }
        }

        #region ITEM SO GETTERS

        public HerbSO GetHerbSO(HerbNames herbname)
        {
            HerbSO herbSO = allHerbSO.Find(t => t.herbName == herbname);
            if (herbSO != null)
                return herbSO;
            else
                Debug.Log("herbSO  not found");
            return null;
        }
        public PotionSO GetPotionSO(PotionNames potionName)
        {
            PotionSO potionSO = allPotionSO.Find(t => t.potionName == potionName);
            if (potionSO != null)
                return potionSO;
            else
                Debug.Log("potionSO  not found");
            return null;
        }
        public GemSO GetGemSO(GemNames gemName)
        {
            GemSO gemSO = allGemsSO.Find(t => t.gemName == gemName);
            if (gemSO != null)
                return gemSO;
            else
                Debug.Log("GemSO  not found");
            return null;
        }
        public ScrollSO GetScrollSO(ScrollNames scrollName)
        {
            ScrollSO scrollSO = allScrollSO.Find(t => t.scrollName == scrollName);
            if (scrollSO != null)
                return scrollSO;
            else
                Debug.Log("scrollSO  not found");
            return null;
        }
        public ScrollSO GetScrollSOFrmGem(GemNames gemName)
        {
            ScrollSO scrollSO = allScrollSO.Find(t=>t.enchantmentGemName == gemName);
            if (scrollSO != null)
                return scrollSO;
            else
                Debug.Log("scrollSO  not found");
            return null;


        }
        public SagaicGewgawSO GetSagaicGewgawSO(SagaicGewgawNames sagaicNames)
        {
            SagaicGewgawSO sagaicSO = sagaicGewgawSOs.Find(t => t.sagaicGewgawName == sagaicNames);
            if (sagaicSO != null)
                return sagaicSO;
            else
                Debug.Log("Sagaic SO  not found");
            return null;
        }
        public GenGewgawSO GetGenGewgawSO(GenGewgawNames genGewgawName)
        {
            GenGewgawSO genGewgawSO = allGenGewgawSO.Find(t => t.genGewgawName == genGewgawName);
            if (genGewgawSO != null)
                return genGewgawSO;
            else
                Debug.Log(genGewgawName+ "genGewGaw SO  not found");
            return null;
        }
        public PoeticGewgawSO GetPoeticGewgawSO(PoeticGewgawNames poeticGewgawName)
        {
            PoeticGewgawSO poeticGewgawSO = allPoeticGewgawSO.Find(t => t.poeticGewgawName == poeticGewgawName);
            if (poeticGewgawSO != null)
                return poeticGewgawSO;
            else
                Debug.Log(poeticGewgawName + "poeticGewgawSO   not found");
            return null;
        }

        public FoodSO GetFoodSO(FoodNames foodName)
        {
            FoodSO foodSO = allFoodSO.Find(t => t.foodName == foodName);
            if (foodSO != null)
                return foodSO;
            else
                Debug.Log("foodSO  not found");
            return null;
        }
        public FruitSO GetFruitSO(FruitNames fruitName)
        {
            FruitSO fruitSO = allFruitSO.Find(t => t.fruitName == fruitName);
            if (fruitSO != null)
                return fruitSO;
            else
                Debug.Log("fruitSO  not found");
            return null;
        }
        public IngredSO GetIngredSO(IngredNames ingredName)
        {
            IngredSO ingredSO = allIngredSO.Find(t => t.ingredName == ingredName);
            if (ingredSO != null)
                return ingredSO;
            else
                Debug.Log("ingredSO  not found");
            return null;
        }
        public TGSO  GetTradeGoodsSO(TGNames tgName)
        {
            TGSO tgSO = allTGSO.Find(t => t.tgName == tgName);
            if (tgSO != null)
                return tgSO;
            else
                Debug.Log("Trade goods SO  not found");
            return null;
        }
        public ToolsSO GetToolSO(ToolNames toolName)
        {
            ToolsSO toolsSO = allToolsSO.Find(t => t.toolName == toolName);
            if (toolsSO != null)
                return toolsSO;
            else
                Debug.Log("Trade goods SO  not found");
            return null;
        }
        #endregion

        #region GETTERS 

     

        #endregion

        // game reload or item found in the game


        # region GEMS, ENCHANTMENT AND  SCROLLS
        public bool CanEnchantGemThruScroll(CharController charController, GemNames gemName)
        {
            // get corresponding gem
            ScrollSO scrollSO = GetScrollSOFrmGem(gemName); 
            ItemController itemController = charController.gameObject.GetComponent<ItemController>();
            if (!allScrollRead.Any(t => t.scrollName == scrollSO.scrollName))            
                return false;
            if (!itemController.itemModel.IsNotEnchanted())
                return false; 

            if (charController.charModel.enchantableGem4Weapon == gemName)
                return true; 
            else
                return false; 
        }
        void OnDayTickOnScroll()
        {
            foreach (ScrollReadData scrollData in allScrollRead.ToList())
            {
                if (scrollData.activeDaysRemaining >= scrollData.activeDaysNet)
                {
                    allScrollRead.Remove(scrollData);
                }
                scrollData.activeDaysRemaining++;
            }
        }
        public void OnScrollRead(ScrollNames scrollName)
        {
            ScrollSO scrollSO = ItemService.Instance.GetScrollSO(scrollName);
            ScrollReadData scrollReadData = new ScrollReadData(scrollName
                                                    , scrollSO.castTime);
            allScrollRead.Add(scrollReadData);
        }
        public GemType GetGemType(GemNames gemName)
        {
            GemType gemType =
                        allGemsSO.Find(t => t.gemName == gemName).gemType;
            if (gemType != 0)
                return gemType;
            else
                Debug.Log("GemType Not found");
            return 0;
        }
        #endregion

        public void InitItemToInv(SlotType slotType, ItemType itemType, int itemName,
                                     CauseType causeType, int causeID, GenGewgawQ gQuality = GenGewgawQ.None)
        {
            if(slotType == SlotType.CommonInv)
            {
                if (gQuality == GenGewgawQ.None)  //Items apart from genGewgaw
                {
                    Iitems iitems = itemFactory.GetNewItem(itemType, itemName);
                    iitems.invSlotType = slotType;
                    InvService.Instance.invMainModel.AddItem2CommInv(iitems);
                }
                else  //  its a Generic gewgaw
                {
                    Iitems iitems = itemFactory.GetNewGenGewgaw((GenGewgawNames)itemName, gQuality);
                    iitems.invSlotType = slotType;
                    InvService.Instance.invMainModel.AddItem2CommInv(iitems);
                }
            }
            

        }
        #region GET ITEM CONTROLLERS AND MODELS
        public ItemController GetItemController(CharNames charName)
        {
            // all charController in party

           ItemController itemController =
                        allItemControllers.Find(t => t.itemModel.charName == charName); 
            if (itemController != null)
            {
                return itemController;
            }
            else
            {
                Debug.Log("Item Controller not found");
                return null;
            }
        }
        #endregion

        #region GET SCRIPTABLES 

        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                InitItemToInv(SlotType.CommonInv, ItemType.Scrolls, (int)ScrollNames.ScrollOfEarth,
                                     CauseType.Items,2); 
        
            }
        }

    }
}
//create  item
//add to inv with slot type


// get items form item factory                
// iitems init=> give slot type, inv location ,  
// add to common, excess , potion slot , stash .. excess 

//PotionSO potionSO = GetPotionModelSO(_potionName);
//PotionModel potionModel = potionBase.PotionInit(potionSO);
//allPotionModel.Add(potionModel);

//Iitems item = potionBase as Iitems;
//item.invType = SlotType.CommonInv;

//CharModel charModel = CharService.Instance.GetAllyCharModel(charName);
//if (charModel != null)
//{
//    InvData invData = new InvData(charModel.charName, item);
//    InvService.Instance.invMainModel.AddItem2CommInv(invData);
//}
//else
//{
//    Debug.Log("CharModel is null" + charName);
//}
