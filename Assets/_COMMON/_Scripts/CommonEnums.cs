using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CommonEnums
    {
    }
    public enum GameState
    {
        None,
        InTown,    
        InQuestRoom,
        InCombat,
        InCamp,
        InJobs,
        InIntro,
        InMapInteraction,         
    };



    public enum GameDifficulty
    {
        Easy, // KIWI
        Medium, // Kiwano
        Hard, // Mangosteen 
    }

    public enum LocationName
    {
        None, 
        Nekkisari, 
        Murabo,
        Maghile,
        Bluetown, 
        Smaeru, 
    }
    public enum StateOfChar
    {
        None,
        Locked,
        UnLocked,
        Dead,
    }

    public enum AvailOfChar
    {
        None,      
        Available, 
        UnAvailable_Fame, 
        UnAvailable_Loc, 
        UnAvailable_Prereq,
        UnAvailable_InParty,
        UnAvailable_WhereAboutsUnKnown, //is actually travelling 
        UnAvailable_Resting,
    }

    public enum LandscapeNames
    {
        None,
        Cave,
        Jungle,
        Rainforest,
        Savannah, //demo
        Sewers, // demo
        Subterranean,
        Swamp,
        Coast, //demo
        Desert, 
        DesertCave,
        Highland,
        MysticForest,
        CoffeeForest, 
        Volcano,
        AcidLake,     
        Field, // demo        
    };



    public enum FleeBehaviour
    {
        None,
        Easy,// 60% flee Combat
        NeverFlees,// 0 flee Combat
        Hard, // 30% flee combat
    }
     public enum CharFleeState
     {
        None,  
        FledCombat, 
        FledQuest, 
     }

    public enum TimeState
    {
        None,
        Day,
        Night,
    }

    public enum TimeFrame
    {
        None,
        EndOfRound,
        EndOfCombat,
        EndOfDay,
        EndOfNight,       
        EndOfQuest,
        Infinity,
    }
    public enum StateFor   // char States
    {
        Mutual, // Heroes + bestiary  
        Heroes, // heroes only
    }

    public enum Levels
    {
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10,
        Level11,
        Level12,
    }

    public enum LvlUpGroup
    {
        None, 
        Group1, 
        Group2,
        Group3,
        Group4, 
        Group5,
    }

    #region ROSTER

    public enum RosterSlotType
    {
        None, 
        CharScrollSlot, 
        PartySetSlot, 
    }


    #endregion


}



