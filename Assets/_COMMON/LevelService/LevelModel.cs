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
        public Levels lvl;
        public List<LvlData> allOptions = new List<LvlData>();

        public ManualOptData(Levels lvl)
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
            for (int i = 1; i <= 12; i++)
            {
                ManualOptData manualOptData = new ManualOptData((Levels)i);
                allOptionsChosen.Add(manualOptData); 
            }
            for (int i = 1; i <= 12; i++)
            {
                LvlDataComp lvlDataComp = new LvlDataComp((Levels)i);
                allOptionsPending.Add(lvlDataComp);
            }
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
        public void AddOptions2ChosenStack(CharNames charName, List<LvlData> allStatData, Levels lvl)
        {
            int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            int j = allCharLvlUpData[i].allOptionsChosen.FindIndex(t => t.lvl == lvl);
            if (allCharLvlUpData[i].allOptionsChosen[j].allOptions.Count > 0)
            {
                allCharLvlUpData[i].allOptionsChosen[j].allOptions.Clear(); 

            }
            allCharLvlUpData[i].allOptionsChosen[j].allOptions.AddRange(allStatData); 

        }

        //public void RemoveOptions2ChosenStack(CharNames charName, Levels lvl)
        //{
        //    int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
        //    int j = allCharLvlUpData[i].allOptionsChosen.FindIndex(t => t.lvl == lvl);
        //    if (allCharLvlUpData[i].allOptionsChosen[j].allOptions.Count > 0)
        //    {
        //        allCharLvlUpData[i].allOptionsChosen[j].allOptions.Clear();
        //    }
        //    //foreach (var stat in allStatData)
        //    //{
        //    //    allCharLvlUpData[i].allOptionsChosen[j].allOptions.Remove(stat);
        //    //}
        //}
        public void AddOptions2PendingStack(CharNames charName, List<LvlData> allStatData1
            ,List<LvlData> allStatData2, Levels lvl)
        {
            int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            if (i == -1)
            {
                LvlStackData lvlStackData = new LvlStackData(charName);
                allCharLvlUpData.Add(lvlStackData);
                 i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            }

            int j = allCharLvlUpData[i].allOptionsPending.FindIndex(t => t.level == lvl);
            
            if (allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Count > 0)
            {
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Clear();
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2.Clear();
            }
            allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.AddRange(allStatData1);
            allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2.AddRange(allStatData2);

        }

        public void RemoveOptions2PendingStack(CharNames charName, Levels lvl, int opt)
        {
            int i = allCharLvlUpData.FindIndex(t => t.charName == charName);
            int j = allCharLvlUpData[i].allOptionsPending.FindIndex(t => t.level == lvl);
            if (allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Count > 0)
            {
                if(opt == 1)
                {
                    LevelService.Instance.ManLvlUp(charName, allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1); 
                }
                if (opt == 2)
                {
                    LevelService.Instance.ManLvlUp(charName, allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2);
                }
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption1.Clear();
                allCharLvlUpData[i].allOptionsPending[j].allStatDataOption2.Clear();
            }
        }
    }




}
