using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common; 

namespace Town
{
    //public interface IBuildInteract
    //{
    //    void Bounty();  // tavern 
    //    void Purchase(); // house,                 
    //    void Chest();  // stash slots increse on upgrade
    //    void Enchant();// temple 
    //    void Fermentation();// house          
        
    //    Music, // house
    //    Safekeep,// safekeeper ..Bank ??
    //    Serve,// ship// tavern 
    //    Smuggle, // ship    
    //    Trophy,// tavern 
    //    EndDay, // house
    //    Provision, // ABbas changes provision in town... 
    //    DryFood, 
    //    Rest,
    //    Brawl, 
    //    RechargeWeapon, 

    //}


    public class BuildingService : MonoSingletonGeneric<BuildingService>
    {

        public AllBuildSO allBuildSO; 
        public HouseController houseController; 
        void Start()
        {
            houseController = GetComponent<HouseController>();
        }
        public void PopulateInteractBtns(BuildingNames buildingName)
        {



        }
    }
}

