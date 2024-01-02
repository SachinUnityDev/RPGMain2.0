using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Combat
{


    public class RatPack1 : EnemyPackBase
    {
        public override EnemyPackName enemyPackName => EnemyPackName.RatPack1;

        public override void InitEnemyPack()
        {
            lootData.Clear();
            if (35f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 1);                 
                lootData.Add(itemQty);  
            }else if (15f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 2);
                lootData.Add(itemQty);
            }

        }
    }
}