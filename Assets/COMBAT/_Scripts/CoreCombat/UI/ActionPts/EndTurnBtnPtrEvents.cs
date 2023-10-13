using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Combat
{
    public class EndTurnBtnPtrEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHovered;
        [SerializeField] Sprite spriteClicked;

        Image img;
        [SerializeField] TextMeshProUGUI descTxt; 
        public void OnPointerClick(PointerEventData eventData)
        {
            img.sprite = spriteClicked;
            SkillService.Instance.Move2Nextturn(); // on End turn clicked
            descTxt.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           descTxt.gameObject.SetActive(true);
            img.sprite = spriteHovered; 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            descTxt.gameObject.SetActive(false);
            img.sprite = spriteN;
        }

        void Start()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
        }


    }
}
