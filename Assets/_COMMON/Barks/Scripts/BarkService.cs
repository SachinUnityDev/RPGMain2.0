using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class BarkService : MonoSingletonGeneric<BarkService>
    {
        // Start is called before the first frame update
        public AllBarkSO allBarkSO;
        public QbarkController qbarkController;
        public CurioBarkController curioBarkController; 
        public BuildBarkController buildBarkController; 
        void Start()
        {
            qbarkController = gameObject.GetComponent<QbarkController>();
            curioBarkController = GetComponent<CurioBarkController>();
            buildBarkController= GetComponent<BuildBarkController>();
        }

        public void PlayBark( int BarkID, BarkType barkTrigger, 
            CharNames charName)
        {
            // Bark trigger .. who will carry the bark 
            // get bark SO play here 
        }

        public void PlayBark(int barkID)
        {



        }     
     
    }
}




