using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Common
{
    public class ToggleRosterLockDisbandBtn : MonoBehaviour, IPointerClickHandler, INotify, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Color colorN;
        [SerializeField] Color colorHL;
        [SerializeField] Color colorNA;

        [SerializeField] bool isClickable;
        public bool isDontShowItAgainTicked { get ; set ; }
        public NotifyName notifyName { get; set; }
        Image img;
        RosterViewController rosterView; 
        private void OnEnable()
        {
            img = GetComponent<Image>();    
        }
        public void Init(RosterViewController rosterView)
        {
            this.rosterView = rosterView;
            SetState(); 
        }
        void ShowNotifyBox()
        {
            if (CharService.Instance.isPartyLocked)
            {
                notifyName = NotifyName.RosterLock;
                transform.GetChild(0)
                    .GetComponent<NotifyBoxView>().OnShowNotifyBox(this, notifyName);
                img.sprite = RosterService.Instance.rosterSO.rosterLock; 
            }
            else
            {
                notifyName = NotifyName.RosterDisband;
                transform.GetChild(0)
                    .GetComponent<NotifyBoxView>().OnShowNotifyBox(this, notifyName);
                img.sprite = RosterService.Instance.rosterSO.rosterDisband;

            }
        }
        void SetState()
        {
            if (AbbasAvailChk())
            {
                isClickable = true;
                img.color = colorN;
            }
            else
            {
                isClickable = false;
                img.color = colorNA;
            }
        }
        public void OnNotifyAnsPressed()
        {
           // CharService.Instance.isPartyLocked = !CharService.Instance.isPartyLocked;
            if(!CharService.Instance.isPartyLocked)
            {
                img.sprite = RosterService.Instance.rosterSO.rosterLock;
                CharService.Instance.On_PartyLocked(); 
            }
            else
            {
                img.sprite = RosterService.Instance.rosterSO.rosterDisband;
                CharService.Instance.On_PartyDisbanded();

            }
        }

        bool AbbasAvailChk()
        {
            CharController charController = CharService.Instance.GetAbbasController(CharNames.Abbas);
            AvailOfChar availOfChar = charController.charModel.availOfChar; 
            if(availOfChar == AvailOfChar.UnAvailable_InParty)
            {
                return true;
            }
            return false;            
        }
   
        public void OnPointerClick(PointerEventData eventData)
        {
            if (isClickable)
            {
                ShowNotifyBox();
                SetState();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isClickable)
                img.color = colorHL;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isClickable)
                img.color = colorN;
        }

    }
}


