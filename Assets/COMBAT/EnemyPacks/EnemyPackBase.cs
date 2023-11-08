using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables; 

namespace Combat
{
    public abstract class EnemyPackBase
    {
        public abstract EnemyPackName enemyPackName { get; }        
        public List<ItemDataWithQty> lootData = new List<ItemDataWithQty>();        
        protected int val1, val2, val3, val4;
        public abstract void InitEnemyPack();        
        public abstract void EnemyPackInteract();
    }
}