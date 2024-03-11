using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "AllItemSO", menuName = "Item Service/AllItemSO")]

public class AllItemSO : ScriptableObject
{
    public bool IsGewgawEquipable(CharController charController, Iitems item)
    {
    
            if (item.itemType == ItemType.GenGewgaws)
            {
                GenGewgawSO genGewgawSO = GetGenGewgawSO((GenGewgawNames)item.itemName);
                GewgawSlotType slotType = genGewgawSO.gewgawSlotType;
                return IsClassRaceCultReqMatch(charController, genGewgawSO.classRestrictions, genGewgawSO.cultureRestrictions
                     , genGewgawSO.raceRestrictions);

            }
            else if (item.itemType == ItemType.SagaicGewgaws)
            {
                SagaicGewgawSO sagaicGewgawSO = GetSagaicGewgawSO((SagaicGewgawNames)item.itemName);
                return IsClassRaceCultReqMatch(charController, sagaicGewgawSO.classRestrictions, sagaicGewgawSO.cultureRestrictions
                     , sagaicGewgawSO.raceRestrictions);
            }
            else if (item.itemType == ItemType.PoeticGewgaws)
            {
                PoeticGewgawSO poeticGewgawSO = GetPoeticGewgawSO((PoeticGewgawNames)item.itemName);
                return IsClassRaceCultReqMatch(charController, poeticGewgawSO.classRestrictions, poeticGewgawSO.cultureRestrictions
                     , poeticGewgawSO.raceRestrictions);
            }        
        return false;
    }
    bool IsClassRaceCultReqMatch(CharController charController, List<ClassType> classReq, List<CultureType> cultReq, List<RaceType> raceReq)
    {

        if (classReq.Count > 0)
        {
            ClassType classType = charController.charModel.classType;
            if (classReq.Any(t => t == classType))
                return true;
            else
                return false;
        }
        else if (cultReq.Count > 0)
        {
            CultureType cultType = charController.charModel.cultType;
            if (cultReq.Any(t => t == cultType))
                return true;
            else
                return false;
        }
        else if (raceReq.Count > 0)
        {
            RaceType raceType = charController.charModel.raceType;
            if (raceReq.Any(t => t == raceType))
                return true;
            else
                return false;
        }
        return false; 
    }
    public int IsSlotRestricted(CharController charController, Iitems item)
    {
        if (item == null)
            return -1;
        GewgawSlotType slotType = GetSlotType(item); 
        if (!IsSlotUNRetricted(charController, slotType))
        {
            ActiveInvData activeInvData = InvService.Instance.invMainModel.GetActiveInvData(charController.charModel.charID);
            if (activeInvData == null) return -1;

            for (int i = 0; i < activeInvData.gewgawActiveInv.Length; i++)
            {
                Iitems item1 = activeInvData.gewgawActiveInv[i]; 
                if(item1 == null) continue;
                if (GetSlotType(item1) == slotType)// to be fixed
                    return i; 
            }            
        }
        return -1; 
    }

    public bool IsSlotUNRetricted(CharController charController, GewgawSlotType slotType)
    {
        // get other equipped items
        ActiveInvData activeInvData = InvService.Instance.invMainModel.GetActiveInvData(charController.charModel.charID);
        if (activeInvData == null) return true; 
        // slot type count from current Data
        int currCount = 0; int allowedCount = 1; 
        foreach (Iitems item in activeInvData.gewgawActiveInv)
        {
            if(item== null) continue;
            if(GetSlotType(item) == slotType)
                currCount++;
        }
        charController.charModel.extraSlotTypeAllowed.ForEach(t => { if (t == slotType) allowedCount++; });
        if (currCount < allowedCount)
            return true;
        else return false; 
    }

    GewgawSlotType GetSlotType(Iitems item)
    {
        // get so 
        if (item.itemType == ItemType.GenGewgaws)
        {
            GenGewgawSO genGewgawSO = GetGenGewgawSO((GenGewgawNames)item.itemName);
            GewgawSlotType slotType = genGewgawSO.gewgawSlotType;
            return slotType; 
        }
        else if (item.itemType == ItemType.SagaicGewgaws)
        {
            SagaicGewgawSO sagaicGewgawSO = GetSagaicGewgawSO((SagaicGewgawNames)item.itemName);
            return sagaicGewgawSO.gewgawSlotType; 
        }
        else if (item.itemType == ItemType.PoeticGewgaws)
        {
            PoeticGewgawSO poeticGewgawSO = GetPoeticGewgawSO((PoeticGewgawNames)item.itemName);
            return poeticGewgawSO.gewgawSlotType;   
        }
        return 0; 
    }

    #region  ITEM SO REFENCES
    [Header("Item View SO")]
    public ItemViewSO itemViewSO;

    [Header("Generic gewgaws SO")]
    public List<GenGewgawSO> allGenGewgawSO = new List<GenGewgawSO>();

