using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using Common;

namespace Interactables
{
    public class Ruri : GemBase,Iitems, ISocketable, IEnchantable, ISupportable
    {
        public override GemName gemName => GemName.Ruri;
        public override GemType gemType => GemType.Divine; 
        public override CharNames charName { get; set; }
        public override int charID { get ; set; }
        public override GemModel gemModel { get; set; }
        public override float singleBoost { get; set; }   // SUPPORT JOB SPEC BOOST
        public override float doubleBoost { get ; set ; }
        public ArmorType armorType { get; }
        public WeaponType weaponType { get; }
        public int currCharge { get; set; }

        public ItemType itemType => ItemType.Gems;

        public int itemName => (int)GemName.Ruri;

        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }

        float val = 0;
        
        public void OnHoverItem()
        {

        }
        public void SocketGemFX()
        {
            val = Random.Range(6f, 10f);

            charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
                    , StatsName.waterRes, val, TimeFrame.Infinity, 1, true);



        }

        void SupportGemEventHandler(GemModel gemModel, CharNames charName, float multiplier)
        {
            // it has to be two way.. 


        }
        public void SupportGemFX(float multiplier)
        {
            val = multiplier * val;
            charController.buffController.ApplyBuff(CauseType.Gems, (int)gemName, charID
                    , StatsName.waterRes, val, TimeFrame.Infinity, 1, true);
        }
        public void EnchantGemFX()
        {
            gemModel.gemState = GemState.Enchanted;
            // weapon skill activated 

        }



        public void OnSocket()
        {
            gemModel.gemState = GemState.Socketed;
            SocketGemFX();
        }
        public void OnUnSocket()
        {
            GemService.Instance.DisposeGem(this); 
        }
        public bool IsEnchantable()
        {
            if (gemModel.gemState == GemState.None)
                return true;
            else return false;
        }

        public bool IsSocketAble()
        {
            if (gemModel.gemState != GemState.None)
                return true;
            else
                return false;
        }

        
    }



}
