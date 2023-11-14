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
        Fastest, 
    }

    public class GameSpeedController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        
        [SerializeField] TextMeshProUGUI centerTxt;
        
        public GameSpeed currSpeed;  // move to combat service 
        public float timeval; 

        private void Start()
        {
            
            centerTxt = gameObject.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            transform.GetChild(0).GetComponent<RectTransform>().DOScaleX(0, 0.4f);

            currSpeed = GameSpeed.Normal;
            centerTxt.text = currSpeed.ToString();
           timeval =  Time.timeScale = 1.0f; 
        }

        void IncrementSpeed()
        {
            int nextSpeed = (int)currSpeed+1;
            if (nextSpeed < 3)
            {
                currSpeed = (GameSpeed)nextSpeed;
               
            }else if (nextSpeed == 3)
            {
                currSpeed = (GameSpeed)0; 
            }
            centerTxt.text = currSpeed.ToString();
            switch (currSpeed)
            {
                case GameSpeed.Normal:
                    timeval= Time.timeScale = 1.0f; 
                    break;
                case GameSpeed.Fast:
                    timeval= Time.timeScale = 1.25f; 
                    break;
                case GameSpeed.Fastest:
                    timeval=  Time.timeScale = 1.5f; 
                    break;
           
                default:
                    break;
            }



        }

        public void OnPointerClick(PointerEventData eventData)
        {
            IncrementSpeed();
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

