﻿using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;

namespace Quest
{
    public class Cocoon : CurioBase
    {
        public override CurioNames curioName => CurioNames.Cocoon;
        public override void InitCurio()
        {
        }
        //        %10 nothing happens 
        //        %40 debuff -  -2 to -3 Haste until eoq	+2-3 Earth Res permanently
        //       %50 loot GenGew	Herb or Potion	Food or Fruit	Herb or Potion	GenGew or PoeticGew	Tool or Gem	Food or Fruit
        public override void CurioInteractWithoutTool()
        {
            float chance = Random.Range(0f, 100f);
            if (chance < 10f)
            {
                //Nothing happens
                resultStr = "Spida had clutched it all and left nothing for you.";
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
            float chance = 20f;
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
                                                AttribName.earthRes, UnityEngine.Random.Range(2,4));

                charCtrl.buffController.ApplyBuff(CauseType.Curios, (int)curioName,
                                charCtrl.charModel.charID, AttribName.haste
                                , UnityEngine.Random.Range(-2, -4), TimeFrame.EndOfQuest, 1, false);

            }
            resultStr = "The web might feel a bit itchy.";
            
        }
        void Fx2()
        {
            float chance2 = 50f;
            lootTypes.Add(ItemType.GenGewgaws);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Potions);
            else
                lootTypes.Add(ItemType.Herbs);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Fruits);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Herbs);
            else
                lootTypes.Add(ItemType.Potions);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.PoeticGewgaws);
            else
                lootTypes.Add(ItemType.GenGewgaws);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Tools);
            else
                lootTypes.Add(ItemType.Gems);
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Fruits);
            else
                lootTypes.Add(ItemType.Foods);

            resultStr = "Carefully woven web still holds bunch of items together.";
        }
    }
}