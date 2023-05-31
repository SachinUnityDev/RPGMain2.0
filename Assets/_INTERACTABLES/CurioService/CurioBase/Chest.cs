using System.Collections;
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
            if (chance < 30f)
            {
                //Nothing happens                
            }
            else if (chance < 60f)
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                , charCtrl.charModel.charID, CharStateName.Blinded, TimeFrame.EndOfRound, 12);
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                        , charCtrl.charModel.charID, CharStateName.BurnHighDOT);
                }
            }
            else
            {
                float chance2 = 50f;
                lootTypes.Add(ItemType.GenGewgaws);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Fruits);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Tools);
                else
                    lootTypes.Add(ItemType.Scrolls);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.PoeticGewgaws);
                else
                    lootTypes.Add(ItemType.GenGewgaws);

                lootTypes.Add(ItemType.Gems);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Potions);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Fruits);
                else
                    lootTypes.Add(ItemType.Herbs);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.SagaicGewgaws);
                else
                    lootTypes.Add(ItemType.GenGewgaws);
            }
        }
        public override void CurioInteractWithTool()
        {
            float chance = 30f;
            if (chance.GetChance())
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {
                    // Fire Res range 1-2
                    charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,AttribName.fireRes
                                                ,UnityEngine.Random.Range(1,3));
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName, charCtrl.charModel.charID
                                                , CharStateName.Blinded, TimeFrame.EndOfRound, 12);
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                        , charCtrl.charModel.charID, CharStateName.BurnHighDOT);
                }
            }
            else
            {
                float chance2 = 50f;
                lootTypes.Add(ItemType.GenGewgaws);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Fruits);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Tools);
                else
                    lootTypes.Add(ItemType.Scrolls);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.PoeticGewgaws);
                else
                    lootTypes.Add(ItemType.GenGewgaws);
                lootTypes.Add(ItemType.Gems);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Potions);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Fruits);
                else
                    lootTypes.Add(ItemType.Herbs);
                if (chance2.GetChance())
                    lootTypes.Add(ItemType.SagaicGewgaws);
                else
                    lootTypes.Add(ItemType.GenGewgaws);
            }
        }

    }
}