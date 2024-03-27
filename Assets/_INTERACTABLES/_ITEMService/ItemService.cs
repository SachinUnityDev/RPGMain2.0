using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

namespace Interactables
{
    public class ItemService : MonoSingletonGeneric<ItemService>
    {
        public event Action<CharController, GemNames> OnGemSocketed;
        //public event Action<CharController> OnGemEnchanted;
        public event Action<CharController, Iitems> OnItemConsumed;
        public event Action<CharController, Iitems> OnItemEnchanted;
        public event Action<CharController, Iitems> OnItemRead;


        public List<ItemController> allItemControllers = new List<ItemController>();        
        public List<Iitems> allItemsInGame = new List<Iitems>();

        [Header("Curr CharSelected")]
      //  public CharController selectChar; 
        public ItemFactory itemFactory;

        [Header("All Item SO")]
        public AllItemSO allItemSO; 
        //public ItemCardViewController cardViewController;

        // CASE FOR ITEM MAIN MODEL 
        public List<ScrollReadData> allScrollRead = new List<ScrollReadData>();

        //#region SO LIST REFERENCES 

        //[Header("Item View SO ")]
        //public ItemViewSO itemViewSO;

        //[Header("Generic gewgaws SO")]
        //public List<GenGewgawSO> allGenGewgawSO = new List<GenGewgawSO>();

        //[Header("All sagaic Gewgaws SO ")]
        //public List<SagaicGewgawSO> sagaicGewgawSOs = new List<SagaicGewgawSO>();

        //[Header("All Poetic Gewgaw SO")]
        //public List<PoeticGewgawSO> allPoeticGewgawSO = new List<PoeticGewgawSO>();

        //[Header("All Poetic Set SO")]
        //public List<PoeticSetSO> allPoeticSetSO = new List<PoeticSetSO>();

        //[Header("All Potions SO")]
        //public List<PotionSO> allPotionSO = new List<PotionSO>();

        //[Header("All Gems SO")]
        //public List<GemSO> allGemsSO = new List<GemSO>();

        //[Header("All Scroll SO")]
        //public List<ScrollSO> allScrollSO = new List<ScrollSO>();

        //[Header("All Herbs SO ")]
        //public List<HerbSO> allHerbSO = new List<HerbSO>();

        //[Header("All Tools SO")]
        //public List<ToolsSO> allToolsSO = new List<ToolsSO>();

        //[Header("All Food SO")]
        //public List<FoodSO> allFoodSO = new List<FoodSO>();

        //[Header("All Fruit SO")]
        //public List<FruitSO> allFruitSO = new List<FruitSO>();

        //[Header("All Ingredient SO")]
        //public List<IngredSO> allIngredSO = new List<IngredSO>();

        //[Header("All Trade goods SO")]
        //public List<TGSO> allTGSO = new List<TGSO>();

        //[Header("All Meal SO ")]
        //public List<MealsSO> allMealsSO = new List<MealsSO>();

        //[Header("All Alcohol SO")]
        //public List<AlcoholSO> allAlcoholSO = new List<AlcoholSO>();
        //[Header(" ALl Lore Book SO")]
        //public List<LoreBookSO> allLoreBooksSO= new List<LoreBookSO>();

        //#endregion

        [Header("Item card")]
        public GameObject itemCardGO;
       // public GameObject itemCardPrefab; 

        [Header("Game Init")]
        public bool isNewGInitDone = false;

        void Start()
        {
            itemFactory = gameObject.GetComponent<ItemFactory>();
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            FindItemCardGO();
        }
        void FindItemCardGO()
        {
            GameObject canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (itemCardGO == null)
            {
                itemCardGO = canvasGO.transform.GetComponentInChildren<ItemCardView>(true).gameObject;
            }
            itemCardGO.transform.SetParent(canvasGO.transform);
            itemCardGO.transform.SetAsLastSibling();
            itemCardGO.transform.localScale = Vector3.one;
            itemCardGO.SetActive(false);
        }


        public void Init()
        {
            itemFactory = GetComponent<ItemFactory>();
            itemFactory.ItemInit();
            
            foreach (CharController charController in CharService.Instance.allyInPlayControllers)
            {
                ItemController itemController = 
                            charController.gameObject.AddComponent<ItemController>();
                itemController.Init();
                allItemControllers.Add(itemController);
            }
            CalendarService.Instance.OnStartOfCalDay += (int day) => OnDayTickOnScroll();
            isNewGInitDone = true;
    

        }

        #region ITEM EVENTS
        
