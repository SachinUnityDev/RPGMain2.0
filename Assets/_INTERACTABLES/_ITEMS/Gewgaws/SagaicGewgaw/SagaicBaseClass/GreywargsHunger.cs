using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class GreywargsHunger : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.GreyWargsHunger;

        //+2 morale for each Kugharian in party(self included)
        //+30% Hunger	
        //+14- 20 Air Res	
        //+20% dmg until eoc when First Blood

        public int itemId { get; set; }
        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.GreyWargsHunger;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        int valAir;

        public override void GewGawSagaicInit()
        {
            valAir = UnityEngine.Random.Range(14 , 21);

          
        }

        void OnCharStateStart(CharStateModData charStateModData)
        {
            if(charStateModData.charStateName == CharStateName.FirstBlood)
            {
                AttribData attribDataMin = charController.GetAttrib(AttribName.dmgMin);
                AttribData attribDataMax = charController.GetAttrib(AttribName.dmgMax);

                int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                           (int)sagaicGewgawName, AttribName.dmgMin, attribDataMin.currValue*1.2f, TimeFrame.Infinity, -1, true);
                buffIndex.Add(buffID);

                buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                   (int)sagaicGewgawName, AttribName.dmgMax, attribDataMax.currValue * 1.2f, TimeFrame.Infinity, -1, true);
                buffIndex.Add(buffID);
            }
        }
        public override void EquipGewgawSagaic()
        {
            charController = InvService.Instance.charSelectController;
            foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
            {
                if (c.charModel.cultType == CultureType.Kugharian)
                {
                    int buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                       (int)sagaicGewgawName, AttribName.morale, 2, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);
                }
            }
            int buffID2 = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
               (int)sagaicGewgawName, AttribName.airRes, valAir, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID2);

            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
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
        }

        public void OnHoverItem()
        {
           
        }
    }
}


