using Combat;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Interactables
{
    public class EasyFit : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.EasyFit;

        public int itemId { get; set; }

        public ItemType itemType => ItemType.SagaicGewgaws;

        public int itemName => (int)SagaicGewgawNames.EasyFit;

        public int maxInvStackSize { get ;set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();

        //+2 Wp and Vigor ....Upon Dodge: +1 Stamina and Hp Regen, 3 rds

        int dmgChgVal;

        public override void GewGawSagaicInit()
        {   
            dmgChgVal = UnityEngine.Random.Range(36, 46);
           
            string str = "+2 Vigor and Wp";
            displayStrs.Add(str);
            str = "Upon Dodge: +1 Stm and Hp Regen, 3 rds";
            displayStrs.Add(str);   
        }

        public  void ApplyGewGawFX()
        {
            charController = InvService.Instance.charSelectController;
            if (charController.charStateController.HasCharState(CharStateName.Unslakable))
            {
                ApplyIfUnslackableFx();
            }
            CharStatesService.Instance.OnCharStateStart += OnCharStateChg;
            CombatEventService.Instance.OnSOC += OnStartOfCombat;

            // Beastmen FX
            foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
            {
                if (c.charModel.raceType == RaceType.Beastmen)
                {
                    //+1 Morale and Luck
                    int buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                                (int)sagaicGewgawName, AttribName.morale, 1, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);

                    buffID = c.buffController.ApplyBuff(CauseType.SagaicGewgaw, charController.charModel.charID,
                            (int)sagaicGewgawName, AttribName.luck, 1, TimeFrame.Infinity, -1, true);
                    buffIndex.Add(buffID);
                }
            }

        }

        void OnStartOfCombat()
        {
            int buffId = charController.charStateController.ApplyCharStateBuff(CauseType.SagaicGewgaw, (int)sagaicGewgawName
                  , charController.charModel.charID, CharStateName.Soaked, TimeFrame.EndOfRound, 3);
        }

        void OnCharStateChg(CharStateModData charStateModData)
        {
            ApplyIfUnslackableFx();
        }

        void ApplyIfUnslackableFx()
        {
            AttribData statData = charController.GetAttrib(AttribName.damage);
            float dmgMult = dmgChgVal / 100f;
            int buffID = charController.buffController.ApplyBuffOnRange
                (CauseType.SagaicGewgaw, charController.charModel.charID,
                  (int)sagaicGewgawName, AttribName.damage, (int)statData.maxRange * dmgMult,
                  (int)statData.minRange * dmgMult, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        public override void EquipGewgawSagaic()
        {
            ApplyGewGawFX(); 
        }

        public override void UnEquipSagaic()
        {
            CharStatesService.Instance.OnCharStateStart -= OnCharStateChg;
            CombatEventService.Instance.OnSOC -= OnStartOfCombat;
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

