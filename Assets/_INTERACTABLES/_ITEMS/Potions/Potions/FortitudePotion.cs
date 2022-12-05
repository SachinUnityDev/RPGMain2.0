using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class FortitudePotion : PotionsBase, IEquipAble, IConsumable, ISellable,Iitems
    {
        public override PotionName potionName => PotionName.FortitudePotion;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionName.FortitudePotion;
        public SlotType invSlotType { get; set; }
        public int maxInvStackSize { get; set; }
        public int itemId { get; set; }
        public List<int> allBuffs { get; set; }

        public void OnHoverItem()
        {

        }
        public override void PotionApplyFX()
        {
            float value = Random.Range(22f, 28f);

            int buffId =
                charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                        , StatsName.fortitude, value, TimeFrame.Infinity, -1, true);           
            allBuffs.Add(buffId);    

            buffId =
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                      , StatsName.fortOrg, -2, TimeFrame.EndOfQuest, 1, true);
            allBuffs.Add(buffId);
        }

        public override void PotionEndFX()
        {

        }

        //   **************************CONSUMABLE ***************
  

        public void ApplyConsumableFX()
        {
        }
        //   **************************CONSUMABLE ***************

        public void ApplyDisposable()
        {

        }
   
        public void ApplySellable()
        {

        }

      
    }


}

