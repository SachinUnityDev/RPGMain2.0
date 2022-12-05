using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Interactables
{
 

    public class PotionController : MonoBehaviour
    {
        // check on Potions addictions 
        // max world instance 
        // set world instance
        CharController charController;
        public int netPotionAddictChance =0; 
        

        void Start()
        {
          
        }


#region POTION ADDICT
        public float GetPotionAddictChance(PotionName potionName)
        {
            PotionSO potionSO = PotionService.Instance.GetPotionModelSO(potionName);
            float chance = potionSO.potionAddict;
            return chance; 
        }

 

        public bool PotionAddictCheck()
        {
            

            return false; 
        }

        public void ApplyPotionAddict()
        {


        }
#endregion




    }
}



