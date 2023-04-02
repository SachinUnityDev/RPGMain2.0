using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Town
{

    public class FortifyBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnDisabled;
        [SerializeField] Transform costDisplay;



        [SerializeField] CharNames charSelect;
        [SerializeField] ArmorModel armorModel;
        [SerializeField] FortifyView fortifyView;
        Currency fortifyCost;
        

        public void InitFortifyBtn(CharNames charSelect, ArmorModel armorModel, FortifyView fortifyView)
        {
            this.charSelect = charSelect;
            this.armorModel = armorModel;   
            this.fortifyView= fortifyView;
            LocationName locName = TownService.Instance.townModel.currTown;
            fortifyCost = armorModel.GetFortifyCost(locName).DeepClone(); // get build upgrading
            costDisplay.GetChild(1).GetComponent<DisplayCurrency>().Display(fortifyCost); 
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            ArmorService.Instance.OnArmorFortifyPressed(charSelect, armorModel); 
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
           
        }
    }
}