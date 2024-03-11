using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Town
{
    public class DryerSlotView : MonoBehaviour
    {
        [Header("slot TBR")]
        [SerializeField] DryerSlotController slot1;
        [SerializeField] DryerSlotController slot2;
        [SerializeField] DryerSlotController slot3;

        DryerBtnPtrEvents dryerBtnPtrEvents;
        [Header("Global var")]
        DryerView dryerView;
        public Iitems itemSelect;
        
        public void InitDryerSlotView(DryerView dryerView, DryerBtnPtrEvents dryerBtnPtrEvents, int index)
        {
            this.dryerView = dryerView;
            this.dryerBtnPtrEvents= dryerBtnPtrEvents;
            itemSelect = null; 
            // if index ==1 get venison in first slot, mutton and beef in second and third 
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.gameObject.SetActive(false);
                
                if ((index == 2 || index == 3) && ( i== 0))
                {
                    transform.GetChild(i).GetComponent<DryerSlotController>().InitSlot(dryerView, this, index);
                }
                else
                {
                    transform.GetChild(i).GetComponent<DryerSlotController>().InitSlot(dryerView, this, index);
                }
            }
            dryerBtnPtrEvents.InitDryerBtnPtrEvents(dryerView, this, itemSelect);
        }

        public void OnSlotSelect(Iitems item, int subIndex)
        {
            // mark that slot as HL others remove HL 
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<DryerSlotController>().OnFrameSelect(subIndex); 
            }
            itemSelect = item;
            dryerBtnPtrEvents.InitDryerBtnPtrEvents(dryerView, this, itemSelect);
        }

       


    }
}