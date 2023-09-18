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
                questMode = QuestMissionService.Instance.currQuestMode; 
                if (questMode == QuestMode.Stealth || questMode == QuestMode.Exploration
                                          || questMode == QuestMode.Taunt)
                {
                    if (chance2.GetChance()) //1
                        lootTypes.Add(ItemType.Potions);
                    else
                        lootTypes.Add(ItemType.Herbs);

                    if (chance2.GetChance()) //2
                        lootTypes.Add(ItemType.Foods);
                    else
                        lootTypes.Add(ItemType.Fruits);

                    if (chance2.GetChance()) //3
                        lootTypes.Add(ItemType.Gems);
                    else
                        lootTypes.Add(ItemType.TradeGoods);

                    lootTypes.Add(ItemType.GenGewgaws);//4
                }
                if (questMode == QuestMode.Exploration
                                         || questMode == QuestMode.Taunt)
                {
                    if (chance2.GetChance()) //5
                        lootTypes.Add(ItemType.Foods);
                    else
                        lootTypes.Add(ItemType.Fruits);

                }
                if (questMode == QuestMode.Taunt)
                {
                    if (chance2.GetChance()) //6
                        lootTypes.Add(ItemType.Scrolls);
                    else
                        lootTypes.Add(ItemType.Gems);
                }


                resultStr = "Loot shines upon your face. Take them, all yours!";
                resultStr2 = "Loot gained";

                Transform curioViewTrans = CurioService.Instance.curioView.gameObject.transform;
                LootService.Instance.lootController.ShowLootTableInLandscape(lootTypes, curioViewTrans);
            }
        }
        public override void CurioInteractWithTool()
        {

        }


    }
}