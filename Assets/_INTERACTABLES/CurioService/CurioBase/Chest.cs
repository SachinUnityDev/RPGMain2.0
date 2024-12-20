﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;
using Common;

namespace Quest
{
    public class Chest : CurioBase
    {
        public override CurioNames curioName => CurioNames.Chest;
 
        public override void InitCurio()
        {
        }
        //        %30 nothing happens 
        //        %30 debuff -  Blinded, 12 rds, HighBurn	+1-2 Fire Res permanently		
        //       %40 loot Gen Gewgaw	Food or Fruit	Potion or Herb	Tool or Scroll	Gen Gewgaw or Poetic Gewgaw	Gem	Food or Potion	Fruit or Herb	GenGew or SagaicGew
        public override void CurioInteractWithoutTool()
        {
            float chance = Random.Range(0f, 100f);
            if (chance < 20f)
            {
                //Nothing happens
                resultStr = "You couldn't open it with bare hands...";
            }
            else if (chance < 50f)
            {
                Fx1();
            }
            else
            {
                Fx2();
            }
        }
        public override void CurioInteractWithTool()
        {
            float chance = 25f;
            if (chance.GetChance())
            {
                Fx1();
            }
            else
            {
                Fx2();
            }
        }
        void Fx1()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                // Fire Res range 1-2
                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID, AttribName.fireRes
                                            , UnityEngine.Random.Range(1, 3));
                charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName, charCtrl.charModel.charID
                                            , CharStateName.Blinded, TimeFrame.EndOfRound, 12);
                charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                    , charCtrl.charModel.charID, CharStateName.Burning);
            }
            resultStr = "Explosion!";
            resultStr2 = "Burning\nBlinded, 12 rds\n+1-2 Fire Res";
        }

        void Fx2()
        {
            lootTypes.Clear();
            float chance2 = 50f;
            QuestMode questMode = QuestMissionService.Instance.currQuestMode; 

            if(questMode == QuestMode.Stealth || questMode == QuestMode.Exploration
                                              || questMode == QuestMode.Taunt)
            {
                lootTypes.Add(ItemType.GenGewgaws); //1

                if (chance2.GetChance())  //2
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Fruits);

                if (chance2.GetChance())//3
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);
                if (chance2.GetChance()) //4
                    lootTypes.Add(ItemType.Tools);
                else
                    lootTypes.Add(ItemType.Scrolls);
                if (chance2.GetChance())  //5
                    lootTypes.Add(ItemType.PoeticGewgaws);
                else
                    lootTypes.Add(ItemType.GenGewgaws);
                lootTypes.Add(ItemType.Gems); //6 
            }
            if (questMode == QuestMode.Exploration
                                || questMode == QuestMode.Taunt)
            {
                if (chance2.GetChance())  //7
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Potions);
                if (chance2.GetChance()) //8
                    lootTypes.Add(ItemType.Fruits);
                else
                    lootTypes.Add(ItemType.Herbs);
            }
            if (questMode == QuestMode.Taunt)
            {
                if (chance2.GetChance()) //9
                    lootTypes.Add(ItemType.SagaicGewgaws);
                else
                    lootTypes.Add(ItemType.GenGewgaws);
            }
            resultStr = "What are chests for, eh?";
            resultStr2 = "Loot gained";
            Transform curioViewTrans = CurioService.Instance.curioView.gameObject.transform; 
            LootService.Instance.lootController.ShowLootTableInLandscape(lootTypes, curioViewTrans); 
        }
    }
}