using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class SharpShootersVest : SagaicGewgawBase,Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.SharpshootersVest;

        //  +14% dmg mod on Ranged Physical skills
        // 	+2-3 Dodge	
        // 	-30% Thirst	mod
        // 	+2-3 Willpower
        public int itemId { get; set; }
        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.SharpshootersVest;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }

        int valDodge =0 , valWp = 0; 
        public override void GewGawSagaicInit()
        {
             valDodge = UnityEngine.Random.Range(2, 4);
             valWp = UnityEngine.Random.Range(1, 4);
            string str = $"Ranged Skills: +14 Dmg mod";
            displayStrs.Add(str);
            str = $"+{valDodge} Dodge, +{valWp} Wp";
            displayStrs.Add(str);
            str = "-30% Thirst";
            displayStrs.Add(str);

        }

 
 
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;

            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, AttribName.dodge, valDodge, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);


            buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, AttribName.willpower, valWp, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);



        }

        public override void UnEquipSagaic()
        {
            foreach (int i in buffIndex)
            {
                charController.buffController.RemoveBuff(i);
            }
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

