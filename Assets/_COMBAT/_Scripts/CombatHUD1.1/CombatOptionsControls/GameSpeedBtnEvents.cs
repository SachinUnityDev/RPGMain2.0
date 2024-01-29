using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using DG.Tweening; 

namespace Combat
{
    public enum GameSpeed
    {
        Normal, 
        Fast,
      //  Fastest, 
    }

    public class GameSpeedBtnEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        
        [SerializeField] TextMeshProUGUI centerTxt;
        
        public GameSpeed currSpeed;  // move to combat service 
        public float timeval;
        [Header(" Images")]
        Image img;
        [SerializeField] Sprite spriteN;
        [SerializeField] Sprite spriteHL;
        private void Start()
        {
            img = GetComponent<Image>();
            img.sprite = spriteN;
            centerTxt = gameObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);

            currSpeed = GameSpeed.Normal;
            centerTxt.text = currSpeed.ToString();
           timeval =  Time.timeScale = 1.0f; 
        }

        void ToggleSpeed()
        {
            if(currSpeed == GameSpeed.Normal) 
                currSpeed = GameSpeed.Fast;
            else
                currSpeed = GameSpeed.Normal;

            centerTxt.text = currSpeed.ToString();
            switch (currSpeed)
            {
                case GameSpeed.Normal:
                    timeval= Time.timeScale = 1.0f; 
                    break;
                case GameSpeed.Fast:
                    timeval= Time.timeScale = 1.6f; 
                    break;
                default:
                    break;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            img.sprite = spriteHL;
            ToggleSpeed();     
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(1, 0.4f);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);
            img.sprite = spriteN; 
        }
    }
}

