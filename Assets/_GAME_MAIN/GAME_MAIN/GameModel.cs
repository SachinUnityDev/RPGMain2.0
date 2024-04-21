using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Common
{
    [System.Serializable]
    public class GameModel
    {
        public GameState gameState;       
        public GameDifficulty gameDifficulty;  
        public LocationName locationName;
        public LandscapeNames landscapeNames = LandscapeNames.None;// quest has a landscape

        [Header(" Quick Start Opts")]
        public ClassType abbasClassType;
        public JobNames jobSelect; 

        public GameModel( GameState currGameState
                , GameDifficulty gameDifficulty, LocationName locationName)
        {
            this.gameState = currGameState;
            this.gameDifficulty = gameDifficulty;
            this.locationName = locationName;   
            jobSelect = JobNames.None;
            abbasClassType= ClassType.None;

        }
    }


}


