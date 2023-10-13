using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;


namespace Combat
{
    public class NoBleedNoLuck : PerkBase
    {
        public override PerkNames perkName => PerkNames.NoBleedNoLuck;

        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.BloodyAxe, PerkNames.TooHighBleed };

        public override string desc => "(PR: B1 + B2) /n If Crits on Bleeding target, gain +1 Luck until EOQ(Stacks up) ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker; 

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        // on damage applied check if Baran hit crit 
        // chk if targets is bleeding 
        // for each bleeding target give him +1 luck EOQ
        public override void BaseApply()
        {
            base.BaseApply();
            targetController.damageController.OnDamageApplied -= ApplyCrit;
            targetController.damageController.OnDamageApplied += ApplyCrit; 


        }

        void ApplyCrit(DmgAppliedData dmgAppliedData)
        {
            if(dmgAppliedData.strikeType == StrikeType.Crit)
            {
                if (targetController.GetComponent<CharStateController>().HasCharDOTState(CharStateName.BleedLowDOT))
                {
                    charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.luck, 1, TimeFrame.EndOfQuest, 1, true);
                }   
            }
        }
        public override void ApplyFX1()
        {


        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            targetController.damageController.OnDamageApplied -= ApplyCrit;
        }
        public override void DisplayFX1()
        {
            str0 = $"If Crit on Bleeding target, gain +1<style=Attributes> Luck </style>until EOQ (Stacks up to 6)";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

  
    }
}

//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.EarthCracker;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.NoBleedNoLuck;

//        public override PerkType perkType => PerkType.B3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.BloodyAxe, PerkNames.TooHighBleed };

//        public override string desc => "No bleed no luck";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                          .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);

//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//        }  

//        public override void SkillSelected()
//        {
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            // get first target on the same lane
//        //    targetDyna = GridService.Instance.gridView
//        //             .GetDynaFromPos(skillModel.targetPos[0].pos, skillModel.targetPos[0].charMode);
//        //    adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);
//        }
//        public override void BaseApply()
//        {
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            //if (_chance.GetChance())
//            //{
//            //    CharStatesService.Instance.SetCharState(targetDyna.charGO, CharStateName.Rooted);
//            //    targetDyna.charGO.GetComponent<CharController>().dmgController
//            //        .ApplyDamage(charController, DamageType.Physical, 50f, false);

//            //}
//        }

//        public override void ApplyFX2()
//        {           
//        }

//        public override void ApplyFX3()
//        {           
//        }

//        public override void ApplyFX4()
//        {           
//        }


//        public override void DisplayFX1()
//        {
//            str1 = $"If Crit to Bleeding +1 Luck ,EOQ";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }
//        public override void DisplayFX2()
//        {            
//        }

//        public override void DisplayFX3()
//        {            
//        }

//        public override void DisplayFX4()
//        {           
//        }

//        public override void PostApplyFX()
//        {

//        }

//        public override void PreApplyFX()
//        {

//        }

//        public override void RemoveFX1()
//        {
//        }

//        public override void RemoveFX2()
//        {           
//        }

//        public override void RemoveFX3()
//        {            
//        }

//        public override void RemoveFX4()
//        {            
//        }

//        public override void SkillEnd()
//        {           
//        } 

//        public override void Tick()
//        {            
//        }

//        public override void WipeFX1()
//        {

//        }

//        public override void WipeFX2()
//        {            
//        }

//        public override void WipeFX3()
//        {           
//        }

//        public override void WipeFX4()
//        {           
//        }