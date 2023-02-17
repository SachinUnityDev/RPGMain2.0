using System.Collections;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    public class ItemCardView : MonoBehaviour
    {
        [Header("Item View SO NTBR")]
        [SerializeField] ItemViewSO itemViewSO; 

        [Header("Tranform NTBR ")]
        [SerializeField] Transform crownTrans;
        [SerializeField] Transform topTrans;
        [SerializeField] Transform midTrans;
        [SerializeField] Transform btmTrans;

        [Header("Crown")]
        [SerializeField] TextMeshProUGUI resHeading;
        [SerializeField] TextMeshProUGUI resLS;

        [Header("Top")]
        [SerializeField] TextMeshProUGUI itemNametxt;
        [SerializeField] TextMeshProUGUI itemTypetxt;
        [SerializeField] Image itemFilterImg; 
        [SerializeField] Image itemImg;

        [Header("Mid")]
        [SerializeField] Transform itemSubTypeTrans;

        [Header("btm")]
        [SerializeField] Transform itemSlotTrans;
        [SerializeField] Transform currTrans;

        [Header("Tail")]
        [SerializeField] Transform tailTrans; 


   
        void Awake()
        {
            gameObject.SetActive(false);
            crownTrans = transform.GetChild(0);
            topTrans = transform.GetChild(1);   
            midTrans = transform.GetChild(2);
            btmTrans = transform.GetChild(3);
            tailTrans = transform.GetChild(4);

            // crown
            resHeading = crownTrans.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            resLS = crownTrans.GetChild(1).GetComponent<TextMeshProUGUI>();

            //top
            itemNametxt = topTrans.GetChild(0).GetComponent<TextMeshProUGUI>();
            itemTypetxt = topTrans.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            itemFilterImg = topTrans.GetChild(1).GetChild(1).GetComponent<Image>();
            itemSubTypeTrans = topTrans.GetChild(2);
            itemImg = topTrans.GetChild(3).GetComponent<Image>();

            // btm     
            currTrans = btmTrans.GetChild(0);   
            itemSlotTrans = btmTrans.GetChild(1);   



            
        }
        void FillMid(List<string> allLines)
        {
            int i = 0; 
            foreach(Transform child in midTrans)
            {
                if(i< allLines.Count)
                {
                    child.gameObject.SetActive(true);
                    child.GetComponent<TextMeshProUGUI>().text = allLines[i];
                    i++;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        public void ShowItemCard(Iitems item)
        {
            gameObject.SetActive(true);
            tailTrans.gameObject.SetActive(false);   
            itemViewSO = ItemService.Instance.itemViewSO; 
            switch (item.itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Potions:
                    PotionSO potionSO = ItemService.Instance.GetPotionSO((PotionNames)item.itemName);
                    if (potionSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);  
                        // top
                        itemNametxt.text = potionSO.potionName.ToString().CreateSpace();
                        itemTypetxt.text = "Potion";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN; 
                        itemImg.sprite = potionSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(potionSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);                          
                        currTrans.GetComponent<DisplayCurrency>().Display(potionSO.cost.DeepClone()); 
                       
                    }
                    break;
                case ItemType.GenGewgaws:
                    GenGewgawSO genGewgawSO = ItemService.Instance.GetGenGewgawSO((GenGewgawNames)item.itemName);
                    if (genGewgawSO != null)
                    {
                       
                        crownTrans.gameObject.SetActive(true);
                        resHeading.text = genGewgawSO.GetRestrictionsType(); 
                        resLS.text = genGewgawSO.GetRestrictionLs();

                        // top
                        itemNametxt.text = genGewgawSO.genGewgawName.ToString().CreateSpace();
                        itemTypetxt.text = "Gewgaw";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = genGewgawSO.iconSprite;

                        itemSubTypeTrans.gameObject.SetActive(true);

                        GenGewgawBase genGewgawBase = item as GenGewgawBase;

                        GenGewgawQ genQ = genGewgawBase.genGewgawQ; 
                        itemSubTypeTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                                                    = genQ.ToString();


                        transform.GetComponent<Image>().sprite = itemViewSO.GetBGSprite(genQ);
                       //mid
                       List<string> allLines = new List<string>();
                        if (genGewgawBase?.prefixBase != null)
                        {
                            if (genGewgawBase.prefixBase.displayStrs.Count > 0)
                                allLines.AddRange(genGewgawBase.prefixBase.displayStrs);
                        }
                        if (genGewgawBase?.suffixBase != null)
                        {
                            if (genGewgawBase.suffixBase.displayStrs.Count > 0)
                                allLines.AddRange(genGewgawBase.suffixBase.displayStrs);
                        }
                        
                        FillMid(allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(genGewgawSO.cost.DeepClone());
                    }
                    // subtypes 
                    break;
                case ItemType.Herbs:
                    HerbSO herbSO = ItemService.Instance.GetHerbSO((HerbNames)item.itemName);
                    if (herbSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = herbSO.herbName.ToString().CreateSpace();
                        itemTypetxt.text = "Herb";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = herbSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(herbSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(herbSO.cost.DeepClone());

                    }
                    break;
                case ItemType.Foods:
                    FoodSO foodSO = ItemService.Instance.GetFoodSO((FoodNames)item.itemName);
                    if (foodSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = foodSO.foodName.ToString().CreateSpace();
                        itemTypetxt.text = "Food";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = foodSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(foodSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(foodSO.cost.DeepClone());
                    }
                    break;
                case ItemType.Fruits:
                    FruitSO fruitSO = ItemService.Instance.GetFruitSO((FruitNames)item.itemName);
                    if (fruitSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = fruitSO.fruitName.ToString().CreateSpace();
                        itemTypetxt.text = "Fruit";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = fruitSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(fruitSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(fruitSO.cost.DeepClone());

                    }
                    break;
                case ItemType.Ingredients:
                    IngredSO ingredSO = ItemService.Instance.GetIngredSO((IngredNames)item.itemName);
                    if (ingredSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = ingredSO.ingredName.ToString().CreateSpace();
                        itemTypetxt.text = "Ingredient";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = ingredSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(ingredSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(ingredSO.cost.DeepClone());
                    }
                    break;
                case ItemType.XXX:
                    break;
                case ItemType.Scrolls:
                    ScrollSO scrollSO = ItemService.Instance.GetScrollSO((ScrollNames)item.itemName);
                    if (scrollSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = scrollSO.scrollName.ToString().CreateSpace();
                        itemTypetxt.text = "Scroll";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = scrollSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(scrollSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(scrollSO.cost.DeepClone());
                    }
                    break;
                case ItemType.TradeGoods:  // start from here
                    TGSO tgSO = ItemService.Instance.GetTradeGoodsSO((TGNames)item.itemName);
                    if (tgSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = tgSO.tgName.ToString().CreateSpace();
                        itemTypetxt.text = " Trade Goods";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = tgSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(tgSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(tgSO.cost.DeepClone());

                    }
                    break;
                case ItemType.Tools:
                    ToolsSO toolSO = ItemService.Instance.GetToolSO((ToolNames)item.itemName);
                    if (toolSO != null)
                    {
                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG;
                        crownTrans.gameObject.SetActive(false);
                        // top
                        itemNametxt.text = toolSO.toolName.ToString().CreateSpace();
                        itemTypetxt.text = " Tools";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = toolSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(false);
                        //mid
                        FillMid(toolSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        Currency curr = toolSO.cost.DeepClone();
                        currTrans.GetComponent<DisplayCurrency>().Display(curr);
                    }
                    break;
                case ItemType.Teas:
                    break;
                case ItemType.Soups:
                    break;
                case ItemType.Gems:
                    GemSO gemSO = ItemService.Instance.GetGemSO((GemNames)item.itemName);
                    if (gemSO != null)
                    {
                        crownTrans.gameObject.SetActive(false);   
                        // top
                        itemNametxt.text = gemSO.gemName.ToString().CreateSpace();
                        itemTypetxt.text = "Gem";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = gemSO.iconSprite;
                        itemSubTypeTrans.gameObject.SetActive(true);

                        itemSubTypeTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                                                    = gemSO.gemType.ToString().CreateSpace();


                        transform.GetComponent<Image>().sprite = itemViewSO.lyricBG; 
                        //mid                       
                 
                        FillMid(gemSO.allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);                      
                        currTrans.GetComponent<DisplayCurrency>().Display(gemSO.cost.DeepClone());
                    }
                    // sub types 
                    break;
                case ItemType.Alcohol:
                    break;
                case ItemType.Meals:
                    break;
                case ItemType.SagaicGewgaws:
                    SagaicGewgawSO sagaicGewgawSO = ItemService.Instance.GetSagaicGewgawSO((SagaicGewgawNames)item.itemName);
                    if (sagaicGewgawSO != null)
                    {
                        crownTrans.gameObject.SetActive(false);                      

                        // top
                        itemNametxt.text = sagaicGewgawSO.sagaicGewgawName.ToString().CreateSpace();
                        itemTypetxt.text = "Gewgaw";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = sagaicGewgawSO.iconSprite;

                        itemSubTypeTrans.gameObject.SetActive(true);
                        itemSubTypeTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                                                    = "Sagaic";

                        transform.GetComponent<Image>().sprite = itemViewSO.sagaicBG;
                        //mid
                        FillMid(sagaicGewgawSO.allLines);

                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(sagaicGewgawSO.cost.DeepClone());
                    }
                    break;
                case ItemType.PoeticGewgaws:
                    PoeticGewgawSO poeticGewgawSO = ItemService.Instance.GetPoeticGewgawSO((PoeticGewgawNames)item.itemName);
                    if (poeticGewgawSO != null)
                    {
                        crownTrans.gameObject.SetActive(false);

                        // top
                        itemNametxt.text = poeticGewgawSO.gewgawMidName.ToString().CreateSpace();
                        itemTypetxt.text = "Gewgaw";
                        itemFilterImg.sprite = itemViewSO.GetItemTypeImgData(item.itemType).FilterIconN;
                        itemImg.sprite = poeticGewgawSO.iconSprite;

                        itemSubTypeTrans.gameObject.SetActive(true);
                        itemSubTypeTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                                                                 = "Poetic";

                        transform.GetComponent<Image>().sprite = itemViewSO.poeticBG;
                        // mid
                        PoeticGewgawBase poeticBase = item as PoeticGewgawBase; 
                        FillMid(poeticBase.displayStrs);

                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(poeticGewgawSO.cost.DeepClone());

                        //tail
                        tailTrans.gameObject.SetActive(true);
                        tailTrans.GetChild(0).GetComponent<TextMeshProUGUI>().text
                                                = poeticGewgawSO.poeticSetName.ToString().CreateSpace();
                        // get poetic SO Set and get strings 
                        PoeticSetSO poeticSetSO = ItemService.Instance.GetPoeticSetSO(poeticGewgawSO.poeticSetName);
                         
                        for (int i = 0; i < poeticSetSO.bonusBuffStr.Count; i++)
                        {                        
                            tailTrans.GetChild(1).GetChild(i).GetComponent<TextMeshProUGUI>().text
                                    = poeticSetSO.bonusBuffStr[i];
                        }
                        foreach (Transform child in tailTrans.GetChild(1))
                        {
                            int index = child.GetSiblingIndex(); 
                            if(index < poeticSetSO.bonusBuffStr.Count)
                            {
                                child.gameObject.SetActive(true);
                            }
                            else
                            {
                                child.gameObject.SetActive(false);
                            }
                        }

                        // TAIL verses
                        int setNum = poeticGewgawSO.setNumber;
                        tailTrans.GetChild(2).GetComponent<TextMeshProUGUI>().text
                            = "Verse" + setNum + ": " + poeticGewgawSO.verseDesc;

                    }
                    // write Poetic
                    break;
                case ItemType.RelicGewgaws:
                    // write Relic 
                    break;
                case ItemType.Pouches:
                    break;

                default:
                    break;
            }

        }

    }
}