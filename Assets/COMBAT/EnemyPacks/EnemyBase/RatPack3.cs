using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{


    public class RatPack3 : EnemyPackBase
    {
        public override EnemyPackName enemyPackName => EnemyPackName.RatPack3;

        public override void InitEnemyPack()
        {
            lootData.Clear();
            if (50f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 1);
                lootData.Add(itemQty);
            }
            else if (30f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 2);
                lootData.Add(itemQty);
            }

        }
        public override void EnemyPackInteract()
        {

        }
    }
}