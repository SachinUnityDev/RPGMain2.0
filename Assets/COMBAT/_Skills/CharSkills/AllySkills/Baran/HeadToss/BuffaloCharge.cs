using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class BuffaloCharge : PerkBase
    {
        public override PerkNames perkName => PerkNames.BuffaloCharge;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Cast from 4,5,6,7 /n Hits first target on same lane /n +40% dmg on backrow enemies /n Pull self";

        public override CharNames charName => CharNames.Baran; 

        public override SkillNames skillName => SkillNames.HeadToss;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        DynamicPosData targetDyna; 
        public override void AddTargetPos()
        {
            if (skillModel == null) return;

            targetDyna = GridService.Instance.GetInSameLaneOppParty
                            (new CellPosData(CharMode.Ally, GridService.Instance.GetDyna4GO(charGO).currentPos))[0];

            if (targetDyna != null)
            {
                skillModel.targetPos.Clear();
                skillModel.targetPos.Add(new CellPosData(CharMode.Enemy, targetDyna.currentPos));
            }
        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.castPos.Clear();
            skillModel.castPos.AddRange(new List<int>() { 4, 5, 6, 7 });

            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;
        }


        public override void SkillSelected()
        {
            base.SkillSelected();

            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;

        }
        public override void BaseApply()
        {
            base.BaseApply();

            targetGO = CombatService.Instance.mainTargetDynas[0].charGO;
            targetController = targetGO.GetComponent<CharController>();
        }

        public override void ApplyFX1()
        {
            if (GridService.Instance.IsTargetInBackRow(targetDyna))
                            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill,
                                (int)skillName, DamageType.Physical, (skillModel.damageMod + 40f), false);
            else
                targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical, (skillModel.damageMod), false);
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
          
        }

        public override void ApplyMoveFX()
        {
            GridService.Instance.gridMovement.MovebyRow(CombatService.Instance.mainTargetDynas[0], MoveDir.Forward, 2);

        }

        public override void ApplyVFx()
        {
           
        }



        public override void DisplayFX1()
        {
           
            str1 = $"{skillModel.damageMod}%<style=Physical> Physical </style>";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"+40%<style=Physical> Physical </style>on targets back row";
            SkillService.Instance.skillCardData.descLines.Add(str2);

        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Move> Move </style>forward 2";
            SkillService.Instance.skillCardData.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
         
        }
    }
}


//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.HeadToss;

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.BuffaloCharge;

//        public override PerkType perkType => PerkType.A1;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Buffalo Charge";

//        private float _chance = 60f;
//        public override float chance { get => _chance; set => _chance = value; }
//        List<DynamicPosData> sameLaneTargets = new List<DynamicPosData>();

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                            .Find(t => t.skillName == skillName);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            skillModel.castPos.AddRange(new List<int>() { 4, 5, 6, 7 }); 
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
//                                     && t.skillLvl == SkillLvl.Level0).WipeFX1;
//        }

//        public override void SkillSelected()
//        {
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            // get first target on the same lane
//            sameLaneTargets = GridService.Instance.GetInSameLaneOppParty
//                    (new CellPosData(CharMode.Ally, GridService.Instance.GetDyna4GO(charGO).currentPos));
//            skillController.allSkillBases.Find(t => t.skillName == skillName 
//                                    && t.skillLvl == SkillLvl.Level0).RemoveFX1();
//        }
//        public override void BaseApply()
//        {
//            targetGO = sameLaneTargets[0].charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            CombatEventService.Instance.OnEOR += Tick;
//        }
//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            if (sameLaneTargets[0].currentPos == 5 || sameLaneTargets[0].currentPos == 6 
//                                                    || sameLaneTargets[0].currentPos == 7)
//            {
//                targetController.dmgController.ApplyDamage(charController, DamageType.Physical,
//                    skillModel.damageMod + 40f, false);

//            }
//            else
//            {
//                targetController.dmgController.ApplyDamage(charController, DamageType.Physical, 
//                    skillModel.damageMod, false);
//            }         
//        }

//        public override void ApplyFX2()
//        {
//            DynamicPosData selfDyna = GridService.Instance.GetDyna4GO(charGO);
//            GridService.Instance.gridMovement.MovebyRow(selfDyna, MoveDir.Forward, 1); 

//        }

//        public override void ApplyFX3()
//        {
//        }

//        public override void ApplyFX4()
//        {
//        }


//        public override void DisplayFX1()
//        {
//            str1 = $"Cast from 4,5,6,7";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {
//            str2 = $"+40% dmg to Backrow";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
//        }

//        public override void DisplayFX3()
//        {
//            str3 = $"<style=Move>Pull</style> Self";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
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
//            SkillService.Instance.SkillHovered -= DisplayFX1;

//        }

//        public override void WipeFX2()
//        {
//            SkillService.Instance.SkillHovered -= DisplayFX2;
//        }

//        public override void WipeFX3()
//        {
//        }

//        public override void WipeFX4()
//        {
//        }
