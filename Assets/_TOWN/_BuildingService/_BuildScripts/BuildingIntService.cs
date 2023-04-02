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
        public event Action<Iitems, TavernSlotType> OnItemWalled; 

        [Header("Char and NPC Select")]
        public CharNames selectChar;
        public NPCNames selectNpc; 

        public AllBuildSO allBuildSO; 

        public HouseController houseController;
        public TavernController tavernController; 
        public TempleController templeController;
        public MarketController marketController; 
        void Start()
        {
            houseController = GetComponent<HouseController>();
            templeController = GetComponent<TempleController>();
            tavernController = GetComponent<TavernController>();    
            marketController= GetComponent<MarketController>();

        }

        #region 
        //public bool CanItemBeWalled(Iitems item, TavernSlotType tavernSlotType)
        //{
        //    Iitems currItem = null;
        //    if (tavernSlotType == TavernSlotType.Trophy)
        //    {
        //        currItem = tavernController.tavernModel.trophyOnWall;
        //    }


        //}

        public void On_ItemWalled(Iitems item, TavernSlotType tavernSlotType)
        {
            
            if (tavernSlotType == TavernSlotType.Trophy)
            {
               tavernController.tavernModel.trophyOnWall = item; 

            }
                
            if (tavernSlotType == TavernSlotType.Pelt)
            {
                
                tavernController.tavernModel.peltOnWall = item;
            }
            OnItemWalled?.Invoke(item, tavernSlotType);
        }

        #endregion 



    }
}
