using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{


    public class RingLegacyOfSpida : PoeticGewgawBase, Iitems , IEquipAble

    {
        //1-3 focus
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.RingLegacyOfTheSpida;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.RingLegacyOfTheSpida; 
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }
        int valFocus; 
        public override void PoeticInit()
        {
            valFocus = UnityEngine.Random.Range(1, 4);
            string str = $"+{valFocus} Focus";
            displayStrs.Add(str);
            str = $"";
            displayStrs.Add(str);          
            str = $"<i>Verse 3: Going out of salty lake, you lost the ring already</i>";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
          //  charController = InvService.Instance.charSelectController;

            int index =
                charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, AttribName.focus, valFocus, TimeFrame.Infinity, -1, true);
            allBuffIds.Add(index);
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
            this.charController= charController;
            EquipGewgawPoetic();
        }

        public void RemoveEquipableFX()
        {
            UnEquipPoetic();
            charController = null; 
        }
    }
}