using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{
    [System.Serializable]
    public class HexNameNSprite
    {
        public HexNames hexName; 
        public Sprite hexSprite; 
    }

    [Serializable]
    public class SkillIncliSprites
    {
        public SkillInclination SkillIncliType;
        public Sprite SkillIncliSprite;
        public Sprite SkillIncliBG; 

    }
    [Serializable]
    public class AttackSprites
    {
        public AttackType attackType;
        public Sprite attackTypeSprite;
        public Sprite attackTypeBG; 

    }


    [CreateAssetMenu(fileName = "SkillHexSO", menuName = "Skill Service/SkillHexSO")]
    public class SkillHexSO : ScriptableObject
    {
        [Header("Frame SPRITES")]
        public Sprite skillIconFrameN;
        public Sprite skillIconFrameHL;

        [Header("Prefabs")]
        public GameObject txtPrefab;  

        [Header("SkillInclination SPRITES ")]
        public List<SkillIncliSprites> allSkillIncli = new List<SkillIncliSprites>();

        [Header("Attack Type SPRITES ")]
        public List<AttackSprites> allAttacksSprites = new List<AttackSprites>();


        [Header("HEX SPRITES ")]        
        public List<HexNameNSprite> allHexes = new List<HexNameNSprite>();


        [Header("Skill Btn Related Sprites")]
        public Sprite SkillSelectFrame;
        public Sprite SkillNormalFrame;
        public Sprite skillSelectFrame2;

        [Header("Skill Card Prefab")]
        public GameObject skillCardPrefab; 


        private void Awake()
        {
            if (allHexes.Count < 1)
            {
                for (int i = 0; i < Enum.GetNames(typeof(HexNames)).Length; i++)
                {
                    HexNameNSprite hexNSprite = new HexNameNSprite();
                    hexNSprite.hexName = (HexNames)i;
                    allHexes.Add(hexNSprite);
                }

            } 

            if (allSkillIncli.Count < 1)
            {
                for (int i = 0; i < Enum.GetNames(typeof(SkillInclination)).Length; i++)
                {
                    SkillIncliSprites skillIncliSprites = new SkillIncliSprites();
                    skillIncliSprites.SkillIncliType = (SkillInclination)i;
                    allSkillIncli.Add(skillIncliSprites); 
                }
            }

            if (allAttacksSprites.Count < 1)
            {
                for (int i = 0; i < Enum.GetNames(typeof(AttackType)).Length; i++)
                {
                    AttackSprites attackSprites = new AttackSprites();
                    attackSprites.attackType = (AttackType)i;
                    allAttacksSprites.Add(attackSprites);
                }
            }


        }

        public Sprite GetSkillIncliSprite(SkillInclination skillIncli)
        {
            int index = allSkillIncli.FindIndex(t => t.SkillIncliType == skillIncli); 
            if(index != -1)
            {
                return allSkillIncli[index].SkillIncliSprite; 
            }
            else
            {
                Debug.Log("SkillIncli not found" + allSkillIncli);
                return null; 
            }
        }

        public Sprite GetSkillAttackType(AttackType attackType)
        {
            int index = allAttacksSprites.FindIndex(t => t.attackType == attackType); 
            if(index != -1)
            {
                return allAttacksSprites[index].attackTypeSprite; 
            }
            else
            {
                Debug.Log("attack sprite not found" + attackType);
                return null;
            }
        }

        public Sprite GetHexSprite(HexNames hexName)
        {
            int index = allHexes.FindIndex(t=>t.hexName == hexName);    
            if(index != -1)
            {
                return allHexes[index].hexSprite; 
            }
            else
            {
                Debug.Log("hex sprite not found" + hexName);
                return null;
            }
        }
    }

    public enum HexNames
    {
        none,
        allies_infront_7,                  
        ally_anyoneNcast_anyone,      
        ally_everyoneNcast_everyone,
        ally1Ncast1,                   
        ally123Ncast123,               
        ally1234Ncast1234,             
        ally123456Ncast123456,        
        ally14Ncast14,
        ally567Ncast567,             
        ally4567Ncast4567,             
        base_hex,
        enemy_anyone,
        enemy_backdiamond,
        enemy_backrow,
        enemy_everyone,
        enemy_firstonlane,
        enemy_firstrow,
        enemy_frontdiamond,
        enemy_frontorbackdiamond,
        enemy_frontrow,
        enemy_lastonlane,
        enemy_lastrow,
        enemy_midlane,
        enemy1234,
        self_any,
        self1,
        self123,
        self4567,
        ally456Ncast456,
        enemy_firstonmidlane_plusCOL_behind,
        enemy_firstonmidlane,
        self1234,
        enemy_firstonmidlane_plusCOL_adj,
        self14,
        enemy_firstonlane_same,
        enemy_fulllane_same,
        ally234567NCast234567,
        anyTile,
    }


}
