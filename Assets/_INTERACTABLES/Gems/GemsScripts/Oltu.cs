using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common; 

namespace Interactables
{
    public class Oltu : GemBase, ISocketable, Iitems
    {
        public override GemName gemName => GemName.Oltu;
        public override GemType gemType => GemType.Support;
        public override CharNames charName { get; set; }
        public override int charID { get; set; }
        public override GemModel gemModel { get; set; }
        public override float singleBoost { get; set; }   // SUPPORT JOB SPEC BOOST
        public override float doubleBoost { get; set; }
        public ArmorType armorType { get; }
        public WeaponType weaponType { get; }
        public int currCharge { get; set; }

        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemName.Oltu; 

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }

        public void OnHoverItem()
        {

        }

        public void SocketGemFX()
        {
            int val = (int)Random.Range(8f, 12f);

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)gemName, charID
                    , StatsName.earthRes, val, TimeFrame.Infinity, 1, true);
        }
    

        public void OnSocket()
        {
            SocketGemFX();
        }

        public void OnUnSocket()
        {

        }
  

        public bool IsSocketAble()
        {
            return true;
        }

        
    }
}

