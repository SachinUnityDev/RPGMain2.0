using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class TrapMGController : MonoBehaviour
    {
        public AllTrapMGSO allTrapSO;
        public TrapMGSO trapMGSO;

        public TrapMGModel trapMGModel; 

        public TrapView trapView; 

        public MGGameState trapGameState;
    
        public void InitGame()
        {
            GameDifficulty gameDiff = GameDifficulty.Easy;
                  // GameService.Instance.gameModel.gameDifficulty;

            trapMGSO = allTrapSO.GetTrapSO(gameDiff); 
            trapMGModel = new TrapMGModel(trapMGSO);
            
            trapGameState = MGGameState.Start;
           
            // show trap game here dia service copy 



            trapView.StartSeq(trapMGModel, allTrapSO);

        }


    }
}