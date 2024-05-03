using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Common
{
    [System.Serializable]
    public class GameModel
    {
        public int profileSlot; 
        [SerializeField] string profileName; 
        public GameScene gameState;       
        public GameDifficulty gameDifficulty;  
        public LocationName locationName;
        public LandscapeNames landscapeNames = LandscapeNames.None;// quest has a landscape

        [Header(" Quick Start Opts")]
        public ClassType abbasClassType;
        public JobNames jobSelect; 

        public GameModel( GameScene currGameState
                , GameDifficulty gameDifficulty, LocationName locationName)
        {
            this.gameState = currGameState;
            this.gameDifficulty = gameDifficulty;
            this.locationName = locationName;   
            jobSelect = JobNames.None;
            abbasClassType= ClassType.None;
            profileName = ""; 
        }
        public GameModel(int profileSlot, string profileName)
        {
            this.profileSlot = profileSlot;
            this.profileName= profileName;
        }
        public string GetProfileName()
        {
            return profileName;
        }
    }

    public enum GameProgress
    {
        None, 
        InIntro, 
        NewGameInit, 
        LoadGameInit, 
        GameInProgress, 
        GameQuit, 
    }



}


