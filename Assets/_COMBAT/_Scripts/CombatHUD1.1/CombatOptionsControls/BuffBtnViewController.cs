using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Combat
{
    public class BuffBtnViewController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] GameObject BuffBtnsGO; 
        [SerializeField] TextMeshProUGUI toggleTxt;
        public bool isON = true;
        [Header(" Images")]
        Image img;
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        private void Start()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
            toggleTxt = gameObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);
            toggleTxt.text = "ON";
            isON = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            img.sprite = spriteHL;
            ToggleBuffBtns(!isON);
            if (isON)
            {
                toggleTxt.text = "ON";
            }
            else
            {
                toggleTxt.text = "OFF";
            }
            isON = !isON;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(1, 0.4f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.sprite = spriteN;
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);
        }

        void ToggleBuffBtns(bool _isON)
        {
            BuffBtnsGO.SetActive(isON);
        }
    }
}