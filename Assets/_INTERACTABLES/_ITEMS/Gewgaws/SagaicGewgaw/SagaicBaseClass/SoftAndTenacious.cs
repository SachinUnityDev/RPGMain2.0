using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class SoftAndTenacious : SagaicGewgawBase, Iitems, IEquipAble
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.SoftAndTenacious;

        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.SoftAndTenacious;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public List<int> allBuffs { get; set; }
        public int itemId { get; set; }

        //When Unslakable: +36-45% Dmg(attribute)
        //First 3 rds of combat: Soaked	
        //+1 Morale and Luck per Beastmen in party
        int dmgChgVal;
        int valFortorg; 
        public override void GewGawSagaicInit()
        {
            dmgChgVal = UnityEngine.Random.Range(36, 47);
            valFortorg = UnityEngine.Random.Range(3, 6);
            string str = $"When Unslakable: +36-46% Dmg";
            displayStrs.Add(str);
            str = $"First 3 rds of combat: Soaked";
            displayStrs.Add(str);
            str = "+1 Morale and Luck per Beastmen in party";
            displayStrs.Add(str);
            str = $"{valFortorg} Fort Org";
            displayStrs.Add(str);
        }

 
        void OnStartOfCombat()
        {       
            charController.charStateController
                            .ApplyCharStateBuff(CauseType.SagaicGewgaw, (int)sagaicGewgawName
                            , charController.charModel.charID, CharStateName.Soaked, TimeFrame.EndOfRound, 3);
        }

        void OnCharStateChg(CharStateModData charStateModData)
        {
            // TO BE REVIEWED 
            ApplyIfUnslackableFx();
        }

        void ApplyIfUnslackableFx()
        {
            AttribData statData = charController.GetAttrib(AttribName.damage);
            float dmgMult = 1+ (dmgChgVal / 100f);
            int buffID = charController.buffController.ApplyBuffOnRange
                (CauseType.SagaicGewgaw, charController.charModel.charID,
                  (int)sagaicGewgawName, AttribName.damage, (int)statData.maxRange * dmgMult,
                  (int)statData.minRange * dmgMult, TimeFrame.Infinity, -1, true);
            buffIndex.Add(buffID);
        }

        public override void EquipGewgawSagaic()
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

        public void ApplyEquipableFX()
        {
            
        }

        public void RemoveEquipableFX()
        {
           
        }
    }
}

