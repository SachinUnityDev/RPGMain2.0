using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] Color Unclickable; 

        SetProfileView setProfileView;
        [SerializeField] float prevTime = 0f;  
        

        private void Start()
        {
            img.color= colorN;  
        }

        public void Init(SetProfileView setProfileView)
        {
            this.setProfileView = setProfileView;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if(Time.time - prevTime> 0.25f)
            {
                img.color = colorClicked;
                setProfileView.OnContinuePressed(); 
            }            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.color = colorOnHover;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.color = colorN;
        }

        public void SetState(bool isClickable)
        {

        }

    }
}