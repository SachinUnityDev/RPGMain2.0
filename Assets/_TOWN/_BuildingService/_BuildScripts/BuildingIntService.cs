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
        public event Action<Iitems, TavernSlotType> OnTrophyableTavern; 

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
            tavernController = GetComponent<TavernController>();    
        }

        #region 
        public void On_TrophyableTavern(Iitems item, TavernSlotType tavernSlotType)
        {
            if (tavernSlotType == TavernSlotType.Trophy)
            {
                // check if item need to be added 
                tavernController.tavernModel.trophyOnWall = item;
            }
            if (tavernSlotType == TavernSlotType.Pelt)
            {
                // check if item need to be added 
                tavernController.tavernModel.peltOnWall = item;
            }
            OnTrophyableTavern?.Invoke(item, tavernSlotType);
        }

        #endregion 



    }
}

