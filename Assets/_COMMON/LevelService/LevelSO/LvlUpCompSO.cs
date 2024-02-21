using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [Serializable]
    public class LvlData
    {
        public AttribName attribName;
        public int val; 
    }
    [Serializable]
    public class LvlDataComp
    {
        public Levels level;      
        public List<LvlData> allStatDataAuto = new List<LvlData>();
        public List<LvlData> allStatDataOption1 = new List<LvlData>();
        public List<LvlData> allStatDataOption2 = new List<LvlData>();

        public LvlDataComp()
        {

        }
        public LvlDataComp(Levels level)
        {
            this.level = level;
        }
    }

    [System.Serializable]
    public class CharLvlUpdata
    {
        public Archetype heroType;
        public List<LvlDataComp> allLvldataComp = new List<LvlDataComp>(); 
    }

    [CreateAssetMenu(fileName = "LevelUpCompSO", menuName = "Common/LvlUpCompSO")]
    public class LvlUpCompSO : ScriptableObject
    {

        public List<CharLvlUpdata> allCharLvlUpData = new List<CharLvlUpdata>();

        public Sprite lvlPlusSprite;
        public Sprite lvlMinusSprite;
        public Sprite spriteN; 

        public LvlDataComp GetLvlData(Archetype heroType, Levels lvl)
        {
            foreach (CharLvlUpdata lvlUpData in allCharLvlUpData)
            {
                if(lvlUpData.heroType == heroType)
                {
                    foreach(LvlDataComp lvlComp in lvlUpData.allLvldataComp)
                    {
                        if(lvlComp.level == lvl)
                        {
                            return lvlComp; 
                        }
                    }
                }

            }
            Debug.Log("LEVEL DATA NOT Recieved ");
            return null;
        }

    }


}

