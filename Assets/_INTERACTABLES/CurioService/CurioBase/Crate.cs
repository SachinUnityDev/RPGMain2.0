using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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
            }
            else
            {
                //  Potion or Herb Food or Fruit Scroll or Tool Gewgaw Potion or Herb Gem
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
                    lootTypes.Add(ItemType.Scrolls);
                else
                    lootTypes.Add(ItemType.Tools);

                lootTypes.Add(ItemType.GenGewgaws);

                if (chance2.GetChance())
                    lootTypes.Add(ItemType.Potions);
                else
                    lootTypes.Add(ItemType.Herbs);

                lootTypes.Add(ItemType.Gems);
            }
        }

        public override void CurioInteractWithTool()
        {
            
        }
    }
}