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
        public event Action<CharController, Iitems> OnItemConsumed;
        public event Action<CharController, Iitems> OnItemEnchanted;
        public event Action<CharController, Iitems> OnItemRead;


        public List<ItemController> allItemControllers = new List<ItemController>();        
        public List<Iitems> allItemsInGame = new List<Iitems>();

        [Header("Curr CharSelected")]
        public ItemFactory itemFactory;

        [Header("All Item SO")]
        public AllItemSO allItemSO;
        public Scene currentScene;
        public List<ScrollReadData> allScrollRead = new List<ScrollReadData>();

        [Header("Item card")]
        public GameObject itemCardGO;
       

        [Header("Game Init")]
        public bool isNewGInitDone = false;
        public GameObject canvasGO = null;

        void Start()
        {
            itemFactory = gameObject.GetComponent<ItemFactory>();
        }
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnLoaded;
        }
        void OnSceneUnLoaded(Scene scene)
        {
            if (GameService.Instance.currGameModel.gameScene == GameScene.InTown
                || GameService.Instance.currGameModel.gameScene == GameScene.InCombat
                || GameService.Instance.currGameModel.gameScene == GameScene.InQuestRoom)
            {
                FindItemCardGO();
            }
        }


        void OnSceneLoaded(Scene oldScene, Scene newScene)
        {
            //if(GameService.Instance.currGameModel.gameScene == GameScene.InTown
            //    || GameService.Instance.currGameModel.gameScene == GameScene.InCombat
            //    || GameService.Instance.currGameModel.gameScene == GameScene.InQuestRoom)
            //{
            //    FindItemCardGO();
            //}    
            
        }
        void FindItemCardGO()
        {            
            canvasGO = GameObject.FindGameObjectWithTag("Canvas");
            if (itemCardGO == null)
            {
                itemCardGO = canvasGO.transform.GetComponentInChildren<ItemCardView>(true).gameObject;
            }
            itemCardGO.transform.SetParent(canvasGO.transform);
            itemCardGO.transform.SetAsLastSibling();
            itemCardGO.GetComponent<RectTransform>().localScale = Vector3.one;
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
                InitItemToInv(SlotType.CommonInv, ItemType.Tools, (int)ToolNames.Key,
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

