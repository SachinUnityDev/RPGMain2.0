using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.Tilemaps;
using UnityEngine.UI; 

namespace Combat
{
    public class HexController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] GameObject hexGrid; 
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
            ToggleHex(!isON);
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

        void ToggleHex(bool _isON)
        {
            if (_isON)
            {
                hexGrid.GetComponentInChildren<Tilemap>().color = new Color(1, 1, 1, 0.75f); 
            }else
            {
                hexGrid.GetComponentInChildren<Tilemap>().color = new Color(1, 1,1 , 0.0f);
            }
        }

    }



}

