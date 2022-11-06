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
        void Start()
        {
           //TownEventService.Instance.OnQuestBegin += temptraitsFactory.InitTempTraits;       // working 
        }

        // Update is called once per frame
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {

                Debug.Log("temptrait service in ues"); 
            }
        }

    }


}

