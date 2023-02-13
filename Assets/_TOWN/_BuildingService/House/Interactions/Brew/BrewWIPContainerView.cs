using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class BrewWIPContainerView : MonoBehaviour
    {

        public List<Transform> allBrewWIP = new List<Transform>();  
        BrewSlotView brewSlotView;
        private void Awake()
        {            
            foreach (Transform child in transform)
            {
                child.GetComponent<BrewWIPPtrEvents>().InitBrewWIP();            
            }
        }
        public void InitBrewWIP(BrewSlotView brewSlotView)
        {
            this.brewSlotView = brewSlotView;


        }
        public bool AllotBrewSlot()
        {
            foreach (Transform child in transform)
            {
                if (!child.GetComponent<BrewWIPPtrEvents>().IsSlotFilled())
                {
                    child.GetComponent<BrewWIPPtrEvents>().StartBrewWIP(brewSlotView);
                    return true; 
                }
            }

            return false;
        }

    }
}