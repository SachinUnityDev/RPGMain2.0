using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class RatPack2 : EnemyPackBase
    {
        public override EnemyPackName enemyPackName => EnemyPackName.RatPack2;

        public override void InitEnemyPack()
        {
            lootData.Clear();
            if (40f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 1);
                lootData.Add(itemQty);
            }
            else if (20f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 2);
                lootData.Add(itemQty);
            }

        }
 
    }
}