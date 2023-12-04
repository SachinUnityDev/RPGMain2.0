using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Common;

namespace Combat
{
    public class HurlForward : PerkBase
    {
        public override PerkNames perkName => PerkNames.HurlForward; 

        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.Breathtaker };

        public override string desc => "(PR: B1 + B2)/n  Hits all enemies /n Shuffle /n -2 Focus, 2 rds /n 0 --> 2 rd cd ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.Cleave;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> targetDynaCopy = new List<DynamicPosData>();
        public override void AddTargetPos()
        {
            if (skillModel == null) return;

            skillModel.targetPos.Clear();
            targetDynaCopy.Clear(); 
            CombatService.Instance.mainTargetDynas.Clear(); 
            for (int i = 1; i < 8; i++)
            {
                CellPosData cell = new CellPosData(CharMode.Enemy, i);
                DynamicPosData dyna = GridService.Instance.gridView?.GetDynaFromPos(cell.pos, cell.charMode);
                if(dyna != null)
                {
                    skillModel.targetPos.Add(cell);
                    CombatService.Instance.mainTargetDynas.Add(dyna);
                    targetDynaCopy.Add(dyna);
                }        
            }
        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.cd = 2;
            skillModel.attackType = AttackType.Ranged;

            SkillService.Instance.SkillWipe += skillController.allPerkBases
                                                .Find(t => t.skillName == skillName && t.perkName == PerkNames.Breathtaker).WipeFX1;
        }

        public override void ApplyFX1()
        {
            GridService.Instance.ShuffleCharMode(CharMode.Enemy);
        }

        public override void ApplyFX2()
        {
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>()
                    .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , AttribName.focus, -2, TimeFrame.EndOfRound, skillModel.castTime, false);
            }

        }
        //public override void SkillEnd()
        //{
        //    base.SkillEnd();
        //    //foreach (DynamicPosData dyna in targetDynaCopy)
        //    //{
        //    //    dyna.charGO.GetComponent<CharController>()
        //    //        .ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.focus, 2);

        //    //}

        //}
        public override void ApplyFX3()
        {
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }
        public override void DisplayFX1()
        {
            str0 = $"Target everyone and <style=Move>Shuffle</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        // wipe breathtaker display 0 (and cleave 2)
        public override void DisplayFX2()
        {
            str1 = $"-2 Focus";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        //public override void DisplayFX1()
        //{
        //    str0 = $"<style=Move> Shuffle </style>";
        //    SkillService.Instance.skillModelHovered.AddDescLines(str0);
        //}

        //public override void DisplayFX2()
        //{
        //    str1 = $" -2 <style=Attributes>Focus</style>, {skillModel.castTime} rd ";
        //    SkillService.Instance.skillModelHovered.AddDescLines(str1);
        //}

        public override void DisplayFX3()
        {
           
        }

        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Attack type: Melee -> Ranged";                
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Target everyone and shuffle";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);            
            perkDesc = "- 2 Focus";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Cd: 1 rd-> 2 rds";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }

}



//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.Cleave;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.HurlForward;

//        public override PerkType perkType => PerkType.B3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.Breathtaker };

//        public override string desc => "hurl forward";

//        private float _chance = 0;
//        public override float chance { get => _chance; set => _chance = value; }

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                              .Find(t => t.skillName == skillName);

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
//        }
//        public override void BaseApply()
//        {

//        }

//        public override void ApplyFX1()
//        {
//        }

//        public override void ApplyFX2()
//        {
//        }

//        public override void Tick()
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
