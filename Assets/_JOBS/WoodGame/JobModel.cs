using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common;

namespace Town
{


    public class JobModel
    {
        public bool isPlayedOnce; 
        public JobRank currJobRank;
        public int currentGameSeq;
        public int currGameJobExp;
        public int dayInYrPlayed;
        public MonthName monthPlayed; 
        public JobModel(JobRank currJobRank, int currentGameSeq, int currGameJobExp
                        , int dayInYrPlayed, MonthName monthPlayed)
        {
            isPlayedOnce = true;
            this.currJobRank = currJobRank;
            this.currentGameSeq = currentGameSeq;
            this.currGameJobExp = currGameJobExp;
            this.dayInYrPlayed= dayInYrPlayed;
            this.monthPlayed = monthPlayed;   
        }

        public JobModel()
        {

        }
        public JobModel(WoodGameSO woodGameSO)
        {
            isPlayedOnce = false; 
            currJobRank = woodGameSO.allWoodData[0].woodGameRank; 
            currentGameSeq = woodGameSO.allWoodData[0].gameSeq;
            currGameJobExp = woodGameSO.allWoodData[0].minJobExpAdded;
            dayInYrPlayed = CalendarService.Instance.dayInYear;
            monthPlayed = CalendarService.Instance.currentMonth; 
        }
        public void SaveModel(JobModel woodGameModel)
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