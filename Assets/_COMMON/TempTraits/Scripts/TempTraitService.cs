using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 



namespace Common
{
  
    public class TempTraitService : MonoSingletonGeneric<TempTraitService>
    {
        //  FUNCTIONALITY 
        //  will add all temp traits to itself
        // independent mono behaviours will inform when a trait has started to this service.  

        public event Action<TempTraitData> OnTempTraitStart;
        public event Action<TempTraitData> OnTempTraitEnd;

        public TempTraitsFactory temptraitsFactory; 
        // Start is called before the first frame update
        /// <summary>
        /// get all temp trait controllers => temp traits controller to act as buff controller for temp traits
        /// get all models here
        /// 
        /// </summary>


        void Start()
        {
           //TownEventService.Instance.OnQuestBegin += temptraitsFactory.InitTempTraits;       // working 
        }


        public void ApplyPermTraits(GameObject go)
        {
            CharController charController = go?.GetComponent<CharController>();
            PermTraitBase[] tempTraits = go.GetComponents<PermTraitBase>();
            foreach (PermTraitBase p in tempTraits)
            {
                p.ApplyTrait(charController);
            }
        }

        public bool HasTempTraits(TempTraitName tempTraitName)
        {
            // check for 

            return false; 
        }
    }


}

