using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

namespace Common
{
    public class ToggleRosterLockDisbandBtn : MonoBehaviour, IPointerClickHandler, INotify
    {
        public bool isDontShowItAgainTicked { get ; set ; }
        public NotifyName notifyName { get; set; }
        Image img;
        private void Start()
        {
            img = GetComponent<Image>();    
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

       
        public void OnPointerClick(PointerEventData eventData)
        {
            ShowNotifyBox();

        }

    
    }
}


