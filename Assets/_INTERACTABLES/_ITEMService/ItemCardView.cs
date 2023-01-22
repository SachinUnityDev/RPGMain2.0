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

        //[Header("Crown top")]
        //[SerializeField] TextMeshProUGUI headingTxt;
        //[SerializeField] TextMeshProUGUI restrictionsList; 
        void Start()
        {
            crownTrans = transform.GetChild(0);
            topTrans = transform.GetChild(1);   
            midTrans = transform.GetChild(2);
            btmTrans = transform.GetChild(3);

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
                        currTrans.GetComponent<DisplayCurrency>().Display(potionSO.cost); 
                       
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
                        if (genGewgawBase.prefixBase != null)
                        {
                            if (genGewgawBase.prefixBase.displayStrs.Count > 0)
                                allLines.AddRange(genGewgawBase.prefixBase.displayStrs);
                        }
                        if (genGewgawBase.suffixBase != null)
                        {
                            if (genGewgawBase.suffixBase.displayStrs.Count > 0)
                                allLines.AddRange(genGewgawBase.suffixBase.displayStrs);
                        }
                        
                        FillMid(allLines);
                        //btm
                        itemSlotTrans.gameObject.SetActive(false);
                        currTrans.GetComponent<DisplayCurrency>().Display(genGewgawSO.cost);

                    }


                    // subtypes 
                    break;
                case ItemType.Herbs:
                    break;
                case ItemType.Foods:
                    break;
                case ItemType.Fruits:
                    break;
                case ItemType.Ingredients:
                    break;
                case ItemType.XXX:
                    break;
                case ItemType.Scrolls:
                    break;
                case ItemType.TradeGoods:
                    break;
                case ItemType.Tools:
                    break;
                case ItemType.Teas:
                    break;
                case ItemType.Soups:
                    break;
                case ItemType.Gems:
                    // sub types 
                    break;
                case ItemType.Alcohol:
                    break;
                case ItemType.Meals:
                    break;
                case ItemType.SagaicGewgaws:
                    // write sagaic
                    break;
                case ItemType.PoeticGewgaws:
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