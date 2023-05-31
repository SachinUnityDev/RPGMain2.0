﻿using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables; 

namespace Quest
{
    public class PileOfThrash : CurioBase
    {
        public override CurioNames curioName => CurioNames.PileOfThrash;
        public override void InitCurio()
        {
        }
        //        %10 nothing happens 
        //        %20 debuff 1 Immune to Despaired until eoq	+1 Willpower permanently		
        //       %20 debuff 2  	Gain Nausea trait	-30% Hunger until eoq	-1 Morale until eoq
        //      %50 loot 
        public override void CurioInteractWithoutTool()
        {
            float chance = Random.Range(0f, 100f);
            if (chance < 10f)
            {
                //Nothing happens               
            }
            else if (chance < 30f)
            {
                Fx1(); 
            }
            else if (chance < 50f)
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {
                    // Gain Nausea
                    charCtrl.tempTraitController.ApplyTempTrait(CauseType.Curios, (int)curioName, charCtrl.charModel.charID
                                                                , TempTraitName.Nausea); 

                    //-30 Hunger 
                    
                    //    charCtrl.ChangeStat(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                    //        StatName.hunger, 

                    charCtrl.buffController.ApplyBuff(CauseType.Curios, (int)curioName
                                     , charCtrl.charModel.charID, AttribName.morale, -1, TimeFrame.EndOfQuest, 1, false);
                }
            }
            else
            {
                Fx2();
            }
        }
        public override void CurioInteractWithTool()
        {
            float chance = 30f;
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
                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                                                                            AttribName.willpower, 1);
                charCtrl.charStateController.ApplyImmunityBuff(CauseType.CharState, (int)curioName, charCtrl.charModel.charID
                                  , CharStateName.Despaired, TimeFrame.EndOfQuest, 1);
            }
        }
        void Fx2()
        {
            float chance2 = 50f;
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Fruits);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Herbs);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Potions);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.GenGewgaws);
            lootTypes.Add(ItemType.Herbs);
            lootTypes.Add(ItemType.Scrolls);

        }

    }
}