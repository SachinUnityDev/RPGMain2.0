using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class SnowLeopardsBreath : PotionsBase, Iitems,IEquipAble, IConsumable 
    {
        public override PotionNames potionName => PotionNames.SnowLeopardsBreath; 
        public ItemType itemType => ItemType.Potions;
        public int itemName => (int)PotionNames.SnowLeopardsBreath;        
        public int maxInvStackSize { get ; set ; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        public void OnHoverItem()
        {

        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
        }
        public override void PotionApplyFX()
        {

            PotionSO potionSO = ItemService.Instance.allItemSO.GetPotionSO((PotionNames)itemName);
            int castTime = (int)UnityEngine.Random.Range(potionSO.minCastTime, potionSO.maxCastTime);
            int buffID=
            charController.GetComponent<CharStateController>().ApplyImmunityBuff(CauseType.Potions, (int)potionName, charID
                                , CharStateName.Soaked, TimeFrame.EndOfRound , castTime); // TO BE FILLED 
            allBuffs.Add(buffID);
            // immune to soaked 
            buffID=
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                      , AttribName.waterRes, Random.Range(24f, 37f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
            buffID =
            charController.buffController.ApplyBuff(CauseType.Potions, (int)potionName, charID
                , AttribName.fireRes, Random.Range(-12f, -19f), TimeFrame.EndOfRound, castTime, true);
            allBuffs.Add(buffID);
        }
        public void ApplyConsumableFX()
        {
            PotionApplyFX();
        }

        public void ApplyEquipableFX(CharController charController)
        {
            this.charController = charController;
        }

        public void RemoveEquipableFX()
        {
            charController = null;
        }
    }
}

