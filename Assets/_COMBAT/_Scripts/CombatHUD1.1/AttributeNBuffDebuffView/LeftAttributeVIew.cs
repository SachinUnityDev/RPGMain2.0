using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{


    public class LeftAttributeVIew : MonoBehaviour
    {
        private void Start()
        {
            CombatEventService.Instance.OnCharOnTurnSet
                                          += AttributeViewInit;

            CombatEventService.Instance.OnCharClicked += AttributeViewInit;
        }
        private void OnDisable()
        {
            CombatEventService.Instance.OnCharOnTurnSet
                                      -= AttributeViewInit;

            CombatEventService.Instance.OnCharClicked -= AttributeViewInit;
        }
        public void AttributeViewInit(CharController charController)
        {
            foreach (Transform child in transform)
            {   
                if(child?.GetComponent<PopUpAndHL>() != null)
                    child.GetComponent<PopUpAndHL>().InitAttrib(charController); 
            }
        }


    }
}