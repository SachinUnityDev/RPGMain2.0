using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class ItemService : MonoSingletonGeneric<ItemService>
    {
        public List<ItemController> allItemControllers = new List<ItemController>();        
        public List<Iitems> allItemsInGame = new List<Iitems>();
            

        [Header("Curr CharSelected")]
        public CharController selectChar; 

        ItemFactory itemFactory;    


        public ItemCardViewController cardViewController;

#region SO LIST REFERNCES 
        [Header("Generic gewgaws SO")]
        public List<GenGewgawSO> allGenGewgawSO = new List<GenGewgawSO>();

        [Header("All sagaic Gewgaws SO ")]
        public List<SagaicGewgawSO> sagaicGewgawSOs = new List<SagaicGewgawSO>();

        [Header("All Potions SO")]
        public List<PotionSO> allPotionSO = new List<PotionSO>();

        [Header("All Gems SO")]
        public List<GemSO> allGemsSO = new List<GemSO>();

        [Header("All Herbs SO ")]
        public List<HerbSO> allHerbSO = new List<HerbSO>();

        [Header("All Tools SO")]
        public List<ToolsSO> allToolsSO = new List<ToolsSO>();

        [Header("All Food SO")]
        public List<FoodSO> allFoodSO = new List<FoodSO>();

        [Header("All Fruit SO")]
        public List<FruitSO> allFruitSO = new List<FruitSO>();

        #endregion


        void Start()
        {

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
        public PotionSO GetPotionSO(PotionName potionName)
        {
            PotionSO potionSO = allPotionSO.Find(t => t.potionName == potionName);
            if (potionSO != null)
                return potionSO;
            else
                Debug.Log("potionSO  not found");
            return null;
        }
        public GemSO GetGemSO(GemName gemName)
        {
            GemSO gemSO = allGemsSO.Find(t => t.gemName == gemName);
            if (gemSO != null)
                return gemSO;
            else
                Debug.Log("GemSO  not found");
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
                Debug.Log("genGewGaw SO  not found");
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

        #endregion
        // game reload or item found in the game
        public void InitItemToInv(SlotType slotType, ItemType itemType, int itemName,
                                     CauseType causeType, int causeID)
        {
            Iitems iitems = itemFactory.GetNewItem(itemType, itemName); 

            iitems.invSlotType = slotType;

            // inventory Data



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
        }




        #region GET ITEM CONTROLLERS AND MODELS
        public ItemController GetItemController(CharNames charName)
        {
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

        public GenGewgawSO GetGewgawSO(GenGewgawNames genGewgawName)
        {
            foreach (GenGewgawSO genGewgaw in allGenGewgawSO)
            {
                if (genGewgaw.genGewgawName == genGewgawName)
                {
                    return genGewgaw;
                }
            }
            Debug.Log("GenGewGawSO not found");
            return null;
        }


        #endregion 
    }







}