        public void On_ItemConsumed(CharController charController, Iitems iitem)
        {
            OnItemConsumed?.Invoke(charController, iitem);  
        }
        public void On_ItemEnchanted(CharController charController, Iitems iitem)
        {
            OnItemEnchanted?.Invoke(charController, iitem);
        }
        public void On_ItemRead(CharController charController, Iitems iitem)
        {
            OnItemRead?.Invoke(charController, iitem);
        }

        #endregion

        #region ITEM BASE

        public Iitems GetItemBase(ItemData itemData)
        {
            int index = 
                    allItemsInGame.FindIndex(t => t.itemName == itemData.itemName && t.itemType == itemData.itemType); 
            if(index != -1)
            {
                return allItemsInGame[index];
            }
            else
            {
                return GetNewItem(itemData); // which slot to add this to ?? 
            }
        }

        public Iitems GetNewItem(ItemData itemData)
        {
            Iitems iitems;
            itemFactory = GetComponent<ItemFactory>();
            if (itemData.genGewgawQ == GenGewgawQ.None)
            {
                iitems = itemFactory.GetNewItem(itemData.itemType, itemData.itemName);
            }
            else
            {
                Debug.Log(" Item Created" + itemData.itemName + " type" + itemData.itemType);
                iitems = itemFactory.GetNewGenGewgaw((GenGewgawNames)itemData.itemName, itemData.genGewgawQ);
            }
            return iitems;
        }
        #endregion 

        public int GetRandomItemExcpt(List<int> exceptionLs, int max)
        {
            int ran = GetRandom(max); 
     
            for(int i =0; i< max; i++)
            {
                if (ran == exceptionLs[i])
                {
                    ran = GetRandom(max);// get new random 
                    i = 0; 
                }
            }
            return ran; 
        }
        int GetRandom(int max)
        {
            int ran = UnityEngine.Random
                    .Range(1, (max+1));
            return ran;
        }
        //#region ITEM SO GETTERS

        //public HerbSO GetHerbSO(HerbNames herbname)
        //{
        //    HerbSO herbSO = allHerbSO.Find(t => t.herbName == herbname);
        //    if (herbSO != null)
        //        return herbSO;
        //    else
        //        Debug.Log("herbSO  not found" + herbname);
        //    return null;
        //}
        //public PotionSO GetPotionSO(PotionNames potionName)
        //{
        //    PotionSO potionSO = allPotionSO.Find(t => t.potionName == potionName);
        //    if (potionSO != null)
        //        return potionSO;
        //    else
        //        Debug.Log("potionSO  not found");
        //    return null;
        //}
        //public GemSO GetGemSO(GemNames gemName)
        //{
        //    GemSO gemSO = allGemsSO.Find(t => t.gemName == gemName);
        //    if (gemSO != null)
        //        return gemSO;
        //    else
        //        Debug.Log("GemSO  not found");
        //    return null;
        //}
        //public ScrollSO GetScrollSO(ScrollNames scrollName)
        //{
        //    ScrollSO scrollSO = allScrollSO.Find(t => t.scrollName == scrollName);
        //    if (scrollSO != null)
        //        return scrollSO;
        //    else
        //        Debug.Log("scrollSO  not found");
        //    return null;
        //}
        //public ScrollSO GetScrollSOFrmGem(GemNames gemName)
        //{
        //    ScrollSO scrollSO = allScrollSO.Find(t=>t.enchantmentGemName == gemName);
        //    if (scrollSO != null)
        //        return scrollSO;
        //    else
        //        Debug.Log("scrollSO  not found");
        //    return null;
        //}
        //public SagaicGewgawSO GetSagaicGewgawSO(SagaicGewgawNames sagaicNames)
        //{
        //    SagaicGewgawSO sagaicSO = sagaicGewgawSOs.Find(t => t.sagaicGewgawName == sagaicNames);
        //    if (sagaicSO != null)
        //        return sagaicSO;
        //    else
        //        Debug.Log("Sagaic SO  not found" + sagaicNames);
        //    return null;
        //}
        //public GenGewgawSO GetGenGewgawSO(GenGewgawNames genGewgawName)
        //{
        //    GenGewgawSO genGewgawSO = allGenGewgawSO.Find(t => t.genGewgawName == genGewgawName);
        //    if (genGewgawSO != null)
        //        return genGewgawSO;
        //    else
        //        Debug.Log(genGewgawName+ "genGewGaw SO  not found");
        //    return null;
        //}

