using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Boss1 : PassiveSkillBase
    {
        public override PassiveSkillName passiveSkillName => PassiveSkillName.Boss1;

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override string desc => "";

        List<int> allBuffId = new List<int>();  

        public override void ApplyFX()
        {
            if (allBuffId.Count > 0) return; /// apply buffs only once
            charController.charStateController.ApplyImmunityBuff(CauseType.PassiveSkillName, (int)passiveSkillName
                 , charController.charModel.charID, CharStateName.Despaired, TimeFrame.Infinity, 1);

            charController.charStateController.ApplyImmunityBuff(CauseType.PassiveSkillName, (int)passiveSkillName
                 , charController.charModel.charID, CharStateName.Rooted, TimeFrame.Infinity, 1);

            charController.charStateController.ApplyDOTImmunityBuff(CauseType.PassiveSkillName, (int)passiveSkillName
              , charController.charModel.charID, CharStateName.PoisonedLowDOT, TimeFrame.Infinity, 1, true);

            charController.charStateController.ApplyDOTImmunityBuff(CauseType.PassiveSkillName, (int)passiveSkillName
              , charController.charModel.charID, CharStateName.BleedLowDOT, TimeFrame.Infinity, 1, true);

            charController.OnStatChg += OnHPBelow;
            charController.OnStatChg += OnHPAbove;
     

        }
     
        void OnHPBelow(StatModData statModData)
        {
            if (statModData.statModified != StatName.health)
                return; 
            StatData HpStat = charController.GetStat(StatName.health);
            if (HpStat.currValue < HpStat.maxLimit * 0.12f)
            {
                int buffId = charController.buffController.ApplyBuff(CauseType.PassiveSkillName, (int)passiveSkillName
                 , charController.charModel.charID, AttribName.dodge, +3, TimeFrame.Infinity, 1, true);
                allBuffId.Add(buffId);

                allBuffId.AddRange(charController.buffController.BuffAllRes(CauseType.PassiveSkillName, (int)passiveSkillName
                  , charController.charModel.charID, +20, TimeFrame.Infinity, 1, true));


                AttribData armorMax = charController.GetAttrib(AttribName.armorMax);
                AttribData armorMin = charController.GetAttrib(AttribName.armorMin);

                float maxIncr = armorMax.currValue * 0.5f;
                float minIncr = armorMin.currValue * 0.5f;

                buffId =
                charController.buffController.ApplyBuff(CauseType.PassiveSkillName, (int)passiveSkillName
                    , charController.charModel.charID, AttribName.armorMax, maxIncr, TimeFrame.Infinity, 1, true);
                allBuffId.Add(buffId);

                buffId = charController.buffController.ApplyBuff(CauseType.PassiveSkillName, (int)passiveSkillName
                            , charController.charModel.charID, AttribName.armorMin, minIncr, TimeFrame.Infinity, 1, true);
                allBuffId.Add(buffId);
            }
        }
        void OnHPAbove(StatModData statModData)
        {
            if (statModData.statModified != StatName.health)
                return;
            StatData HpStat = charController.GetStat(StatName.health);
            if (HpStat.currValue < HpStat.maxLimit * 0.12f)
            {
                if(allBuffId.Count > 0)
                {
                    foreach (int buffId in allBuffId)
                    {
                        charController.buffController.RemoveBuff(buffId);
                    }
                    
                }
            }
            allBuffId.Clear(); 
        }

        protected override void DisplayFX1(PassiveSkillName passiveSkillName)
        {
            if (this.passiveSkillName != passiveSkillName) return;
            str0 = "Immune to two of DOTs";
            PassiveSkillService.Instance.descLines.Add(str0);
            str1 = "Immune to two of Despaired, Rooted, Feebleminded, Confused";
            PassiveSkillService.Instance.descLines.Add(str1);
            //str2 = "Feebleminded, Confused";
            //PassiveSkillService.Instance.descLines.Add(str2);
            //str4 = "+20 All Res and +3 Dodge";
            //PassiveSkillService.Instance.descLines.Add(str4);
            str3 = "When Hp < 12%: +50% Armor, +20 All Res and +3 Dodge";
            PassiveSkillService.Instance.descLines.Add(str3);            
          
        }

    }
}