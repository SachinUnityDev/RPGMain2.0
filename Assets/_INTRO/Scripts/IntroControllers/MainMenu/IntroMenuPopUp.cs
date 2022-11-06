using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;


namespace Intro
{
    public class IntroMenuPopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI btnTxt;
        [SerializeField] MainMenuController mainMenuController; 
        [Header("Sprite lit and Normal")]
        public Sprite spriteNormal;
        public Sprite spriteLit;

        [Header("Pop up Anim related")]
        RectTransform rectTranform;
        float animTime = 0.15f;
        float scaleValue = 1.25f;
        Image img;

        public void OnPointerEnter(PointerEventData eventData)
        {
            rectTranform.DOScale(scaleValue, animTime);
          //  img.sprite = spriteLit;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rectTranform.DOScale(1, animTime);
            img.sprite = spriteNormal;
        }

        void Start()
        {
            btnTxt = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            rectTranform = btnTxt.GetComponent<RectTransform>();
            img = gameObject.GetComponentInChildren<Image>();
            rectTranform.DOScale(1, animTime);
            spriteNormal = img.sprite; // pre ref in editor
        }

        
    }


}


