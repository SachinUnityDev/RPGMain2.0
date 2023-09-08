using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
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
        public SeqBarkController seqBarkController;
        void Start()
        {
      
        }

        public void InitBarkService()
        {
            qbarkController = GetComponent<QbarkController>();
            curioBarkController = GetComponent<CurioBarkController>();
            buildBarkController = GetComponent<BuildBarkController>();
            seqBarkController = GetComponent<SeqBarkController>();
            seqBarkController.InitBarkController();
        }

 
     
    }
}