        //public PoeticGewgawSO GetPoeticGewgawSO(PoeticGewgawNames poeticGewgawName)
        //{
        //    PoeticGewgawSO poeticGewgawSO = allPoeticGewgawSO.Find(t => t.poeticGewgawName == poeticGewgawName);
        //    if (poeticGewgawSO != null)
        //        return poeticGewgawSO;
        //    else
        //        Debug.Log(poeticGewgawName + "poeticGewgawSO not found" + poeticGewgawName);
        //    return null;
        //}
        //public PoeticSetSO GetPoeticSetSO(PoeticSetName poeticSetName)
        //{
        //    PoeticSetSO poeticSetSO = allPoeticSetSO.Find(t => t.poeticSetName == poeticSetName);
        //    if (poeticSetSO != null)
        //        return poeticSetSO;
        //    else
        //        Debug.Log("poeticGewgawSO not found" + poeticSetName);
        //    return null;
        //}
        //public FoodSO GetFoodSO(FoodNames foodName)
        //{
        //    FoodSO foodSO = allFoodSO.Find(t => t.foodName == foodName);
        //    if (foodSO != null)
        //        return foodSO;
        //    else
        //        Debug.Log("foodSO  not found" + foodName);
        //    return null;
        //}
        //public FruitSO GetFruitSO(FruitNames fruitName)
        //{
        //    FruitSO fruitSO = allFruitSO.Find(t => t.fruitName == fruitName);
        //    if (fruitSO != null)
        //        return fruitSO;
        //    else
        //        Debug.Log("fruitSO  not found");
        //    return null;
        //}
        //public IngredSO GetIngredSO(IngredNames ingredName)
        //{
        //    IngredSO ingredSO = allIngredSO.Find(t => t.ingredName == ingredName);
        //    if (ingredSO != null)
        //        return ingredSO;
        //    else
        //        Debug.Log("ingredSO  not found");
        //    return null;
        //}
        //public TGSO  GetTradeGoodsSO(TGNames tgName)
        //{
        //    TGSO tgSO = allTGSO.Find(t => t.tgName == tgName);
        //    if (tgSO != null)
        //        return tgSO;
        //    else
        //        Debug.Log("Trade goods SO  not found"+ tgName);
        //    return null;
        //}
        //public ToolsSO GetToolSO(ToolNames toolName)
        //{
        //    ToolsSO toolsSO = allToolsSO.Find(t => t.toolName == toolName);
        //    if (toolsSO != null)
        //        return toolsSO;
        //    else
        //        Debug.Log("Trade goods SO  not found");
        //    return null;
        //}
        //public MealsSO GetMealSO(MealNames mealName)
        //{
        //    MealsSO mealSO = allMealsSO.Find(t => t.mealName == mealName);
        //    if (mealSO != null)
        //        return mealSO;
        //    else
        //        Debug.Log("meal  SO  not found" + mealName);
        //    return null;
        //}
        //public AlcoholSO GetAlcoholSO(AlcoholNames alcoholName)
        //{
        //    AlcoholSO alcoholSO = allAlcoholSO.Find(t => t.alcoholName == alcoholName);
        //    if (alcoholSO != null)
        //        return alcoholSO;
        //    else
        //        Debug.Log("alcohol SO  not found" + alcoholName);
        //    return null;
        //}
        //public LoreBookSO GetLoreBookSO(LoreNames loreName)
        //{
        //    LoreBookSO loreBookSO =  allLoreBooksSO.Find(t => t.loreName == loreName);
        //    if (loreBookSO != null)
        //        return loreBookSO;
        //    else
        //        Debug.Log("Lore books SO  not found" + loreName);
        //    return null;
        //}

        //#endregion

