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

        void Start()
        {

        }

        public void InitFortifyBtn(CharNames charSelect, ArmorModel armorModel)
        {
            this.charSelect = charSelect;
            this.armorModel = armorModel;   

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