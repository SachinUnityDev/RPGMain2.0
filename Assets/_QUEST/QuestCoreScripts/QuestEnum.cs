using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QuestEnum 
    {
        
    }

    //public enum QuestState
    //{
    //    None,
    //    QuestPrep, 
    //    QuestMain, 
    //}
    public enum QuestMode
    {
        None,
        Stealth,
        Exploration,
        Taunt,
    }
    public enum BountyQuest
    {
        None, 
        HuntInTheWilderness,
        CrewMemberNeeded, 
    }
    public enum SideQuest
    {
        None,
        RatInfestation, 
    }
    public enum CompQuestNames
    {
        None, 
    }

    public enum QMainNames
    {
        None, 
        LostMemory, 
        ThePowerWithin, 
        APlaceOfEvil, 
    }

    public enum QuestObj
    {
        None, 
        RetrieveTheDebt,
        GoBackToKhalid,
        AttendToJob,
        VisitKhalid,
        VisitTemple,
        RequestRayyan,
        CheckoutTheShips,
        GoBackToSoothsayer,
    }
    public enum QuestState
    {
        None, 
        ToBeTaken, 
        InProgress, 
        Completed, 
    }


}






