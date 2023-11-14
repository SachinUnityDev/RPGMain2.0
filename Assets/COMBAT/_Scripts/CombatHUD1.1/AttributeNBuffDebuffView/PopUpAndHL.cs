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

        public AttribName attribName;
        
        [Header("Sprite lit and Normal")]
        public Sprite spriteNormal;
        public Sprite spriteLit;

        [Header("Sprite lit and Normal COLOR")]
        public Color colorBuff;
        public Color colorDebuff;
        public Color colorN; 



        [Header("Pop up Anim related")]
        RectTransform rectTranform;
        float animTime = 0.15f;
        float scaleValue = 1.25f;
        Image img;
        TextMeshProUGUI attribValTxt;
        [Header("Attribute cards related")]
        [SerializeField] GameObject attriCard;

        [Header("Attribute Data")]
        [SerializeField] AttribData attribData;
        [SerializeField] bool isBuffed; 

        void Start()
        {
            rectTranform = gameObject.GetComponent<RectTransform>();
            img = gameObject.GetComponentInChildren<Image>();
            attribValTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            rectTranform.DOScale(1, animTime);
            attriCard.SetActive(false);
        }

        public void InitAttrib(CharController charController)
        {
            attribData = charController.GetAttrib(attribName);
          
                                          
            if (attribName == AttribName.dmgMax)
            {
                AttribData attribDataMin= charController.GetAttrib(AttribName.dmgMin);
                attribValTxt.text = attribDataMin.currValue.ToString() +"-"+attribData.currValue.ToString();
                

            }else if(attribName == AttribName.armorMax)
            {
                AttribData attribDataMin = charController.GetAttrib(AttribName.armorMin);
                attribValTxt.text = attribDataMin.currValue.ToString() +"-"+attribData.currValue.ToString();
               
            }
            else
            {
                attribValTxt.text = attribData.currValue.ToString(); 
            }
            img.sprite = spriteNormal;
            ChgColorBasedOnBuffDebuff();
        }
        void ChgColorBasedOnBuffDebuff()
        {
            if(attribData.baseValue < attribData.currValue)
            {  // isbuffed
                attribValTxt.color = colorBuff; 
            }
            if (attribData.baseValue > attribData.currValue)
            {// is debuffed
                attribValTxt.color = colorDebuff;
            }
            if (attribData.baseValue == attribData.currValue)
            {
                attribValTxt.color = colorN;
            }
        }


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
            string desc = attribData.desc; 
            attriCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                        = attribName.AttribStrName(); 
            attriCard.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                        = desc; 
        }

        void ShowAttributeCard()
        {  
            RectTransform attriCardRect = attriCard.GetComponent<RectTransform>();
            attriCardRect.DOMoveY(rectTranform.position.y, 0.1f);
            attriCard.SetActive(true);

        }


       
    }


}

