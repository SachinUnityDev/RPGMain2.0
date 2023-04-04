using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Common; 

namespace Combat
{
    public class PopUpAndHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public AttribName statName;
        public string desc = ""; 

        [Header("Sprite lit and Normal")]
        public Sprite spriteNormal;
        public Sprite spriteLit;

        [Header("Pop up Anim related")]
        RectTransform rectTranform;
        float animTime = 0.15f;
        float scaleValue = 1.25f;
        Image img;

        [Header("Attribute cards related")]
        [SerializeField] GameObject attriCard; 

        public void OnPointerEnter(PointerEventData eventData)
        {
            rectTranform.DOScale(scaleValue, animTime);
            img.sprite = spriteLit;

            FillAttributeCard(); 
            ShowAttributeCard(); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rectTranform.DOScale(1, animTime);
            img.sprite = spriteNormal;
            attriCard.SetActive(false);

        }

        void FillAttributeCard()
        {
            int index = transform.GetSiblingIndex()+1;
            //CharController charController = CombatService.Instance.currCharClicked;
            //string desc = charController.charModel.statsList.Find(t=>t.)
            attriCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                        = statName.RealName(); 
            attriCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                        = desc; 
        }

        void ShowAttributeCard()
        {  

            RectTransform rectTransform = transform.GetComponent<RectTransform>();
          
            RectTransform attriCardRect = attriCard.GetComponent<RectTransform>();
            attriCardRect.DOMoveY(rectTranform.position.y, 0.1f);
            attriCard.SetActive(true);

        }


        void Start()
        {
            rectTranform = gameObject.GetComponent<RectTransform>();
            img = gameObject.GetComponentInChildren<Image>(); 
            rectTranform.DOScale(1, animTime);
            attriCard.SetActive(false);
        }
        
    }


}

