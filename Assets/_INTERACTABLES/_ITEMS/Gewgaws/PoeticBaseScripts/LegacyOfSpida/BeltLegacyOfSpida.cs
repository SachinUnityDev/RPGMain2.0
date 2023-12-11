using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{


    public class BeltLegacyOfSpida : PoeticGewgawBase, Iitems , IEquipAble
    {
        public override PoeticGewgawNames poeticGewgawName => PoeticGewgawNames.BeltLegacyOfTheSpida;
        public ItemType itemType => ItemType.PoeticGewgaws;
        public int itemName => (int)PoeticGewgawNames.BeltLegacyOfTheSpida;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }
        public Currency currency { get; set; }

        int valER; 
        public override void PoeticInit()
        {
            valER = UnityEngine.Random.Range(7, 13);            
            string str = $"+{valER} Earth Res";
            displayStrs.Add(str);
            str = $"";
            displayStrs.Add(str);
            str = $"<i>Verse 1: Your webby covers my belly</i>";
            displayStrs.Add(str);
        }
        public override void EquipGewgawPoetic()
        {
    
            int index =
                charController.buffController.ApplyBuff(CauseType.PoeticGewgaw, (int)poeticGewgawName
                , charController.charModel.charID, AttribName.earthRes, valER, TimeFrame.Infinity, -1, true);
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