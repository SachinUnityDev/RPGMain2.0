using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using Interactables;

namespace Town
{ 
    /// <summary>
    ///  To provide a as singleton for the interior interactions of the building
    /// </summary>

    public class BuildingIntService : MonoSingletonGeneric<BuildingIntService>
    {

        public event Action<Iitems> OnTrophyableTavern; 


        [Header("Char and NPC Select")]
        public CharNames selectChar;
        public NPCNames selectNpc; 

        public AllBuildSO allBuildSO; 
        public HouseController houseController;
        public TavernController tavernController; 
        public TempleController templeController;
        void Start()
        {
            houseController = GetComponent<HouseController>();
            templeController = GetComponent<TempleController>();    
        }

        #region 

        public void On_TrophyableTavern(Iitems item)
        {
            OnTrophyableTavern?.Invoke(item);
        }

        #endregion 



    }
}

