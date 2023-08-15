using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Combat;
using Interactables;
using Town;

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
    public bool isPlayedOnce; 
    public WoodGameRank woodGameRank;
    public int gameSeq;
    public int lastGameExp; 
    public int netGameExp;
    public bool isFlawless; 
    public float timeAvailable4Game;
    public int totalMistakesAllowed;
    public int totalCorrectHits; 
    public float barScale;
    public float sliderSpeed; 
    public bool isBarRelocationON;
    public bool isCorrectHitsConseq; 
    public int minJobExpAdded;
    public int maxJobExpAdded;
    public int minJobExpR;
    public int maxJobExpR;

    public List<ItemDataLs> itemDataLs = new List<ItemDataLs>();
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

    public List<ItemDataWithQty> GetRewardItems(int gameSeq, WoodGameRank gameRank)
    {
        WoodGameData woodGameData = GetWoodGameData(gameSeq, gameRank); 
        List<ItemDataWithQty> allItemQty= new List<ItemDataWithQty>();
        foreach (ItemDataLs itemDataLs in woodGameData.itemDataLs)
        {
            int itemName =
                itemDataLs.itemName.GetItemName(itemDataLs.itemType);
            ItemData itemData = new ItemData(itemDataLs.itemType, itemName, itemDataLs.genGawgawQ);
            ItemDataWithQty itemQty = new ItemDataWithQty(itemData, itemDataLs.qty);
            allItemQty.Add(itemQty);                    
        }
        return allItemQty; 
    }
}