    [Header("All sagaic Gewgaws SO ")]
    public List<SagaicGewgawSO> sagaicGewgawSOs = new List<SagaicGewgawSO>();

    [Header("All Poetic Gewgaw SO")]
    public List<PoeticGewgawSO> allPoeticGewgawSO = new List<PoeticGewgawSO>();

    [Header("All Poetic Set SO")]
    public List<PoeticSetSO> allPoeticSetSO = new List<PoeticSetSO>();

    [Header("All Potions SO")]
    public List<PotionSO> allPotionSO = new List<PotionSO>();

    [Header("All Gems SO")]
    public List<GemSO> allGemsSO = new List<GemSO>();

    [Header("All Scroll SO")]
    public List<ScrollSO> allScrollSO = new List<ScrollSO>();

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

    [Header("All Meal SO ")]
    public List<MealsSO> allMealsSO = new List<MealsSO>();

    [Header("All Alcohol SO")]
    public List<AlcoholSO> allAlcoholSO = new List<AlcoholSO>();
    [Header(" ALl Lore Book SO")]
    public List<LoreBookSO> allLoreBooksSO = new List<LoreBookSO>();




    #endregion

    #region ITEM SO GETTERS

    public HerbSO GetHerbSO(HerbNames herbname)
    {
        HerbSO herbSO = allHerbSO.Find(t => t.herbName == herbname);
        if (herbSO != null)
            return herbSO;
        else
            Debug.Log("herbSO  not found" + herbname);
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
        ScrollSO scrollSO = allScrollSO.Find(t => t.enchantmentGemName == gemName);
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
            Debug.Log("Sagaic SO  not found" + sagaicNames);
        return null;
    }
    public GenGewgawSO GetGenGewgawSO(GenGewgawNames genGewgawName)
    {
        GenGewgawSO genGewgawSO = allGenGewgawSO.Find(t => t.genGewgawName == genGewgawName);
        if (genGewgawSO != null)
            return genGewgawSO;
        else
            Debug.Log(genGewgawName + "genGewGaw SO  not found");
        return null;
    }
    public PoeticGewgawSO GetPoeticGewgawSO(PoeticGewgawNames poeticGewgawName)
    {
        PoeticGewgawSO poeticGewgawSO = allPoeticGewgawSO.Find(t => t.poeticGewgawName == poeticGewgawName);
        if (poeticGewgawSO != null)
            return poeticGewgawSO;
        else
            Debug.Log(poeticGewgawName + "poeticGewgawSO not found" + poeticGewgawName);
        return null;
    }
    public PoeticSetSO GetPoeticSetSO(PoeticSetName poeticSetName)
    {
        PoeticSetSO poeticSetSO = allPoeticSetSO.Find(t => t.poeticSetName == poeticSetName);
        if (poeticSetSO != null)
            return poeticSetSO;
        else
            Debug.Log("poeticGewgawSO not found" + poeticSetName);
        return null;
    }
    public FoodSO GetFoodSO(FoodNames foodName)
    {
        FoodSO foodSO = allFoodSO.Find(t => t.foodName == foodName);
        if (foodSO != null)
            return foodSO;
        else
            Debug.Log("foodSO  not found" + foodName);
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
    public TGSO GetTradeGoodsSO(TGNames tgName)
    {
        TGSO tgSO = allTGSO.Find(t => t.tgName == tgName);
        if (tgSO != null)
            return tgSO;
        else
            Debug.Log("Trade goods SO  not found" + tgName);
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
    public MealsSO GetMealSO(MealNames mealName)
    {
        MealsSO mealSO = allMealsSO.Find(t => t.mealName == mealName);
        if (mealSO != null)
            return mealSO;
        else
            Debug.Log("meal  SO  not found" + mealName);
        return null;
    }
    public AlcoholSO GetAlcoholSO(AlcoholNames alcoholName)
    {
        AlcoholSO alcoholSO = allAlcoholSO.Find(t => t.alcoholName == alcoholName);
        if (alcoholSO != null)
            return alcoholSO;
        else
            Debug.Log("alcohol SO  not found" + alcoholName);
        return null;
    }
    public LoreBookSO GetLoreBookSO(LoreBookNames loreName)
    {
        LoreBookSO loreBookSO = allLoreBooksSO.Find(t => t.loreName == loreName);
        if (loreBookSO != null)
            return loreBookSO;
        else
            Debug.Log("Lore books SO  not found" + loreName);
        return null;
    }

    #endregion
    
    public List<PoeticGewgawSO> GetAllInAToolSet(PoeticSetName poeticSetName)
    {
        List<PoeticGewgawSO> allPoeticSO = new List<PoeticGewgawSO>(); 
        foreach (PoeticGewgawSO poeticSO in allPoeticGewgawSO)
        {
            if(poeticSO.poeticSetName == poeticSetName)
            {
                allPoeticSO.Add(poeticSO);
            }
        }
        return allPoeticSO;
    }


    public int GetRandomItem(ItemType itemtype)
    {
        int max = 0; 
        switch (itemtype)
        {
            case ItemType.None:
                max = 0;
                break;
            case ItemType.Potions:
                max = allPotionSO.Count; 
                break;
            case ItemType.GenGewgaws:
                max = allGenGewgawSO.Count;
                break;
            case ItemType.Herbs:
                max  = allHerbSO.Count;
                break;
            case ItemType.Foods:
                max = allFoodSO.Count;  
                break;
            case ItemType.Fruits:
                max = allFruitSO.Count; 
                break;
            case ItemType.Ingredients:
                max = allIngredSO.Count;
                break;
            case ItemType.XXX:
                break;
            case ItemType.Scrolls:
                max = allScrollSO.Count;
                break;
            case ItemType.TradeGoods:
                max = allTGSO.Count;
                break;
            case ItemType.Tools:
                max = allToolsSO.Count;
                break;
            case ItemType.Gems:
                max = allGemsSO.Count;
                break;
            case ItemType.Alcohol:
                max= allAlcoholSO.Count;
                break;
            case ItemType.Meals:
                max = allMealsSO.Count;
                break;
            case ItemType.SagaicGewgaws:
                max = sagaicGewgawSOs.Count;
                break;
            case ItemType.PoeticGewgaws:
                max = allPoeticSetSO.Count;
                break;
            case ItemType.RelicGewgaws:
                break;
            case ItemType.Pouches:
                break;
            case ItemType.LoreBooks:
                max = allLoreBooksSO.Count;
                break;
            default:
                max=  0; 
                break;         
        }
        int random = UnityEngine.Random.Range(0, max);
        return random;
    }


    #region PRICE GETTERS  
    public CostData GetCostData(ItemType itemType, int itemName)
    {
        CostData costData = null;
        switch (itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Potions:
                PotionSO potionSO = GetPotionSO((PotionNames)itemName);
                costData = new CostData(potionSO.cost, potionSO.fluctuation);
                break;
            case ItemType.GenGewgaws:
                GenGewgawSO genGewgawSO = GetGenGewgawSO((GenGewgawNames)itemName);
                costData = new CostData(genGewgawSO.cost, genGewgawSO.fluctuationRate);
                break;
            case ItemType.Herbs:
                HerbSO herbSO = GetHerbSO((HerbNames)itemName);
                costData = new CostData(herbSO.cost, herbSO.fluctuation);
                break;
            case ItemType.Foods:
                FoodSO foodSO = GetFoodSO((FoodNames)itemName);
                costData = new CostData(foodSO.cost, foodSO.fluctuation);
                break;
            case ItemType.Fruits:
                FruitSO fruitSO = GetFruitSO((FruitNames)itemName);
                costData = new CostData(fruitSO.cost, fruitSO.fluctuation);
                break;
            case ItemType.Ingredients:
                IngredSO ingredSO = GetIngredSO((IngredNames)itemName);
                costData = new CostData(ingredSO.cost, ingredSO.fluctuation);
                break;
            case ItemType.XXX:
                break;
            case ItemType.Scrolls:
                ScrollSO scrollSO = GetScrollSO((ScrollNames)itemName);
                costData = new CostData(scrollSO.cost, scrollSO.fluctuation);
                break;
            case ItemType.TradeGoods:
                TGSO tgSO = GetTradeGoodsSO((TGNames)itemName);
                costData = new CostData(tgSO.cost, tgSO.fluctuation);
                break;
            case ItemType.Tools:
                ToolsSO toolSO = GetToolSO((ToolNames)itemName);
                costData = new CostData(toolSO.cost, toolSO.fluctuation);
                break;
            case ItemType.Teas:
                break;
            case ItemType.Soups:
                break;
            case ItemType.Gems:
                GemSO gemSO = GetGemSO((GemNames)itemName);
                costData = new CostData(gemSO.cost.DeepClone(), gemSO.fluctuation);
                break;
            case ItemType.Alcohol:
                break;
            case ItemType.Meals:
                break;
            case ItemType.SagaicGewgaws:
                SagaicGewgawSO sagaicGewgawSO = GetSagaicGewgawSO((SagaicGewgawNames)itemName);
                costData = new CostData(sagaicGewgawSO.cost, sagaicGewgawSO.fluctuation);
                break;
            case ItemType.PoeticGewgaws:
                PoeticGewgawSO poeticGewgawSO = GetPoeticGewgawSO((PoeticGewgawNames)itemName);
                costData = new CostData(poeticGewgawSO.cost, poeticGewgawSO.fluctuationRate);
                break;
            case ItemType.RelicGewgaws:
                break;
            case ItemType.Pouches:

                break;
            case ItemType.LoreBooks:
                LoreBookSO loreBookSO = GetLoreBookSO((LoreBookNames)itemName);
                costData = new CostData(loreBookSO.cost, loreBookSO.fluctuation);
                break;
            default:
                break;

        }
        return costData;
    }

    #endregion

}
