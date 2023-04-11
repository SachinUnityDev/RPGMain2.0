using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using System; 

namespace Common
{

    public class PermaTraitsService : MonoSingletonGeneric<PermaTraitsService>
    {      
     
        public PermaTraitsFactory permaTraitsFactory;
        public event Action<CharController, PermaTraitBase> OnPermaTraitAdded;

        public AllPermaTraitSO allPermaTraitSO; 
     

        void Start()
        {
            permaTraitsFactory = GetComponent<PermaTraitsFactory>();
        
        }

        

        public void ApplyPermaTraits()
        {            
            foreach (CharController charCtrl in CharService.Instance.allCharsInParty)
            {
               charCtrl.permaTraitController.ApplyPermaTraits();
            }
        }




    }

}
