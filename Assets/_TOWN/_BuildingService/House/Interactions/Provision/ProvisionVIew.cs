using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using UnityEngine.UI; 

namespace Town
{
    public class ProvisionView : MonoBehaviour
    {
        public PotionNames selectPotion;
        [SerializeField] Button tickBtn;
        [SerializeField] Transform optContainer; 


        private void Awake()
        {
            tickBtn.onClick.AddListener(OnAdd2ProvisionSlot);                 
        }
        public void OnSelect(PotionNames _potionName)
        {
            foreach (Transform child in optContainer)
            {
                ProvisionOptionsPtrEvents opts = child.GetComponent<ProvisionOptionsPtrEvents>(); 
                if(opts.potionName != _potionName)
                {
                    opts.OnDeSelect(true);
                }                
            }  
            selectPotion = _potionName;
        }

        public void OnAdd2ProvisionSlot()
        {
            //add to abbas provision slot here
        }

    }
}