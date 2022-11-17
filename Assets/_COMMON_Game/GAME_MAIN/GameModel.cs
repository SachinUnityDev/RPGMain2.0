using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Common
{
    public class GameModel
    {
        public GameState gameState;
        public GameDifficulty gameDifficulty;
        public GameMode gameMode;
        public LocationName locationName;
        public LandscapeNames landscapeNames = LandscapeNames.None;// quest has a landscape

        public GameModel( GameState currGameState
                , GameDifficulty gameDifficulty, GameMode gameMode, LocationName locationName)
        {
          
            this.gameState = currGameState;
            this.gameDifficulty = gameDifficulty;
            this.gameMode = gameMode;
            this.locationName = locationName;   
        }
    }


}


