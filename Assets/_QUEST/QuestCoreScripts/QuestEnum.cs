using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class QuestEnum 
    {
        
    }
    public enum QuestMode
    {
        None,
        Stealth,
        Exploration,
        Taunt,
    }
    public enum QuestType
    {
        None, 
        Main, 
        Side, 
        Bounty, 
        Companion, 
    }

    public enum QuestNames
    {
        None, 
        LostMemory, // main 
        ThePowerWithin, // main
        APlaceOfEvil,// main
        RatInfestation,// side        
        JungleTribes,// side        
        HuntInTheWilderness,//Bounty
        CrewMemberNeeded,// Bounty
    }

    public enum ObjNames
    {
        None, 
        RetrieveTheDebt, // main
        GoBackToKhalid,// main
        AttendToJob,// main
        VisitKhalid,// main
        VisitTemple,// main
        RequestRayyan,// main
        CheckoutTheShips,// main
        GoBackToSoothsayer,// main
        CleanseTheSewers,// side
        ReturnBackToTavernkeeper,//side
        TravelDeepInsideTheJungle,// side
        DiscoverTheDeepdark,// side
        BeatTheMngwaNightclaw,// side
        TravelIntoTheWilderness,// Bounty
    }
    public enum QuestState
    {
        None, 
        ToBeTaken, 
        InProgress, 
        Completed, 
    }

    public enum CityENames
    {
        None,
        StreetUrchin,
        HotPriestess,
        Woodstocker,
    }

    public enum CityEState
    {
        None, 
        Locked, 
        UnLockedNAvail,
        UnAvailable, // pre req not matched but start condition met(Unlocked)
        Completed,
    }

    public enum MapENames
    {
        None,
        MigratoryBirds,
        BandOfBanditsOne,
        BandOfBanditsTwo,
        BuffaloStampede,
    }

    public enum Nodes
    {
        None,
        RatInfest, 
        ShipRats,
        HuntWild,
    }
    public enum NodeType
    {
        None, 
        TownNode, 
        QuestNode,
        MapENode,
        CampNode,
    }
    // town  node.. time line completion for the quest 
    // can have a quest node to travel or cannot .. is travellable 
    // directly ////



    // Map ENode ....can be node connection
    // 

}






