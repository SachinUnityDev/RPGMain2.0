﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CommonEnums
    {
    }
    public enum GameScene // Game Scene that Includes all scenes and Panels like map,job and camp 
    {       
        INTRO,
        TOWN,    
        QUEST,
        COMBAT,
        CORE,
        CAMP,
        JOBS,       
        MAPINTERACT,            
    };

    public enum SceneName  // exact scene Name to replace the Str
    {
        INTRO,
        TOWN, 
        QUEST, 
        COMBAT, 
        CORE,
    }
    public enum GameState
    {
        OnIntroAnimStart,
        OnNewGameStart,
        OnNewGameStartComplete, // means Game In Progress
        OnLoadGameStart,
        OnLoadGameStartComplete, // means Game In Progress
        OnGameQuit,
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
        Fled,
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
        UnAvailable_InPartyLocked, 
       // UnAvailable_PartyLeader, 
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
        EndOfWeek, 
    }
    public enum StateFor   // char States
    {
        Mutual, // Allies + enemies  
        Heroes, // Allies only
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



