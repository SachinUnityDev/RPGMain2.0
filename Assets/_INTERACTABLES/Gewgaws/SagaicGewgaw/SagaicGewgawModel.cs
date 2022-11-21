using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class SagaicGewgawModel
    {
        public int itemID;
        public CharNames charName;
        
        [Header("COST")]
        public Currency cost;
        public Currency weeklyCost;        
        public float fluctuation = 20f;  //

        public SagaicGewgawModel()
        {
            

        }

    }


}



