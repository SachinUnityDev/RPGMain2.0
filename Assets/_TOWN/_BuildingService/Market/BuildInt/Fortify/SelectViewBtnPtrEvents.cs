using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Town
{


    public class SelectViewBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("To be ref")]
        [SerializeField] Sprite btnN;
        [SerializeField] Sprite btnHL;
        [SerializeField] TextMeshProUGUI txt;

        [Header("Not be ref")]
        Image btnImg;

        void Awake()
        {
            btnImg = GetComponent<Image>();
            txt = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            txt.gameObject.SetActive(false);
        }

        void SetSpriteN()
        {
            btnImg.sprite = btnN;
            txt.gameObject.SetActive(false);
        }
        void SetSpriteHL()
        {
            btnImg.sprite = btnHL;
            txt.gameObject.SetActive(true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetSpriteHL();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetSpriteN();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetSpriteN();
        }
    }
}