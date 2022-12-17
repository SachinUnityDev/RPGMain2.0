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
        InQuest,
        InCombat,
        InCamp,
        InJobs,
        InIntro,
        InMapInteraction, 
        
    };
    public enum GameMode
    {
        Exploration,
        Stealth,
        Taunt,
    }
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
        Unavailable_Fame, 
        Unavailable_Loc, 
        UnAvailable_Prereq,
        UnAvailable_InParty,
        Unavailable_WhereAboutsUnKnown, //is actually travelling 
    }

    public enum LandscapeNames
    {
        None,
        Cave,
        Jungle,
        Rainforest,
        Savannah,
        Sewers,
        Subterranean,
        Swamp,
        Coast, 
        Desert, 
        DesertCave,
        Highland,
        MysticForest,
        CoffeeForest, 
        Volcano,
        AcidLake,     
        Field,        
    };



    public enum FleeBehaviour
    {
        None,
        Easy,
        NeverFlees,
        Hard,
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
        Permanent,
    }

    public enum QuestCombatMode
    {
        None,
        Stealth,
        Exploration,
        Taunt,
    }

    //public enum DOTType
    //{
    //    NotADOT,
    //    IsDOT,
    //    LowDOT,
    //    MediumDOT,
    //    HighDOT,
    //}

    public enum StateFor   // char States
    {
        Mutual,
        Party,
    }

    //public enum DialogueNames
    //{
    //    None,
    //    KhalidFirst,
    //    MeetKhalid,
    //    RetrieveDebt, 



    //}

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



