using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using UnityEngine.EventSystems;


namespace Intro
{
    public class DelBtnPtrEvents : MonoBehaviour,IPointerClickHandler, INotify
    { 
        [SerializeField] Image img;
        [Header("TBR")]
        [SerializeField] NotifyBoxView notifyBoxView;

        [SerializeField] bool isActive;

        public NotifyName notifyName { get ; set; }
        public bool isDontShowItAgainTicked { get ; set; }

        SetProfileView setProfileView;
        SetProfilePtrEvents setProfilePtrEvents;
        GameModel gameModel; 

        void Start()
        {
            img = GetComponent<Image>();                       
        }
        public void Init(GameModel gameModel, SetProfilePtrEvents setProfilePtrEvents, SetProfileView setProfileView)
        {
            this.setProfileView = setProfileView;
            this.setProfilePtrEvents = setProfilePtrEvents;
            if(gameModel == null)
            SetState(false);
            else SetState(true);    
        }

        public void SetState(bool isActive)
        {
            gameObject.SetActive(isActive); 
            this.isActive = isActive; 
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if(isActive)
            {
                ClearProfileNotifyBoxChk(); 
            }
        }
        public void ClearProfileNotifyBoxChk()
        {
            notifyName = NotifyName.ClearProfile;            
            notifyBoxView.OnShowNotifyBox(this, notifyName);
            notifyBoxView.gameObject.SetActive(true);
        }

        public void OnNotifyAnsPressed()
        {
            UIControlServiceGeneral.Instance.ToggleOffNotify();      
            setProfilePtrEvents.OnProfileClear();
        }
    }
}