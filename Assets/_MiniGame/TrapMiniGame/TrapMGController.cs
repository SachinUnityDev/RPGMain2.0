using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;


namespace Common
{
    public class TrapMGController : MonoBehaviour
    {
        public TrapMGModel trapMGModel;
        public TrapMGSO trapMGSO;
        public TrapView trapView; 

        public MGGameState trapGameState;

        public int currLetterHL; 
    
        public void InitGame()
        {
            trapMGModel = new TrapMGModel(trapMGSO);
            
            trapGameState = MGGameState.Start;
            trapView.StartSeq();

        }


    }
}