using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Combat;
using System.Linq;
using UnityEditor;
using Town;
using UnityEditor.ShaderGraph.Internal;

namespace Interactables
{
    public class ItemController : MonoBehaviour
    {
        /// <summary>
        /// Item controller distrubted controller  i.e. each party member has its own on quest 
        /// performs all item related actions i.e socket, enchant, equip, unequip 
        /// </summary>
        public CharController charController; 
        public ItemModel itemModel;

        public float multFx = 1f;
        List<string> displayStrs; 
        public void Init()
        {
            itemModel = new ItemModel();
            charController = gameObject.GetComponent<CharController>(); 
        }

        void Start()
        {
            displayStrs = new List<string>();
            Init(); 
        }
        #region GEM, ENCHANT AND REMOVE
        public bool EnchantTheWeaponThruScroll(GemBase gemBase)
        {
            GemNames gemName = gemBase.gemName; 
            if(ItemService.Instance.CanEnchantGemThruScroll(charController, gemName))
            {
                itemModel.gemEnchanted = (Iitems)gemBase; 
                itemModel.gemChargeData = new GemChargeData(gemName);
                return true;
            }
            return false;
        }
        public void WeaponSkillUsed()        // On Weapon Skills used 
        {
            itemModel.gemChargeData.chargeRemaining--; 
            if(itemModel.gemChargeData.chargeRemaining <= 0)
            {
                itemModel.gemChargeData.chargeRemaining = 0;
            }
        }     
        
        public bool EnchantInTemple()
        {
            // to be linked to the town scene 
            return false;
        }
        #endregion

        #region  SOCKET RELATED 
        public void OnSocketSupportGem(GemBase gemBase)
        {
      
            UpdateMultValue();
                
               ISupportGem supportGem = gemBase as ISupportGem;                
                supportGem.ClearSocketBuffs();
                supportGem.SocketedFX();
            if (itemModel.divItemsSocketed[0] == null && itemModel.divItemsSocketed[1] == null)
                return;

            foreach (var divGems in itemModel.divItemsSocketed)
            {
                IDivGem gem = divGems as IDivGem;
                if (gem == null) continue;
                gem.ClearSocketBuffs();
                gem.SocketedFX(multFx);
            }

        }
        public void OnSocketDivineGem(GemBase gemBase)
        {
            Iitems item = gemBase as Iitems;

            UpdateMultValue();
            foreach (var divGems in itemModel.divItemsSocketed)
            {                
                IDivGem gem = divGems as IDivGem;
                if (gem == null) continue;
                gem.ClearSocketBuffs();
                gem.SocketedFX(multFx);
            }
        }
        void UpdateMultValue()
        {
            int count = 0;
            if (itemModel.supportItemSocketed == null) return;

            ISupportGem supportGem = itemModel.supportItemSocketed as ISupportGem;
            foreach (Iitems item in itemModel.divItemsSocketed)
            {
                foreach(GemNames gemName in supportGem.divineGemsSupported)
                {
                    if(item.itemName == (int)gemName)
                    {
                        count++; 
                    }
                }
            }
            if (count == 0)
                multFx = 1f;
            else if (count == 1)
                multFx = 1.3f;
            else if (itemModel.divItemsSocketed[0].itemName 
                            == itemModel.divItemsSocketed[1].itemName)
                multFx = 1.6f;
            else if (count == 2)
                multFx = 1.3f;
        }
        # endregion 
        /// <summary>
        /// OC data is the potions OC data
        /// <summary>
        public void ChecknApplyOC(OCData ocData, TempTraitName temptraitName, IOverConsume ocBase)
        {
            if (charController.tempTraitController.HasTempTrait(temptraitName))            
                return;

            float wt = itemModel.AddOCData(ocData); 
            if (wt !=0)
            {
                if (wt.GetChance())
                {
                    ocBase.ApplyOC_FX();
                }
            }
        }

        #region  GEWGAW ACTIONS 
        public void OnGewgawEquip()
        {
            CharController charController = InvService.Instance.charSelectController; 
            
            // remove gew gaw from the inv slot 

            // add to active gewslot of the char Select in the inv 

        }

        public void OnGewGawUnQEquip()
        {


        }

        #endregion


    }
}

//public void AddItemToActiveInvLs(Iitems Iitem)
//{
//    if(Iitem.itemType == ItemType.Potions)
//    {
//        itemsInPotionActiveInv.Add(Iitem);

//    }else
//    {
//        itemInGewgawActiveInv.Add(Iitem);   
//    }

//}