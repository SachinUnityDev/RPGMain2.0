using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class CloakProachertoolset : PoeticGewgawBase, Iitems, IEquipAble
    {
    
        //+2-3 dodge
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.CloakPoachersToolset;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.CloakPoachersToolset;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
  
        int valDodge;
        public override void PoeticInit()
        {
            valDodge = UnityEngine.Random.Range(2, 4);
            string str = $"+{valDodge} Dodge";
            displayStrs.Add(str);
            str = "";
            displayStrs.Add(str);
            str = "<i>Verse 2: Aim: Cover your tracks</i>";
            displayStrs.Add(str);

        }
        public override void EquipGewgawPoetic()
        {
            int index = charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, AttribName.dodge, valDodge, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(index);

        }
        public override void UnEquipPoetic()
        {
            base.UnEquipPoetic();            
        }
        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            PoeticInit();
        }

        public void OnHoverItem()
        {

        }

        public void ApplyEquipableFX(CharController charController)
        {
            this.charController = charController;
            EquipGewgawPoetic();
        }
        public void RemoveEquipableFX()
        {
            UnEquipPoetic();
            charController = null;

        }
    }
}