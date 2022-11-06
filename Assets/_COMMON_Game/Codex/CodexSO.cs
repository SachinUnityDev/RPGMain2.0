using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Common
{

    [Serializable]
    public class CodexSpriteData
    {
        public int pageNum;
        public Sprite pageSprite; 
    }


    [Serializable]
    public class SubHeaderData
    {
        public CodexSubHeader codexSubHeader;
        public List<CodexSpriteData> allSubHeaderSprites = new List<CodexSpriteData>();
       // public int startPg;
    }

    [Serializable]
    public class HeaderData
    {
        public CodexHeader codexHeader;
        public List<CodexSpriteData> allHeaderSprites = new List<CodexSpriteData>();
        public List<SubHeaderData> allSubHeaders = new List<SubHeaderData>();
       // public int startPg;
        public HeaderData(CodexHeader codexHeader)
        {
            this.codexHeader = codexHeader;
        }
    }


    [CreateAssetMenu(fileName = "CodexSO", menuName = "Common/CodexSO")]
    public class CodexSO : ScriptableObject
    {

        public Sprite headingArrowN;
        [Header("Header Btn Sprites")]
        public Sprite spriteHeaderN;
        public Sprite spriteHeaderHover;
        public Sprite spriteHeaderHL;

        [Header("Sub Header btn Sprites")]
        public Sprite spriteSubN;
        public Sprite spriteSubHL;

        public GameObject subHeaderBtnPrefab;

        public List<HeaderData> allHeaderData = new List<HeaderData>();
        void Awake()
        {
            if (allHeaderData.Count > 0) return;
            for (int i = 1; i < Enum.GetNames(typeof(CodexHeader)).Length; i++)
            {
                HeaderData headerData = new HeaderData((CodexHeader)i);
                allHeaderData.Add(headerData);
            }
        }

        
     

    }

    public enum CodexSubHeader
    {
        None,
        BasicRules,
        StatusEffects,
        DOTs,
        Tactics,
    }


    public enum CodexHeader
    {
        None,
        Abbreviations,
        Combat,
        Town,
        Map,
        Quest,
        Fame,
        Lore,
        Roster,
        Characters,
        Bestiary,
        Inventory,
        Camp,
    }


}

