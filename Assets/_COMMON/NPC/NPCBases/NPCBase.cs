using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public abstract class NPCBase
    {
        public abstract NPCNames nPCNames { get; }
        public abstract BuildingNames buildingNames { get; }
        public abstract void NPCInit();

        [Header("NPC item in stock inv ")]
        public List<NPCWeeklyStockData> allWeeklyStock = new List<NPCWeeklyStockData>(); 

    }
}