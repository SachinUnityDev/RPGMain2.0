using Common;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Town
{
    public class HealBtnPtrEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler  
    {
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] Sprite btnNA;

        HealView healView; 
        TempTraitController tempTraitController;
        [SerializeField] CharNames charName;

        [SerializeField] Transform healtxtTrans;

        [Header("Global var")]
        [SerializeField] bool isClickable;
        [SerializeField] Image img;

        public void InitBtnEvents(CharNames charName, TempTraitController tempTraitController, HealView healView)
        {
            healtxtTrans = transform.GetChild(0);
            healtxtTrans.gameObject.SetActive(false);
            img = GetComponent<Image>();
            this.healView = healView;
            this.tempTraitController = tempTraitController;
            this.charName = charName;

        }
        public void SetState(bool isClickable)
        {
            this.isClickable = isClickable;
            SetImg();
        }
        void SetImg()
        {
            img = GetComponent<Image>();
            if (isClickable)
            {
                img.sprite = btnN;
            }
            else
            {
                img.sprite = btnNA;
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClickable)
            {
                healView.OnHealBtnPressed();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            healtxtTrans.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            healtxtTrans.gameObject.SetActive(false);
        }
    }
}