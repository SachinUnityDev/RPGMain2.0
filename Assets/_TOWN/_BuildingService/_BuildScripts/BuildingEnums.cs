using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{

    public class BuildingEnums
    {
    }
    public enum BuildingStatus
    { 
        None, 
        Locked, // locked in the current game
        UnLocked, // Buildings UnLock as Quest progress
        Available, // Can be Opened 
        UnAvailable, // UnLocked but due to current game condition
                        // as in certain days in a week cannot access the building         
    }

    public enum NPCInteractType
    {
        None, // to be shown on NPC panel
        Talk, 
        Trade,
    }
    public enum BuildInteractType
    {
        None,
        Bounty, // tavern 
        Purchase, // house,                 
        Chest, // stash slots increse on upgrade
        Enchant,// temple 
        Fermentation,     // house          
        Music, // house
        Safekeep,// safekeeper ..Bank ??
        Serve,// ship// tavern 
        Smuggle, // ship    
        Trophy,// tavern 
        EndDay, // house
        Provision, // ABbas changes provision in town... 
        DryFood, 
        XXX,
        Brawl, 
        ClearMind, 
    }
    
    public enum HousePurchaseOpts
    {        
        
        UpgradeBed,
        UpgradeStash,
        Fermentor,
        Dryer,
        Cora,
        Drums,
    }

}