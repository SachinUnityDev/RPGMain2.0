using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

namespace Town
{


    public class WoodGameModel
    {
        public bool isPlayedOnce; 
        public WoodGameRank currWoodGameRank;
        public int currentGameSeq;
        public int currGameJobExp;

        public WoodGameModel(WoodGameRank currWoodGameRank, int currentGameSeq, int currGameJobExp)
        {
            isPlayedOnce = true;
            this.currWoodGameRank = currWoodGameRank;
            this.currentGameSeq = currentGameSeq;
            this.currGameJobExp = currGameJobExp;
        }

        public WoodGameModel()
        {

        }
        public WoodGameModel(WoodGameSO woodGameSO)
        {
            isPlayedOnce = false; 
            currWoodGameRank = woodGameSO.allWoodData[0].woodGameRank; 
            currentGameSeq = woodGameSO.allWoodData[0].gameSeq;
            currGameJobExp = woodGameSO.allWoodData[0].minJobExpAdded;
        }
        public void SaveModel(WoodGameModel woodGameModel)
        {
           
            string mydataPath = "/_SaveService/savedFiles/WoodGameModel.txt";


            Debug.Log(" INSIDE wood game save MODEL ");
            if (!File.Exists(Application.dataPath + mydataPath))
            {
                Debug.Log("does not exist");
                File.CreateText(Application.dataPath + mydataPath);
            }
            else
            {
                ClearState();
            }
            string woodGameData = JsonUtility.ToJson(woodGameModel);
            File.WriteAllText(Application.dataPath + mydataPath, woodGameData);

        }
        public void ClearState()
        {
            string mydataPath = "/_SaveService/savedFiles/WoodGameModel.txt";
            File.WriteAllText(Application.dataPath + mydataPath, "");

        }
    }
}