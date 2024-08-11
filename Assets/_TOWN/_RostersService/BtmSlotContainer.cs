using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{


    public class BtmSlotContainer : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] PortraitDragNDrop portraitDragNDrop;
        
     
        RosterViewController rosterViewController;

        private void OnEnable()
        {
            CharService.Instance.OnPartyLocked += OnPartyLocked;    
            CharService.Instance.OnPartyDisbanded += OnPartyDisbanded;
        }
        private void OnDisable()
        {
            CharService.Instance.OnPartyLocked -= OnPartyLocked;
            CharService.Instance.OnPartyDisbanded -= OnPartyDisbanded;
        }
        void OnPartyLocked()
        {
            foreach (Transform child in transform)
            {
                if (child.childCount >= 1)
                {
                    Transform portTransX = child?.GetChild(0);
                    if (portTransX != null)
                        portTransX.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }            
        }
        void OnPartyDisbanded()
        {
            foreach (Transform child in transform)
            {
                if (child.childCount >= 1)
                {
                    Transform portTransX = child?.GetChild(0);
                    if (portTransX != null)
                        portTransX.GetComponent<CanvasGroup>().blocksRaycasts = true;
                }
            }
        }
        public void Init(RosterViewController rosterViewController)
        {            
            this.rosterViewController = rosterViewController;
            // loop through all the slots and add the char to the slot
            RosterModel rosterModel = RosterService.Instance.rosterModel;
            int i = 0; 
            foreach (CharNames charName in rosterModel.charInParty)
            {
                i++;
                if (charName == CharNames.Abbas) continue;
                PartyPortraitSlotController partyPortraitSlotController = 
                        transform.GetChild(i)?.GetComponent<PartyPortraitSlotController>();
                if (partyPortraitSlotController == null) continue; 
                PortraitDragNDrop portraitDragNDrop1 =
                        transform.GetChild(i)?.GetComponentInChildren<PortraitDragNDrop>();
                GameObject portTransGO;
                if (portraitDragNDrop1 == null)                
                    portTransGO = Instantiate(portraitDragNDrop.gameObject);                
                else                
                    portTransGO = portraitDragNDrop1.gameObject;
                
                partyPortraitSlotController.AddChar2SlotOnLoad(portTransGO, charName);                
            }
        }
    }
}