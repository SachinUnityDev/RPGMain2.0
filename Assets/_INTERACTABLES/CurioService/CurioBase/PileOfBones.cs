using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;


namespace Quest
{
    public class PileOfBones : CurioBase
    {
        public override CurioNames curioName => CurioNames.PileOfBones;
        public override void InitCurio()
        {
        }
        //        %10 nothing happens 
        //        %30debuff -6 fort orign until eoq	Horrified until eoq	+1 Willpower Permanently			
        //      %60 loot 

        public override void CurioInteractWithoutTool()
        {
            float chance = Random.Range(0f, 100f);
            if (chance < 10f)
            {
                //Nothing happens
                resultStr = "Useless skulls, nothing else...";
            }
            else if (chance < 40f)
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                    , charCtrl.charModel.charID, CharStateName.Horrified, TimeFrame.EndOfQuest,1);
                    charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                                                                       AttribName.willpower, 1);
                    charCtrl.buffController.ApplyBuff(CauseType.Curios, (int)curioName
                                            , charCtrl.charModel.charID, AttribName.fortOrg, -6, TimeFrame.EndOfQuest,1, false);
                }
                resultStr = "Bones… A reminder of death and evoker of fear.";
                resultStr2 = "Fort Org debuff, eoq\nHorrified, eoq\n+1 Willpower";

            }
            else
            {
                //  Herb or Potion	Gewgaw or Trade Good	Tool or Scroll	Gem	Food or Herb
                Fx1();
            }
        }
        public override void CurioInteractWithTool()
        {
            Fx1();
        }

        void Fx1()
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
                    lootTypes.Add(ItemType.TradeGoods);
                else
                    lootTypes.Add(ItemType.GenGewgaws);
                if (chance2.GetChance()) //3
                    lootTypes.Add(ItemType.Scrolls);
                else
                    lootTypes.Add(ItemType.Tools);
            }
            if (questMode == QuestMode.Exploration || questMode == QuestMode.Taunt)
            {
                lootTypes.Add(ItemType.Gems); //4
            }
            if (questMode == QuestMode.Taunt)
            {
                if (chance2.GetChance()) //5
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Herbs);
            }
            resultStr = "Dead does not need loot, why not take it?";
            resultStr2 = "Loot gained";
            Transform curioViewTrans = CurioService.Instance.curioView.gameObject.transform;
            LootService.Instance.lootController.ShowLootTable(lootTypes, curioViewTrans);

        }

    }
}