using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Common; 


namespace Combat
{
    public class HPBarController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI toggleTxt;

        public bool isON;
        private void Start()
        {
            toggleTxt = gameObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);
            toggleTxt.text = "ON";
            isON = true;
            ToggleHealthBars(isON); 
        }

        void ToggleHealthBars(bool turnON)
        {
            //foreach (GameObject charGO in CharService.Instance.charsInPlay)
            //{
            //    Transform HPbarsTrans = charGO.transform.GetChild(2);
            //    SpriteRenderer[] HPBarRen = HPbarsTrans.GetComponentsInChildren<SpriteRenderer>();
            //    for (int i = 0; i < HPBarRen.Length; i++)
            //    {
            //        HPBarRen[i].enabled = turnON;
            //    }
            //}
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isON)
            {
                toggleTxt.text = "ON";
                ToggleHealthBars(true); 
                isON = false;
            }
            else
            {
                toggleTxt.text = "OFF";
                ToggleHealthBars(false); 
                isON = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(1, 0.4f);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);
        }
    }


}


