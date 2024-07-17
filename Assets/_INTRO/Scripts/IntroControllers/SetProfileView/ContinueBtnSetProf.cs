using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 


namespace Intro
{
    public class ContinueBtnSetProf : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image img;

        [SerializeField] Color colorN;
        [SerializeField] Color colorClicked;
        [SerializeField] Color colorOnHover;
        [SerializeField] Color colorUnclickable; 

        SetProfileView setProfileView;
        [SerializeField] float prevTime = 0f;
        [SerializeField] bool isClickable; 

        private void Start()
        {
            img.color= colorN;
        }

        public void Init(SetProfileView setProfileView)
        {
            this.setProfileView = setProfileView;
            SetState(false);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClickable) 
            if(Time.time - prevTime> 0.25f)
            {
                img.color = colorClicked;
                setProfileView.OnContinuePressed(); 
            }            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isClickable)
            img.color = colorOnHover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(isClickable)
            img.color = colorN;
        }

        public void SetState(bool isClickable)
        {
            img.color = isClickable ? colorN : colorUnclickable;
            this.isClickable = isClickable; 

        }

    }
}