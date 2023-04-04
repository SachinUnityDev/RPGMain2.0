using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    [System.Serializable]
    public class LvlDataComp
    {
        public Levels level;      
        public List<AttribData> allStatDataAuto = new List<AttribData>();
        public List<AttribData> allStatDataOption1 = new List<AttribData>();
        public List<AttribData> allStatDataOption2 = new List<AttribData>();

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
        public ArcheType heroType;
        public List<LvlDataComp> allLvldataComp = new List<LvlDataComp>(); 
    }

    [CreateAssetMenu(fileName = "LevelUpCompSO", menuName = "Common/LvlUpCompSO")]
    public class LvlUpCompSO : ScriptableObject
    {

        public List<CharLvlUpdata> allCharLvlUpData = new List<CharLvlUpdata>();

        public Sprite lvlPlusSprite;
        public Sprite lvlMinusSprite;

        public LvlDataComp GetLvlData(ArcheType heroType, Levels lvl)
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

