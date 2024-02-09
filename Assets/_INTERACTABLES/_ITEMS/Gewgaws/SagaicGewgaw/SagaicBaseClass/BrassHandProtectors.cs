using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Interactables
{
    public class BrassHandProtectors : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.BrassHandProtectors;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.BrassHandProtectors;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }
        //-6-10 Air Res +8-12 Earth Res
        //	+1-2 armor

        //	Returned Regards perk doesnt cost stamina.And applicable to all alies, not just adjacent.
        //	Hone Blades has 2 rds cast time

        int valAir, valEarth; 

        public override void GewGawSagaicInit()
        {
            valAir = UnityEngine.Random.Range(6, 11);
            valEarth = UnityEngine.Random.Range(8, 13);
            string str = $"+{valEarth} Earth Res,-{valAir} Air Res"; 
            displayStrs.Add(str);
            str = "+1-2 Armor";
            displayStrs.Add(str);
            str = "Hone Blades cd: 2 rds";
            displayStrs.Add(str);
            str = "Returned Regards: No Stm cost and applies to all allies";
            displayStrs.Add(str);

        }
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, AttribName.airRes, -valAir, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, AttribName.earthRes, valEarth, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                        (int)sagaicGewgawName, AttribName.armorMin, 1, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                  (int)sagaicGewgawName, AttribName.armorMax, 2, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);

        }

        public override void UnEquipSagaic()
        {
            
        }

        public void InitItem(int itemId, int maxInvStackSize)
        {
            this.itemId = itemId;
            this.maxInvStackSize = maxInvStackSize;
            GewGawSagaicInit();
        }

        public void OnHoverItem()
        {
        }
    }
}

