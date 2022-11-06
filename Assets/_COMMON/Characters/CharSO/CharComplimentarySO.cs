using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace Common
{
    [System.Serializable]
    public class CharStoryPara
    {
        public CharNames charName;
        [TextArea(10,10)]
        public List<string> allParastr = new List<string>(); 
        public int unLockedIndex =0; 
    }

    [System.Serializable]
    public class RaceSpriteData
    {
        public RaceType raceType;
        public Sprite raceSpriteN;
        public Sprite raceSpriteLit; 
    }
    [System.Serializable]
    public class CultureSpriteData
    {
        public CultureType cultureType;
        public Sprite cultureBanner; 
    }
    [System.Serializable]
    public class HexBGdata
    {
        public CharType charType;
        public CharMode orgCharMode;
        public Sprite HexBG; 
    }
 

    [CreateAssetMenu(fileName = "CharComplimentarySO", menuName = "Character Service/CharComplimentarySO")]

    public class CharComplimentarySO : ScriptableObject
    {
        public List<RaceSpriteData> allRaceSprites = new List<RaceSpriteData>();
        public List<CultureSpriteData> allCultureSprites = new List<CultureSpriteData>();
        public List<CharStoryPara> allCharStoryPara = new List<CharStoryPara>();
        public List<HexBGdata> allHexBgData = new List<HexBGdata>();
        [TextArea(1,4)]
        public string PreReqToolTip;
        [TextArea(1, 4)]
        public string ProvisionToolTip;
        [TextArea(1, 4)]
        public string earningsToolTip;


        [Header("SPRITE REFERENCES")]
        public Sprite BGUnavail;
        public Sprite BGAvailUnClicked;
        public Sprite BGAvailClicked; 
        public Sprite frameUnavail;
        public Sprite frameAvail;
        public Sprite lvlbarUnAvail;
        public Sprite lvlBarAvail;

        public Sprite GhostlyBG; 
        
        private void Awake()
        {

            if (allRaceSprites.Count < 1)   // patch fix to prevent recreation of fields 
            {
                allRaceSprites.Clear();
                for (int i = 1; i < Enum.GetNames(typeof(RaceType)).Length; i++)
                {
                    RaceSpriteData race = new RaceSpriteData();
                    race.raceType = (RaceType)i;                   

                    allRaceSprites.Add(race);
                }
            }
            if (allCultureSprites.Count < 1)   // patch fix to prevent recreation of fields 
            {
                allCultureSprites.Clear();
                for (int i = 1; i < Enum.GetNames(typeof(CultureType)).Length; i++)
                {
                    CultureSpriteData culture = new CultureSpriteData();
                    culture.cultureType = (CultureType)i;                   

                    allCultureSprites.Add(culture);
                }


            }
        }


        public Sprite GetCultBanner(CultureType cultType)
        {
            Sprite sprite = allCultureSprites.Find(t => t.cultureType == cultType).cultureBanner; 

            return sprite; 
        }

        public RaceSpriteData GetRaceSpriteData(RaceType raceType)
        {
            RaceSpriteData r = allRaceSprites.Find(t => t.raceType == raceType);
            return r; 
        }

    }


}

