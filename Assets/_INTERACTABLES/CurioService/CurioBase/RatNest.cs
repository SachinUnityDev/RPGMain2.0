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
            resultStr2 = "Sickness gained\n Poisoned";

        }
        void Fx2()
        {
            lootTypes.Clear();
            float chance2 = 50f;
            questMode = QuestMissionService.Instance.currQuestMode;
            if (questMode == QuestMode.Stealth || questMode == QuestMode.Exploration
                                           || questMode == QuestMode.Taunt)
            {
                if (chance2.GetChance()) //1
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Fruits);

                if (chance2.GetChance()) //2
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);

                lootTypes.Add(ItemType.GenGewgaws);//3

                if (chance2.GetChance()) //4
                    lootTypes.Add(ItemType.Foods);
                else
                    lootTypes.Add(ItemType.Gems);

                if (chance2.GetChance()) //5
                    lootTypes.Add(ItemType.Tools);
                else
                    lootTypes.Add(ItemType.Scrolls);
            }
            if (questMode == QuestMode.Exploration
                                               || questMode == QuestMode.Taunt)
            {
                if (chance2.GetChance()) //6
                    lootTypes.Add(ItemType.GenGewgaws);
                else
                    lootTypes.Add(ItemType.SagaicGewgaws);
            }
            if (questMode == QuestMode.Taunt)
            {
                if (chance2.GetChance()) //7
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);
            }

                

            resultStr = "Couple of valuable items hidden in this dirty nest.";
            resultStr2 = "Loot gained";
            Transform curioViewTrans = CurioService.Instance.curioView.gameObject.transform;
            LootService.Instance.lootController.ShowLootTable(lootTypes, curioViewTrans);

        }
    }
}