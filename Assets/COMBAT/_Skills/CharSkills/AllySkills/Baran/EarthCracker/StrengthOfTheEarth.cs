using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class StrengthOfTheEarth : PerkBase
    {
        public override PerkNames perkName => PerkNames.StrengthOfTheEarth; 
        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.GravityForce, PerkNames.SplintersOfEarth };

        public override string desc => "(PR: A1 + A2) /n Ignores armor on initial target /n Collateral targets receive -25 ER, 4 rds";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> colTargetDyna = new List<DynamicPosData>();

        public override void BaseApply()
        {
            base.BaseApply();
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);
            colTargetDyna.Clear();
            if (targetDyna != null)
            {
                CombatService.Instance.colTargetDynas =
                   GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

                colTargetDyna.AddRange(CombatService.Instance.colTargetDynas);
            }
        }
        public override void SkillHovered()
        {
            base.SkillHovered();

            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;

        }

        public override void ApplyFX1()
        {
            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                        , DamageType.Physical, skillModel.damageMod, true);
        }

        public override void ApplyFX2()
        {
            CombatService.Instance.colTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
            .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, -40
            ,TimeFrame.EndOfRound, skillModel.castTime, false ));

        }
        public override void SkillEnd()
        {
            base.SkillEnd();
           // CombatService.Instance.colTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>()
           //.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.earthRes, -40));


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

        public override void DisplayFX1()
        {
            str0 = $"Ignore<style=Attributes> Armor </style>on main target";
            SkillServiceView.Instance.skillCardData.descLines.Add(str0);          
        }

        public override void DisplayFX2()
        {
            str1 = $"-40<style=Earth> Earth res </style>on col targets";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

  
    }
}

//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.EarthCracker;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.StrengthOfTheEarth;

//        public override PerkType perkType => PerkType.A3;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SplintersOfEarth, PerkNames.GravityForce };

//        public override string desc => "Strength of earth";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        List<DynamicPosData> adjDynas = new List<DynamicPosData>();
//        DynamicPosData targetDyna; 
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                            .Find(t => t.skillName == skillName);

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
//            targetDyna = GridService.Instance.gridView              // dyna from target pos 
//                    .GetDynaFromPos(skillModel.targetPos[0].pos, skillModel.targetPos[0].charMode);
//            adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);
//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            foreach (DynamicPosData dyna in adjDynas)
//            {
//                CharController target = dyna.charGO.GetComponent<CharController>();
//                target.ChangeStat(StatsName.earthRes, -25, 0, 0); 
//            }

//        }
//        public override void Tick()
//        {
//            if (roundEnd >= 4)
//                SkillEnd();
//            roundEnd++;

//        }
//        public override void SkillEnd()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            foreach (DynamicPosData dyna in adjDynas)
//            {
//                CharController target = dyna.charGO.GetComponent<CharController>();
//                target.ChangeStat(StatsName.earthRes, 25, 0, 0);
//            }
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
//            str1 = $"-25<style=Earth> ER</style>, 4 rds";
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