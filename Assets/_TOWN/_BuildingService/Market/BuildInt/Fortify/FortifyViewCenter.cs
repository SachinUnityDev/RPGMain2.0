using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
namespace Town
{
    public class FortifyViewCenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header(" Armor Slot TBR")]
        [SerializeField] Transform armorSlotTrans;

        [Header("img NTBR")]
        [SerializeField] Image armorImg;

        FortifyView fortifyView;
        [SerializeField] ArmorModel armorModel;
        [SerializeField] CharController charController;



        [SerializeField] Transform leftCharPanel;
        //[SerializeField] Transform rightGemPanel;

     

        private void Awake()
        {
            armorImg = transform.GetChild(0).GetComponent<Image>();
        }
        public void InitFortifyPanel(CharController charController, ArmorModel armorModel, FortifyView fortifyView)
        {
            this.fortifyView = fortifyView;
            this.armorModel = armorModel;
            this.charController = charController;

            ArmorSO armorSO = ArmorService.Instance.allArmorSO.GetArmorSOWithCharName(charController.charModel.charName);
            armorImg.sprite = armorSO.GetArmorSprite(charController.charModel.charName);

            //if (armorModel.weaponState == WeaponState.Enchanted || armorModel.weaponState == WeaponState.Rechargeable)
            //{
               // armorSlotTrans.GetChild(0).gameObject.SetActive(true);
                //GemSO gemSO = ItemService.Instance.GetGemSO(armorModel.gemName);
                //armorSlotTrans.GetChild(0).GetComponent<Image>().sprite = gemSO.iconSprite;
            //}
            //else
            //{
            //    armorSlotTrans.GetChild(0).gameObject.SetActive(false);
            //}
        }

        void ShowRightLeftPanel()
        {
            //rightGemPanel.gameObject.SetActive(true);
            leftCharPanel.gameObject.SetActive(true);
            leftCharPanel.GetComponent<LeftPanelArmorView>().InitLeftPanel(charController, armorModel);
            //rightGemPanel.GetComponent<RightEnchantPanelView>().InitRightPanel(selectChar, armorModel);
        }
        void HideRightLeftPanel()
        {
            //rightGemPanel.gameObject.SetActive(false);
            leftCharPanel.gameObject.SetActive(false);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowRightLeftPanel();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideRightLeftPanel();
        }




    }
}