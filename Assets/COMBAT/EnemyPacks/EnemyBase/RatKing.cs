using Combat;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class RatKing : EnemyPackBase
    {
        public override EnemyPackName enemyPackName => EnemyPackName.RatKing;

        public override void InitEnemyPack()
        {
            lootData.Clear();
            if (30f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.GenGewgaws, (int)GenGewgawNames.AncientTabletOfEarth,
                                    GenGewgawQ.Folkloric), 1);
                lootData.Add(itemQty);
                itemQty = new ItemDataWithQty
                                 (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 2);
                lootData.Add(itemQty);
            }
            else if (50f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.GenGewgaws, (int)GenGewgawNames.SkullBeltOfTheRat,
                                    GenGewgawQ.Folkloric), 1);
                lootData.Add(itemQty);
                itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Ingredients, (int)IngredNames.RatFang), 2);
                lootData.Add(itemQty);
            }
            else if (50f.GetChance())
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.Potions, (int)PotionNames.ElixirOfWillpower), 1);
                lootData.Add(itemQty);
            }
            else
            {
                ItemDataWithQty itemQty = new ItemDataWithQty
                                    (new ItemData(ItemType.SagaicGewgaws, (int)SagaicGewgawNames.EasyFit), 1);
                lootData.Add(itemQty);
            }

        }

    }
}