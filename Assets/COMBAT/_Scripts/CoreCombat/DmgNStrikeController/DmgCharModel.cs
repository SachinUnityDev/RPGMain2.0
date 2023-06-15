using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class DmgCharModel
    {
        public float dmgMultiplier;
        public float dmgMultPercent;

        // pass charModel and get all types
        // pass Skill model and get all attack types and Inclinations etc


        public float GetDmgPercentMultiplier(SkillModel skillModel, CharModel charModel)
        {
            dmgMultPercentAttackType = GetAttackTypeDmgMultPercent(skillModel.attackType);
            dmgMultPercentRaceType = GetRaceTypeDmgMultPercent(charModel.raceType);
            cultType_dmgMultPercent = GetCultTypeDmgMultPercent(charModel.cultType); 

            // charStateModel from charStateService...
            // loop thru all the charStates 
            // add up all the values to a mult percent value

            // temp trait and permanent trait have no Dmg Mult or mod for now 


            return 0;
        }
        
        [Header("ATTACK TYPE  MultPercent")]
        float dmgMultPercentAttackType;
        public float dmgMultPercentMelee_AttackType;
        public float dmgMultPercentRanged_AttackType;
        public float dmgMultPercentRemote_AttackType;

        [Header("RACE TYPE  MultPercent")]   
        public float dmgMultPercentRaceType;
        public float dmgMultPercentRaceType_Animal;
        public float dmgMultPercentRaceType_Beastmen;
        public float dmgMultPercentRaceType_Elf;
        public float dmgMultPercentRaceType_Pygmy;
        public float dmgMultPercentRaceType_Human;
        public float dmgMultPercentRaceType_Undead;
        public float dmgMultPercentRaceType_Dwarf;
        public float dmgMultPercentRaceType_Huwawa;
        public float dmgMultPercentRaceType_Monster;
        public float dmgMultPercentRaceType_Ogre;
        public float dmgMultPercentRaceType_Orc;
        public float dmgMultPercentRaceType_Goblin;
        public float dmgMultPercentRaceType_Demon;

        [Header("CULTURE TYPE MultPercent")]
        float cultType_dmgMultPercent;
        public float CanineCultType_dmgMultPercent;
        public float FelineCultType_dmgMultPercent;
        public float ArachnidCultType_dmgMultPercent;
        public float ReptileCultType_dmgMultPercent;
        public float BoudingoCultType_dmgMultPercent;

        [Header("SKILL INCLI DMG MOD")]     // ADDITION To be added to initial dmg mod value 
        public float PhysicalskillIncl_dmgMod;
        public float MagicalskillIncl_dmgMod;


        [Header("CHAR STATE DMG MULT")]

        public float BleedLowDOT_dmgMultPercentCharState;	                    
        public float BleedMedDOT_dmgMultPercentCharState;
        public float BleedHighDOT_dmgMultPercentCharState;
        public float Blinded_dmgMultPercentCharState;
        public float BurnLowDOT_dmgMultPercentCharState;
        public float BurnMedDOT_dmgMultPercentCharState;
        public float BurnHighDOT_dmgMultPercentCharState;
        public float CheatedDeath_dmgMultPercentCharState;
        public float Confused_dmgMultPercentCharState;
        public float Despaired_dmgMultPercentCharState;
        public float Faithful_dmgMultPercentCharState;
        public float Fearful_dmgMultPercentCharState;
        public float Feebleminded_dmgMultPercentCharState;
        public float Hexed_dmgMultPercentCharState;
        public float Horrified_dmgMultPercentCharState;
        public float PoisonedLowDOT_dmgMultPercentCharState;
        public float PoisonedMedDOT_dmgMultPercentCharState;
        public float PoisonedHighDOT_dmgMultPercentCharState;
        public float Rooted_dmgMultPercentCharState;
        public float Shocked_dmgMultPercentCharState;
        public float Soaked_dmgMultPercentCharState;
        float GetAttackTypeDmgMultPercent(AttackType attacktype)
        {
           
            switch (attacktype)
            {
                case AttackType.None:
                    return 1;                     
                case AttackType.Melee:
                    return dmgMultPercentMelee_AttackType;                     
                case AttackType.Ranged:
                    return dmgMultPercentRanged_AttackType;                    
                case AttackType.Remote:
                    return dmgMultPercentRemote_AttackType;                   
                default:
                    return 1;                     
            }
        }

        float GetRaceTypeDmgMultPercent(RaceType raceType)
        {
            switch (raceType)
            {
                case RaceType.None:
                    return 1;
                case RaceType.Animal:
                    return dmgMultPercentRaceType_Animal;
                case RaceType.Beastmen:
                    return dmgMultPercentRaceType_Beastmen;
                case RaceType.Elf:
                    return dmgMultPercentRaceType_Elf;
                case RaceType.Pygmy:
                    return dmgMultPercentRaceType_Pygmy;
                case RaceType.Human:
                    return dmgMultPercentRaceType_Human;
                case RaceType.Undead:
                    return dmgMultPercentRaceType_Undead;
                case RaceType.Dwarf:
                    return dmgMultPercentRaceType_Dwarf;
                case RaceType.Huwawa:
                    return dmgMultPercentRaceType_Huwawa;
                case RaceType.Monster:
                    return dmgMultPercentRaceType_Monster;
                case RaceType.Ogre:
                    return dmgMultPercentRaceType_Ogre;
                case RaceType.Orc:
                    return dmgMultPercentRaceType_Orc;
                case RaceType.Goblin:
                    return dmgMultPercentRaceType_Goblin;
                case RaceType.Demon:
                    return dmgMultPercentRaceType_Demon;
                default:
                    return 1;

            }
        }

        float GetCultTypeDmgMultPercent(CultureType cultType)
        {
            switch (cultType)
            {
                case CultureType.None:
                    return 1;
                case CultureType.Canine:
                    return CanineCultType_dmgMultPercent;
                case CultureType.Feline:
                    return FelineCultType_dmgMultPercent;
                case CultureType.Arachnid:
                    return ArachnidCultType_dmgMultPercent;
                case CultureType.Reptile:
                    return ReptileCultType_dmgMultPercent;
                case CultureType.Boudingo:
                    return BoudingoCultType_dmgMultPercent;
                default:
                    break;
            }

            return 1;
        }

        float GetCharStateMultPercent(CharStateName charStateName)
        {
            switch (charStateName)
            {
                case CharStateName.None:
                    return 1;
                case CharStateName.BleedLowDOT:
                    return BleedLowDOT_dmgMultPercentCharState;
                case CharStateName.BleedMedDOT:
                    return BleedMedDOT_dmgMultPercentCharState;  
                case CharStateName.BleedHighDOT:
                    return BleedHighDOT_dmgMultPercentCharState; 
                case CharStateName.Blinded:
                    return Blinded_dmgMultPercentCharState;
                case CharStateName.BurnLowDOT:
                    return BurnLowDOT_dmgMultPercentCharState;
                case CharStateName.BurnMedDOT:
                    return BurnMedDOT_dmgMultPercentCharState;
                case CharStateName.BurnHighDOT:
                    return BurnHighDOT_dmgMultPercentCharState;
                case CharStateName.CheatedDeath:
                    return CheatedDeath_dmgMultPercentCharState; 
                case CharStateName.Confused:
                    return Confused_dmgMultPercentCharState; 
                case CharStateName.Despaired:
                    return Despaired_dmgMultPercentCharState;    
                case CharStateName.Faithful:
                    return Faithful_dmgMultPercentCharState;
                case CharStateName.Fearful:
                    return Fearful_dmgMultPercentCharState;
                case CharStateName.Feebleminded:
                    return Feebleminded_dmgMultPercentCharState;
                case CharStateName.Hexed:
                    return Hexed_dmgMultPercentCharState;
                case CharStateName.Horrified:
                    return Horrified_dmgMultPercentCharState;
                case CharStateName.PoisonedLowDOT:
                    return PoisonedLowDOT_dmgMultPercentCharState;
                case CharStateName.PoisonedMedDOT:
                    return PoisonedMedDOT_dmgMultPercentCharState;
                case CharStateName.PoisonedHighDOT:
                    return PoisonedHighDOT_dmgMultPercentCharState;
                case CharStateName.Rooted:
                    return Rooted_dmgMultPercentCharState;
                case CharStateName.Shocked:
                    return Shocked_dmgMultPercentCharState;
                case CharStateName.Soaked:
                    return Soaked_dmgMultPercentCharState; 
                default:                    
                    return 1f; 
            }
        }

        float GetSkillIncDmgMod(SkillInclination skillIncl)
        {
            switch (skillIncl)
            {
                case SkillInclination.None:
                    return 1; 
                case SkillInclination.Physical:
                    return PhysicalskillIncl_dmgMod;
                case SkillInclination.Magical:
                    return MagicalskillIncl_dmgMod;           
                default:
                    return 1;
                                   
            }


        }


    }
}

