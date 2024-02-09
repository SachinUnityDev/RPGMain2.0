using Combat;
using Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Town;
using UnityEngine;



namespace Interactables
{
    public class HornsAndBalls : SagaicGewgawBase, Iitems
    {
        public override SagaicGewgawNames sagaicGewgawName => SagaicGewgawNames.HornsAndBalls;
        public int itemId { get; set; }
        public ItemType itemType => ItemType.SagaicGewgaws;
        public int itemName => (int)SagaicGewgawNames.HornsAndBalls;
        public int maxInvStackSize { get; set; }
        public SlotType invSlotType { get; set; }
        public int slotID { get; set; }
        public List<int> allBuffs { get; set; } = new List<int>();
        public Currency currency { get; set; }
        int valFort = 0; 
        public override void GewGawSagaicInit()
        {
            valFort = UnityEngine.Random.Range(4, 7);
            string str = $"When Starving: +4 Vigor";
            displayStrs.Add(str);
            str = "Upon Crit: +2 Acc until eoc";
            displayStrs.Add(str);
            str = $"Upon No Patience: Gain {valFort} Fortitude";
            displayStrs.Add(str);
        }

        //   +4 vigor when Starving(buff)  
        //    Gain +2 Acc until eoc upon Crit(buff) 
        //    Gains 4-6 Fortitude upon No Patience(instant buff)  
        public override void EquipGewgawSagaic()
        {
            CharStatesService.Instance.OnCharStateStart += OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd += OnCharStateEnd;
            CombatEventService.Instance.OnDamageApplied += OnCritHit;
            SkillService.Instance.OnSkillUsed += OnSkillUsed;
        }
        void OnCharStateStart(CharStateModData charStateModData)
        {
            if (charStateModData.charStateName != CharStateName.Starving) return;
             int buffID =   charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                             (int)sagaicGewgawName, charStateModData.causeByCharID,AttribName.vigor,
                             +4, TimeFrame.Infinity, -1, true); 
            buffIndex.Add(buffID);
        }

        void OnCharStateEnd(CharStateModData charStateData)
        {
            charController.buffController.RemoveBuff(buffIndex[0]);// vigor buff  
        }

        void OnCritHit(DmgAppliedData dmgAppliedData)
        {
            if (dmgAppliedData.targetController.charModel.charID != charController.charModel.charID) return;
            if (dmgAppliedData.strikeType != StrikeType.Crit) return;
            int buffID = charController.buffController.ApplyBuff(CauseType.SagaicGewgaw,
                             (int)sagaicGewgawName, charController.charModel.charID, AttribName.acc,
                             +2, TimeFrame.EndOfCombat, 1, true);
            buffIndex.Add(buffID); // id # 1 
        }

        void OnSkillUsed(SkillEventData skillEventData)
        {
            if (skillEventData.strikerController != charController.strikeController) return; 
            if(skillEventData.skillModel.skillName  == SkillNames.NoPatience)
            {
                charController.ChangeStat(CauseType.SagaicGewgaw, (int)sagaicGewgawName, charController.charModel.charID
                    , StatName.fortitude, UnityEngine.Random.Range(4,6),true); 
            }
            return; 
        }
        public override void UnEquipSagaic()
        {
            CharStatesService.Instance.OnCharStateStart -= OnCharStateStart;
            CharStatesService.Instance.OnCharStateEnd -= OnCharStateEnd;

            CombatEventService.Instance.OnDamageApplied -= OnCritHit;

            SkillService.Instance.OnSkillUsed -= OnSkillUsed;
            charController.buffController.RemoveBuff(buffIndex[0]);// vigor buff // no other buff sticks  
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
