using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;

namespace Interactables
{
    public class BlueOnyx : GemBase, ISocketable, IEnchantable, Iitems
    {
        public override GemName gemName => GemName.BlueOnyx;
        public override GemType gemType => GemType.Divine;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public override GemModel gemModel { get; set; }
        public override float singleBoost { get; set; }   // SUPPORT JOB SPEC BOOST
        public override float doubleBoost { get; set; }
        public ArmorType armorType { get; }
        public WeaponType weaponType { get; }
        public int currCharge { get; set; }

        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemName.BlueOnyx;

        public int maxInvStackSize { get; set; }
        public SlotType invType { get; set; }
        public void OnHoverItem()
        {

        }
        public void EnchantGemFX()
        {
            
        }

        public void OnSocket()
        {
            SocketGemFX();
        }

        public void OnUnSocket()
        {
        }
        public bool IsEnchantable()
        {
            return false;
        }

        public bool IsSocketAble()
        {
            return false;
        }
        public void SocketGemFX()
        {
            int val = (int)Random.Range(6f, 12f);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)gemName, charID
                                                , StatsName.darkRes, val, TimeFrame.Infinity, 1, true);
        }

  
    }
}
