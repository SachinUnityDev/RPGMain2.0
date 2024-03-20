using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    [Serializable]
    public class WeaponModel
    {   
        public CharNames charName;
        public GemNames gemName;
        public WeaponState weaponState;
        public int chargeRemaining = 0;
        public int noOfTimesRecharged = 0;
        public int maxChargeLimit = 0; 
        public WeaponModel(WeaponSO weaponSO)
        {
            this.charName = weaponSO.charName;
            this.gemName = weaponSO.gemName;
            this.weaponState = weaponSO.weaponState;
            chargeRemaining = 0; 
            noOfTimesRecharged= 0;
            maxChargeLimit = 12; 
        }

        public bool ExhaustedGem()
        {
            ItemData itemData = new ItemData(ItemType.Gems, (int)gemName);
            ItemDataWithQty itemDataWithQty = new ItemDataWithQty(itemData, 1);
           bool hasGem= InvService.Instance.invMainModel.HasItemInQtyCommOrStash(itemDataWithQty); 
            if (hasGem)
            {
                noOfTimesRecharged = 0;
                SetRechargeValue();
                if (InvService.Instance.invMainModel.RemoveItemFrmCommInv(itemData))
                    InvService.Instance.invMainModel.RemoveItemFrmStashInv(itemData); 
            }
            return hasGem; 
        }

        public void SetRechargeValue()
        {   
            switch (noOfTimesRecharged)
            {
                case 0:
                    maxChargeLimit = 12;
                    weaponState = WeaponState.Enchanted;
                    break;
                case 1:
                    maxChargeLimit = 9;
                    weaponState = WeaponState.Enchanted;
                    break;
                case 2:
                    maxChargeLimit = 6;
                    weaponState = WeaponState.Enchanted;
                    break;
                case 3:
                    maxChargeLimit = 3;
                    weaponState = WeaponState.Enchanted;
                    break;           
            }
            chargeRemaining = maxChargeLimit;
            noOfTimesRecharged++;            
        }



        public bool IsChargeZero()
        {
            if (chargeRemaining == 0)
                return true;
            else
                return false; 
        }
    }
    public enum WeaponState
    {
        Unenchantable, 
        Unidentified, 
        Identified, 
        Enchanted, 
        Rechargeable, // 0 charge 
        Exhausted, 
    }
  

}
