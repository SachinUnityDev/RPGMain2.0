using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables;


namespace Quest
{
    public class RatNest : CurioBase
    {
        public override CurioNames curioName => CurioNames.RatNest;
        public override void InitCurio()
        {
        }

        public override void CurioInteractWithoutTool()
        {
            float chance = Random.Range(0f, 100f);
            if (chance < 10f)
            {
                //Nothing happens
                resultStr = "Nothing but dead rats.";

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
                charCtrl.tempTraitController.ApplyTempTrait(CauseType.Curios, (int)curioName, charCtrl.charModel.charID
                                                         , TempTraitName.Nausea);
                charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                         , charCtrl.charModel.charID, CharStateName.PoisonedHighDOT);
            }
            resultStr = "Well, no good would come out of vermins.";
                

        }
        void Fx2()
        {
            float chance2 = 50f;
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Fruits);

            if (chance2.GetChance())
                lootTypes.Add(ItemType.Potions);
            else
                lootTypes.Add(ItemType.Herbs);

            lootTypes.Add(ItemType.GenGewgaws);

            if (chance2.GetChance())
                lootTypes.Add(ItemType.Foods);
            else
                lootTypes.Add(ItemType.Gems);

            if (chance2.GetChance())
                lootTypes.Add(ItemType.Tools);
            else
                lootTypes.Add(ItemType.Scrolls);

            if (chance2.GetChance())
                lootTypes.Add(ItemType.GenGewgaws);
            else
                lootTypes.Add(ItemType.SagaicGewgaws);
            
            if (chance2.GetChance())
                lootTypes.Add(ItemType.Potions);
            else
                lootTypes.Add(ItemType.Herbs);

            resultStr = "Couple of valuable items hidden in this dirty nest.";
        }
    }
}