        //#region PRICE GETTERS  
        //public CostData GetCostData(ItemType itemType, int itemName)
        //{
        //    CostData costData = null; 
        //    switch (itemType)
        //    {
        //        case ItemType.None:
        //            break;
        //        case ItemType.Potions:
        //            PotionSO potionSO = GetPotionSO((PotionNames)itemName);
        //            costData = new CostData(potionSO.cost, potionSO.fluctuation);
        //            break; 
        //        case ItemType.GenGewgaws:
        //            GenGewgawSO genGewgawSO = GetGenGewgawSO((GenGewgawNames)itemName);
        //            costData = new CostData(genGewgawSO.cost, genGewgawSO.fluctuationRate);
        //            break; 
        //        case ItemType.Herbs:
        //            HerbSO herbSO = GetHerbSO((HerbNames)itemName);
        //            costData = new CostData(herbSO.cost, herbSO.fluctuation);
        //            break; 
        //        case ItemType.Foods:
        //            FoodSO foodSO = GetFoodSO((FoodNames)itemName);
        //            costData=new CostData(foodSO.cost, foodSO.fluctuation);
        //            break;
        //        case ItemType.Fruits:
        //            FruitSO fruitSO = GetFruitSO((FruitNames)itemName);
        //            costData = new CostData(fruitSO.cost, fruitSO.fluctuation);
        //            break;                    
        //        case ItemType.Ingredients:
        //            IngredSO ingredSO = GetIngredSO((IngredNames)itemName); 
        //            costData = new CostData(ingredSO.cost, ingredSO.fluctuation);
        //            break;
        //        case ItemType.XXX:
        //            break;
        //        case ItemType.Scrolls:
        //            ScrollSO scrollSO = GetScrollSO((ScrollNames)itemName);
        //            costData = new CostData(scrollSO.cost, scrollSO.fluctuation);   
        //            break;
        //        case ItemType.TradeGoods:
        //            TGSO tgSO = GetTradeGoodsSO((TGNames)itemName);
        //            costData = new CostData(tgSO.cost, tgSO.fluctuation);   
        //            break;
        //        case ItemType.Tools:
        //            ToolsSO toolSO = GetToolSO((ToolNames)itemName); 
        //            costData = new CostData(toolSO.cost, toolSO.fluctuation);
        //            break;
        //        case ItemType.Teas:
        //            break;
        //        case ItemType.Soups:
        //            break;
        //        case ItemType.Gems:
        //            GemSO gemSO = GetGemSO((GemNames)itemName);
        //            costData = new CostData(gemSO.cost.DeepClone(), gemSO.fluctuation);
        //            break;
        //        case ItemType.Alcohol:
        //            break;
        //        case ItemType.Meals:
        //            break;
        //        case ItemType.SagaicGewgaws:
        //            SagaicGewgawSO sagaicGewgawSO = GetSagaicGewgawSO((SagaicGewgawNames)itemName);
        //            costData = new CostData(sagaicGewgawSO.cost, sagaicGewgawSO.fluctuation);
        //            break;
        //        case ItemType.PoeticGewgaws:
        //           PoeticGewgawSO poeticGewgawSO = GetPoeticGewgawSO((PoeticGewgawNames)itemName);
        //            costData = new CostData(poeticGewgawSO.cost, poeticGewgawSO.fluctuationRate);
        //            break;
        //        case ItemType.RelicGewgaws:
        //            break;
        //        case ItemType.Pouches:
                     
        //            break;
                
        //        default:
        //            break;

        //    }
        //    return costData;
        //}

        //#endregion

