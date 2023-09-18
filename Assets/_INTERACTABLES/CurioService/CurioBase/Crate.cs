using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Quest
{


    public class Crate : CurioBase
    {
        public override CurioNames curioName => CurioNames.Crate;

        public override void InitCurio()
        {

        }

        //        %30 buff Full hunger relief  Status Gained: Poisoned(low)  +1 Vigor permanently			
        //      %70 loot Potion or Herb  Food or Fruit Scroll or Tool  Gewgaw Potion or Herb  Gem
        public override void CurioInteractWithoutTool()
        {
            float chance = 30f;
            if (chance.GetChance())
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                    , charCtrl.charModel.charID, CharStateName.PoisonedLowDOT);
                    charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                        AttribName.vigor, 1);
                }
                resultStr = "Crate broke down into pieces and you ate the leftover food.";
                resultStr2 = "Full Hunger relief\nPoisoned\n+1 Vigor"; 
            }
            else
            {
                //  Potion or Herb Food or Fruit Scroll or Tool Gewgaw Potion or Herb Gem
                lootTypes.Clear();  
                float chance2 = 50f;
                questMode = QuestMissionService.Instance.currQuestMode;
                if (questMode == QuestMode.Stealth || questMode == QuestMode.Exploration
                                      || questMode == QuestMode.Taunt)
                {
                    if (chance2.GetChance())//1                
                        lootTypes.Add(ItemType.Potions);
                    else
                        lootTypes.Add(ItemType.Herbs);

                    if (chance2.GetChance()) //2
                        lootTypes.Add(ItemType.Foods);
                    else
                        lootTypes.Add(ItemType.Fruits);

                    if (chance2.GetChance())//3
                        lootTypes.Add(ItemType.Scrolls);
                    else
                        lootTypes.Add(ItemType.Tools);

                    lootTypes.Add(ItemType.GenGewgaws);//4
                }
                if (questMode == QuestMode.Exploration
                                     || questMode == QuestMode.Taunt)
                {
                    if (chance2.GetChance())//5
                        lootTypes.Add(ItemType.Potions);
                    else
                        lootTypes.Add(ItemType.Herbs);
                }
                if (questMode == QuestMode.Taunt)
                {
                    lootTypes.Add(ItemType.Gems);//6
                }



                    

                resultStr = "Crate broke down into pieces and you found some loot.";
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