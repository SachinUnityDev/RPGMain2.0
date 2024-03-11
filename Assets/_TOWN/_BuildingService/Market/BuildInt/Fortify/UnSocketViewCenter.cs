using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Common; 
namespace Town
{
    public class UnSocketViewCenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        UnSocketView UnSocketView;

        [Header(" Armor Slot TBR")]
        [SerializeField] Transform armorSlotTrans;

        [Header("img NTBR")]
        [SerializeField] Image armorImg;

        FortifyView fortifyView;
        [SerializeField] ArmorModel armorModel;
        [SerializeField] CharController selectCharCtrl;


        [SerializeField] Transform leftCharPanel;
        [SerializeField] Transform rightGemPanel;

        [SerializeField] Transform slot1;
        [SerializeField] Transform slot2;
        [SerializeField] Transform slot3;


        [Header("Global Variables")]
        ItemModel itemModel; 
        private void Awake()
        {
            armorImg = transform.GetChild(0).GetComponent<Image>();
        }
        public void InitUnSocketCenter(CharController charController, ArmorModel armorModel, UnSocketView UnSocketView)
        {
            this.UnSocketView= UnSocketView;    
            this.armorModel = armorModel;
            this.selectCharCtrl = charController;

            ArmorSO armorSO = ArmorService.Instance.allArmorSO.GetArmorSOWithCharName(charController.charModel.charName);
            armorImg.sprite = armorSO.GetArmorSprite(charController.charModel.charName);
            FillGemSlots();      
        }
        void FillGemSlots()
        {
            CharController charController = selectCharCtrl; 
            ItemController itemController = charController.itemController;
            itemModel = itemController.itemModel;
            for (int i = 0; i < 2; i++)
            {
                Iitems item = itemModel?.divItemsSocketed[i]; 
                if(item != null)
                {                   
                    if (i == 0)
                    {
                        FillGemSlot(slot1, item, true); 
                    }
                    if (i == 1)
                    {
                        FillGemSlot(slot2, item, true);
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        FillGemSlot(slot1, item, false);
                    }
                    if (i == 1)
                    {
                        FillGemSlot(slot2, item, false);
                    }
                }
            }
            Iitems itemSupport = itemModel.supportItemSocketed;
            if (itemSupport!= null)
            {
                FillGemSlot(slot3, itemSupport, true);
            }
            else
            {
                FillGemSlot(slot3, itemSupport, false);
            }
        }

        void FillGemSlot(Transform slot, Iitems item, bool notEmpty)
        {
            if (notEmpty)
            {
                slot.GetChild(0).gameObject.SetActive(notEmpty);
                GemBase gembase = item as GemBase;
                GemSO gemSO = ItemService.Instance.allItemSO.GetGemSO(gembase.gemName);
                slot.GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
            }
            else
            {
                slot.GetChild(0).gameObject.SetActive(notEmpty);
            }
        }
        void ShowRightLeftPanel()
        {
            rightGemPanel.gameObject.SetActive(true);
            leftCharPanel.gameObject.SetActive(true);

            //leftCharPanel.GetComponent<LeftEnchantPanelView>().InitLeftPanel(selectChar, armorModel);
            //rightGemPanel.GetComponent<RightEnchantPanelView>().InitRightPanel(selectChar, armorModel);
        }
        void HideRightLeftPanel()
        {
            rightGemPanel.gameObject.SetActive(false);
            leftCharPanel.gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            //  ShowRightLeftPanel();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //HideRightLeftPanel();
        }





    }
}