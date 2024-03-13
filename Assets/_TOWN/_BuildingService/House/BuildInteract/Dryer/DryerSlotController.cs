using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using Spine;

namespace Town
{
    public class DryerSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        [SerializeField] int index = 0;
        [SerializeField] int sibIndex = 0;

        List<Iitems> ItemsInSlot = new List<Iitems>();
        [SerializeField] int itemCount =0; 
        DryerView dryerView;
        DryerSlotView dryerSlotView;

        [SerializeField] Image frameImg; 

        [Header("Bg Sprite")]
        [SerializeField] Sprite BGSpriteN;
        [SerializeField] Sprite BGSpriteNA;

        [Header("N/NA Sprite Color")]
        [SerializeField] Color spriteColorN;
        [SerializeField] Color spriteColorNA;

        [SerializeField] Transform OnHoverTrans;
        
        private void OnEnable()
        {
            frameImg = transform.GetChild(3).GetComponent<Image>();
            frameImg.gameObject.SetActive(false);
           // OnHoverTrans.gameObject.SetActive(false);
        }
        public void InitSlot(DryerView dryerView, DryerSlotView dryerSlotView, int index)
        {
            this.index = index;
           sibIndex = transform.GetSiblingIndex();
            transform.gameObject.SetActive(true);
            List<Iitems> allFoods =
                InvService.Instance.invMainModel.GetAllItemsInCommOrStash(ItemType.Foods);
            List<Iitems> allFruits =
               InvService.Instance.invMainModel.GetAllItemsInCommOrStash(ItemType.Fruits);
            List<Iitems> allItems = new List<Iitems>();

            ClearSlot();
            if (index == 1) // meat
            {// venison 

                switch (sibIndex)
                {
                    case 0:
                        allItems = allFoods.Where(t => (FoodNames)t.itemName == FoodNames.Venison).ToList();
                        break;
                    case 1:
                        allItems = allFoods.Where(t => (FoodNames)t.itemName == FoodNames.Mutton).ToList();
                        break;
                    case 2:
                        allItems = allFoods.Where(t => (FoodNames)t.itemName == FoodNames.Beef).ToList();
                        break;
                    default:
                        break;
                }
                if (allItems.Count > 0)
                {
                    foreach (Iitems item in allItems)
                    {
                        AddItemOnSlot(item);
                    }
                }
            } else if (index == 2)
            {
                if (sibIndex == 1 || sibIndex ==2)
                {
                   transform.gameObject.SetActive(false);
                }
                allItems = allFoods.Where(t => (FoodNames)t.itemName == FoodNames.Fish).ToList();

                if (allItems.Count > 0)
                {
                    foreach (Iitems item in allItems)
                    {
                        AddItemOnSlot(item);
                    }
                }
            } else if (index == 3)
            {
                if (sibIndex == 1 || sibIndex == 2)
                {
                    transform.gameObject.SetActive(false);
                }
                allItems = allFruits.Where(t => (FruitNames)t.itemName == FruitNames.Grape).ToList();

                if (allItems.Count > 0)
                {
                    foreach (Iitems item in allItems)
                    {
                        AddItemOnSlot(item);
                    }
                }
            }
            OnHoverTrans = transform.GetChild(2);
            this.dryerView = dryerView;
            this.dryerSlotView = dryerSlotView;          
        }
        public void OnFrameSelect(int sibIndex)
        {
            if(this.sibIndex == sibIndex)
            {
                frameImg.gameObject.SetActive(true);
            }
            else
            {
                frameImg.gameObject.SetActive(false);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //if (ItemsInSlot.Count == 0) return; 
            //OnHoverTrans.GetComponentInChildren<TextMeshProUGUI>().text = ItemsInSlot[0].itemName
            //OnHoverTrans.gameObject.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            //OnHoverTrans.gameObject.SetActive(false);
        }
        void AddItemOnSlot(Iitems item)
        {
            item.invSlotType = SlotType.ExcessInv;
            ItemsInSlot.Add(item);
            item.slotID = transform.GetSiblingIndex();
            itemCount++;

            RefreshImg(item);
            RefreshSlotTxt();
        }

        void RefreshImg(Iitems item)
        {
            for (int i = 0; i < gameObject.transform.GetChild(0).childCount - 1; i++)
            {
                Destroy(gameObject.transform.GetChild(0).GetChild(i).gameObject);
            }
            transform.GetComponent<Image>().sprite = GetBGSprite(item);

            Transform ImgTrans = gameObject.transform.GetChild(0).GetChild(0);
            ImgTrans.GetComponent<Image>().sprite = GetSprite(item);
            ImgTrans.gameObject.SetActive(true);
        }
        void RefreshSlotTxt()
        {
            Transform txttrans = gameObject.transform.GetChild(1);

            if (ItemsInSlot.Count > 1)
            {
                txttrans.gameObject.SetActive(true);
                txttrans.GetComponentInChildren<TextMeshProUGUI>().text = ItemsInSlot.Count.ToString();
            }
            else
            {
                txttrans.gameObject.SetActive(false);
            }
        }
        Sprite GetSprite(Iitems item)
        {
            Sprite sprite = InvService.Instance.InvSO.GetSprite(item.itemName, item.itemType);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;
        }
        Sprite GetBGSprite(Iitems item)
        {
            Sprite sprite = InvService.Instance.InvSO.GetBGSprite(item);
            if (sprite != null)
                return sprite;
            else
                Debug.Log("SPRITE NOT FOUND");
            return null;

        }   
        public void ClearSlot()
        {
            ItemsInSlot.Clear();
            itemCount = 0;
            if (IsEmpty())
            {
                Transform ImgTrans = gameObject.transform.GetChild(0).GetChild(0);
                ImgTrans.gameObject.SetActive(false);
                gameObject.GetComponent<Image>().sprite = InvService.Instance.InvSO.emptySlot;
                RefreshSlotTxt();
            }
            frameImg.gameObject.SetActive(false);
        }
        public bool IsEmpty()
        {
            if (ItemsInSlot.Count > 0)
                return false;
            else
                return true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(itemCount== 0) return;
            dryerSlotView.OnSlotSelect(ItemsInSlot[0], sibIndex); 
        }
    }

}