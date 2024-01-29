using Common;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Combat
{
    public class FortCircleView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {


        [SerializeField] TextMeshProUGUI fortText;
        [SerializeField] Color colorFort;
        [SerializeField] Color colorFortOrg;
        CharController charController; 
        void Start()
        {
            CombatEventService.Instance.OnCharClicked += FillFort;
           
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnCharClicked -= FillFort;
        }

        public void FillFort(CharController charController)
        {   
            this.charController= charController;
            StatData fortData = charController.GetStat(StatName.fortitude);
            string sign = fortData.currValue > 0 ? "+" : "-"; 
            sign = fortData.currValue == 0 ? "" : sign;
            float val = (fortData.currValue - fortData.minLimit)/ (fortData.maxLimit - fortData.minLimit); 

            transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = val;
            fortText.color = colorFort; 
            fortText.text =$"{sign}{fortData.currValue}";
        }

        void FillFortOrg()
        {
            AttribData fortOrgData = charController.GetAttrib(AttribName.fortOrg); 

            string sign = fortOrgData.currValue > 0 ? "+" : "-";
            sign = fortOrgData.currValue == 0 ? "" : sign;
            fortText.color = colorFortOrg; 
            fortText.text = $"{sign}{fortOrgData.currValue}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            FillFortOrg();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            FillFort(charController); 
        }
    }
}