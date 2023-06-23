using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
namespace Quest
{
    public class Sack : CurioBase
    {
        public override CurioNames curioName => CurioNames.Sack;
        
        public override void InitCurio()
        {
        }
        //        %10 nothing happens		
        //      %90 loot 
        public override void CurioInteractWithoutTool()
        {
            float chance = 10f;
            if (chance.GetChance())
            {
                // nothing happens 
                resultStr = "Nothing inside the torn sack. Someone was quicker than you.";
            }
            else
            {
                lootTypes.Clear();
                float chance2 = 50f;
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Fruits);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Gems);
                else
                    lootTypes.Add(ItemType.TradeGoods);

                lootTypes.Add(ItemType.GenGewgaws);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Fruits);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Scrolls);
                else
                    lootTypes.Add(ItemType.Gems);

                resultStr = "Loot shines upon your face. Take them, all yours!";
                resultStr2 = "Loot gained";

                Transform curioViewTrans = CurioService.Instance.curioView.gameObject.transform;
                LootService.Instance.lootController.ShowLootTable(lootTypes, curioViewTrans);
            }
        }
        public override void CurioInteractWithTool()
        {

        }


    }
}