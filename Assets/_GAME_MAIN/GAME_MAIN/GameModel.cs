using Interactables;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Common
{
    [System.Serializable]
    public class GameModel
    {
        public ProfileSlot profileSlot; 
        [SerializeField] string profileName;
        public SaveSlot saveSlot; 
        public GameScene gameScene;       
        public GameDifficulty gameDifficulty;  
        public LocationName locationName;
        public LandscapeNames landscapeNames = LandscapeNames.None;// quest has a landscape
        
        [Header(" Quick Start Opts")]
        public ClassType abbasClassType;
        public JobNames jobSelect;
        /* isCurrGameModel is to mark in the save file which gameModel to load from the list*/ 
        public bool isCurrGameModel; 
        public GameModel(GameScene currGameScene,
            GameDifficulty gameDifficulty, LocationName locationName)
        {
           this.gameScene = currGameScene;
            this.gameDifficulty = gameDifficulty;
            this.locationName = locationName;   
            jobSelect = JobNames.None;
            abbasClassType= ClassType.None;
            profileName = string.Empty;
            isCurrGameModel = false;    
        }
        public GameModel(int profileSlot, string profileName)
        {
            this.profileSlot = (ProfileSlot)profileSlot;
            this.profileName = profileName;
        }
        public string GetProfileName()
        {
            return profileName;
        }
    }

}


