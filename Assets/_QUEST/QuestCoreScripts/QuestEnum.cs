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
    public enum CurioNames
    {
        None,
        AnimalBones,
        AnimalCarcass,
        Barrel,
        BatNest,
        Bush,
        Cactus,
        Cart,
        Chest,
        Crate,
        Cocoon,
        DuneHole,
        Fountain,
        Hive,
        Lorestone,
        MineVein,
        PileOfBones,
        PileOfThrash,
        RatNest,
        Sack,
        SacrificialAltar,
        ShrineOfRuru,
        TreeTrunk,
        Whetstone,
        Any,
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
        Locked,
        UnLocked,
        Completed,       
    }
    public enum QRoomState
    {
        None,
        Prep,
        AutoWalk,
        Walk,
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
    public enum QuestENames
    {
        None,
        Spidaboy,
        RatArmy,
    }

    public enum QBarkNames
    {
        None,
        Qbark_001,
        Qbark_002,
        Qbark_003,
        Qbark_004,
        Qbark_005,
        Qbark_006,
        Qbark_007,
        Qbark_008,
        Qbark_009,
        Qbark_010,
        Qbark_011,
        Qbark_012,
        Qbark_013,
        Qbark_014,
        Qbark_015,
        Qbark_016,
        Qbark_017,
        Qbark_018,
        Qbark_019,
        Qbark_020,
        Qbark_021,
        Qbark_022,
        Qbark_023,
        Qbark_024,
        Qbark_025,
        Qbark_026,
        Qbark_027,
        Qbark_028,
        Qbark_029,
        Qbark_030,

    }

    public enum Traps
    {
        None, 
        TrapSewers,  
        TrapCave,
        TrapJungle, 
        TrapRainforest, 
        TrapDesert, 
        TrapSwamp, 

    }

}






