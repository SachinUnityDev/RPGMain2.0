using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Combat;

public enum WoodGameRank
{
    Apprentice,     
    Expert,
    Master,

}

public enum WoodGameState
{
    NewStartOptions,
    Running,      
    HitPaused, 
    LoadPaused, 
    ExitGame, 
}


[System.Serializable]
public class WoodGameData
{
    public WoodGameRank woodGameRank;
    public int gameSeq;
    public float timeAvailable4Game;
    public int totalMistakesAllowed;
    public int totalCorrectHits; 
    public float barScale;
    public float sliderSpeed; 
    public bool isBarRelocationON;
    public bool isCorrectHitsConseq; 
    public int minJobExpAdded;
    public int maxJObExpAdded;
    public int minJobExpR;
    public int maxJobExpR; 
}

[CreateAssetMenu(fileName = "WoodGameRankSO", menuName = "ScriptableObjects/WoodGameSO")]

public class WoodGameSO : ScriptableObject
{

    public List<WoodGameData> allWoodData = new List<WoodGameData>(); 

    public WoodGameData GetWoodGameData(int gameSeq, WoodGameRank gameRank)
    {
        int index = allWoodData.FindIndex(t=>t.gameSeq == gameSeq && t.woodGameRank == gameRank);
        if(index != -1)
        {
            return allWoodData[index]; 
        }
        Debug.Log(" data not found" + gameSeq + "Rank" + gameRank); 
        return null;
    }

}


//public class WoodGameModel
//{
//    public WoodGameRank currWoodGameRank;
//    public int currentGameSeq;
//    public int currGameJobExp;

//    public WoodGameModel(WoodGameRank currWoodGameRank, int currentGameSeq, int currGameJobExp)
//    {
//        this.currWoodGameRank = currWoodGameRank;
//        this.currentGameSeq = currentGameSeq;
//        this.currGameJobExp = currGameJobExp;
//    }

//    public WoodGameModel()
//    {

//    }
    //public void SaveModel(WoodGameModel woodGameModel)
    //{
    //    //string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.currSlotSelected.ToString()   + "/Grid/DynaModels.txt";

    //    string mydataPath = "/SAVE_SYSTEM/savedFiles/WoodGameModel.txt"; 

     
    //    Debug.Log(" INSIDE wood game save MODEL ");
    //    if (!File.Exists(Application.dataPath + mydataPath))
    //    {
    //        Debug.Log("does not exist");
    //        File.CreateText(Application.dataPath + mydataPath);
    //    }
    //    else
    //    {
    //        ClearState(); 
    //    }

    //    string woodGameData= JsonUtility.ToJson(woodGameModel);
    
    //    File.WriteAllText(Application.dataPath + mydataPath, woodGameData);

    //}
    //public void ClearState()
    //{
    //    //string mydataPath = "/SAVE_SYSTEM/savedFiles/" + SaveService.Instance.currSlotSelected.ToString()
    //    // + "/Grid/DynaModels.txt";

    //    string mydataPath = "/SAVE_SYSTEM/savedFiles/WoodGameModel.txt";
    //    File.WriteAllText(Application.dataPath + mydataPath, "");

    //}

//}



//    20 secs
//3 total mistake chance
//1x speed
//Bar size Big
//3 correct hits to win 1 game
//Bar relocates each correct hit
//+1 job expbExp