using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{


    public class RatNBatPack : EnemyPackBase
    {
        public override EnemyPackName enemyPackName => EnemyPackName.RatNBatPack; 

        public override void InitEnemyPack()
        {
            lootData.Clear();
            if (20f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.GenGewgaws, (int)GenGewgawNames.SilverNecklaceOfWarding,
                                    GenGewgawQ.Lyric), 1);
                lootData.Add(itemQty);
            }
            else if (40f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 1);
                lootData.Add(itemQty);
            }else if (50f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.BatEar), 1);
                lootData.Add(itemQty);
            }
            else 
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.TradeGoods, (int)TGNames.SimpleRing), 1);
                lootData.Add(itemQty);
            }

        }
        public override void EnemyPackInteract()
        {

        }
    }
}