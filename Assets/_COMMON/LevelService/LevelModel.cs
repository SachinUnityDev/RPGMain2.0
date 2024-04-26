using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Common
{
    [Serializable]
    public class ManualOptData
    {
        public int lvl;
        public List<LvlData> allOptions = new List<LvlData>();

        public ManualOptData(int lvl)
        {
            this.lvl = lvl;
        }
    }
    


    [Serializable]
    public class LvlStackData
    {
        public CharNames charName;
        public List<ManualOptData> allOptionsChosen = new List<ManualOptData>();
        public List<LvlData> allAutoDataAdded = new List<LvlData>();
        public List<LvlDataComp> allOptionsPending = new List<LvlDataComp>();

        public LvlStackData(CharNames charName)
        {
            this.charName = charName;           
        }
    }
    [System.Serializable]
    public class LevelModel
    {
        public List<LvlStackData> allCharLvlUpData = new List<LvlStackData>();

        public LvlStackData GetLvlStackData(CharNames charName)
        {
            LvlStackData lvlStackData = allCharLvlUpData.Find(t => t.charName == charName);
            if (lvlStackData != null)
                return lvlStackData;
            else
                Debug.Log("CHARNAME LVL DATA MISSING" + charName);
            return null;
        }
        public void Add2AutoLvledStack(CharNames charName, LvlData lvlData)
        {
            int index = allCharLvlUpData.FindIndex(t=>t.charName == charName);
            if(index != -1)
            {
                allCharLvlUpData[index].allAutoDataAdded.Add(lvlData);
            }
            else
            {
                Debug.Log(" Lvl Stack not found"); 
            }
        }
        public void AddOptions2ChosenStack(CharNames charName, List<LvlData> allStatData, int lvl)
        {
            int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            int j = allCharLvlUpData[i].allOptionsChosen.FindIndex(t => t.lvl == lvl);           
            if (j ==-1)
            {
                ManualOptData manualOptData = new ManualOptData(lvl);
                allCharLvlUpData[i].allOptionsChosen.Add(manualOptData);
                j = allCharLvlUpData[i].allOptionsChosen.FindIndex(t => t.lvl == lvl);
            }
            else if (allCharLvlUpData[i].allOptionsChosen[j].allOptions.Count > 0)
            {
                allCharLvlUpData[i].allOptionsChosen[j].allOptions.Clear();
            }

            allCharLvlUpData[i].allOptionsChosen[j].allOptions.AddRange(allStatData); 

        }
        public void AddOptions2PendingStack(CharNames charName, List<LvlData> allStatData1
            ,List<LvlData> allStatData2, int lvl)
        {
            int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            if (i == -1)
            {
                LvlStackData lvlStackData = new LvlStackData(charName);
                allCharLvlUpData.Add(lvlStackData);
                 i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            }

            int j = allCharLvlUpData[i].allOptionsPending.FindIndex(t => t.level == lvl);
            if (j == -1)
            {
                LvlDataComp lvlDataComp = new LvlDataComp(lvl); 
                allCharLvlUpData[i].allOptionsPending.Add(lvlDataComp);
                j = allCharLvlUpData[i].allOptionsPending.FindIndex(t => t.level == lvl); 
            }

            if (allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Count > 0)
            {
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Clear();
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2.Clear();
            }
            allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.AddRange(allStatData1);
            allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2.AddRange(allStatData2);

        }

        public void RemoveOptions2PendingStack(CharNames charName, int lvl, int opt)
        {
            int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            int j = allCharLvlUpData[i].allOptionsPending.FindIndex(t => t.level == lvl);
            if (allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Count > 0)
            {            
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Clear();
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2.Clear();
            }
        }
    }




}