        # region GEMS, ENCHANTMENT AND  SCROLLS
        public bool CanEnchantGemThruScroll(CharController charController, GemNames gemName)
        {
            // Get corresponding gem
            ScrollSO scrollSO = allItemSO.GetScrollSOFrmGem(gemName); 
            ItemController itemController = charController.gameObject.GetComponent<ItemController>();
            if (!allScrollRead.Any(t => t.scrollName == scrollSO.scrollName))            
                return false;
            if (itemController.itemModel.IsAlreadyEnchanted())
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
            if (slotType == SlotType.StashInv)
            {
                if (gQuality == GenGewgawQ.None)  //Items apart from genGewgaw
                {
                    Iitems iitems = itemFactory.GetNewItem(itemType, itemName);
                    iitems.invSlotType = slotType;
                    InvService.Instance.invMainModel.AddItem2StashInv(iitems);
                }
                else  //  its a Generic gewgaw
                {
                    Iitems iitems = itemFactory.GetNewGenGewgaw((GenGewgawNames)itemName, gQuality);
                    iitems.invSlotType = slotType;
                    InvService.Instance.invMainModel.AddItem2StashInv(iitems);
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

        public void ItemDispose(Iitems item, SlotType slotype)
        {
            
        }



        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                InitItemToInv(SlotType.CommonInv, ItemType.Gems, (int)GemNames.Ruri,
                                     CauseType.Items, 1);
                InitItemToInv(SlotType.CommonInv, ItemType.Gems, (int)GemNames.Malachite,
                                     CauseType.Items, 1);
                InitItemToInv(SlotType.CommonInv, ItemType.Gems, (int)GemNames.Oltu,
                                     CauseType.Items, 1);
                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.Aloe,
                                    CauseType.Items, 2);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Ginger,
                //                    CauseType.Items, 2);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Kiwi,
                //                    CauseType.Items, 2);
                //InitItemToInv(SlotType.CommonInv, ItemType.LoreBooks, (int)LoreBookNames.LandsOfShargad,
                //               CauseType.Items, 2);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                //   InitItemToInv(SlotType.CommonInv, ItemType.GenGewgaws, (int)GenGewgawNames.ScarfOfCourage,
                //             CauseType.Items, 2, GenGewgawQ.Folkloric);
                //   InitItemToInv(SlotType.CommonInv, ItemType.PoeticGewgaws, (int)PoeticGewgawNames.GlovesLegacyOfTheSpida,
                //   CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.Potions, (int)PotionNames.HealthPotion,
                //       CauseType.Items, 2);

                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //       CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.LionPelt,
                //      CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.LionPelt,
                //      CauseType.Items, 2); InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //       CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.LionPelt,
                //      CauseType.Items, 2); InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //       CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.GreenBoots,
                //      CauseType.Items, 2); InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //       CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.LionessPelt,
                //      CauseType.Items, 2); InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //       CauseType.Items, 2);
                //   InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.LionessPelt,
                //      CauseType.Items, 2);


                InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.FelineHeart,
                   CauseType.Items, 4);
                InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.BatEar,
               CauseType.Items, 4);
                InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.HumanEar,
               CauseType.Items, 4);
                InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Hoof,
               CauseType.Items, 4);

                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.Aloe,
                    CauseType.Items, 2);

                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.Echinacea,
                    CauseType.Items, 2);

                //InitItemToInv(SlotType.CommonInv, ItemType.Foods, (int)FoodNames.Venison, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Foods, (int)FoodNames.Venison, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.Foods, (int)FoodNames.Mutton, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Foods, (int)FoodNames.Beef, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.Foods, (int)FoodNames.Fish, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Grape, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Kiwi, CauseType.Items, 2);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Kiwi, CauseType.Items, 2);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Apple, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Apple, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Apple, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Fruits, (int)FruitNames.Apple, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Wheat, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Wheat, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Wheat, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Wheat, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Yeast, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Yeast, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Yeast, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Yeast, CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Yeast, CauseType.Items, 1);


                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Cardamom, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Cardamom, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.Ingredients, (int)IngredNames.Cardamom, CauseType.Items, 1);

                //InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaTrophy,
                //    CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.LionPelt,
                //   CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.DeerSkin,
                //   CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.TradeGoods, (int)TGNames.NyalaPelt,
                //   CauseType.Items, 1);
                //InitItemToInv(SlotType.CommonInv, ItemType.GenGewgaws, (int)GenGewgawNames.ScarfOfCourage,
                //        CauseType.Items, 2, GenGewgawQ.Folkloric);
                //InitItemToInv(SlotType.CommonInv, ItemType.GenGewgaws, (int)GenGewgawNames.RubyRing,
                //  CauseType.Items, 2, GenGewgawQ.Folkloric);
                InitItemToInv(SlotType.CommonInv, ItemType.PoeticGewgaws, (int)PoeticGewgawNames.NecklaceFirstHuntersArsenal,
                        CauseType.Items, 2);
                InitItemToInv(SlotType.CommonInv, ItemType.PoeticGewgaws, (int)PoeticGewgawNames.BeltPoachersToolset,
                   CauseType.Items, 2);
                InitItemToInv(SlotType.CommonInv, ItemType.PoeticGewgaws, (int)PoeticGewgawNames.RingLegacyOfTheSpida,
                CauseType.Items, 2);
                InitItemToInv(SlotType.CommonInv, ItemType.SagaicGewgaws, (int)SagaicGewgawNames.SoftAndTenacious,
                CauseType.Items, 2);

            }
            if (Input.GetKeyDown(KeyCode.G))
            {

                InitItemToInv(SlotType.CommonInv, ItemType.Potions, (int)PotionNames.HealthPotion,
                   CauseType.Items, 2);
                InitItemToInv(SlotType.CommonInv, ItemType.Potions, (int)PotionNames.FortitudePotion,
             CauseType.Items, 2);
                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.Aloe,
                   CauseType.Items, 2);

                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.Myrsine,
                   CauseType.Items, 2);

                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.Buchu,
                   CauseType.Items, 2);

                InitItemToInv(SlotType.CommonInv, ItemType.Herbs, (int)HerbNames.PoisonIvy,
                   CauseType.Items, 2);

            }            
        }

    }

    public class CostData
    {
        public Currency baseCost;
        public float fluctuation;

        public CostData(Currency cost, float fluctuation)
        {
            this.baseCost = cost;
            this.fluctuation = fluctuation;
        }
    }
}

