using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class SplintersOfEarth : PerkBase
    {
        public override PerkNames perkName => PerkNames.SplintersOfEarth;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "splinters of earth";

        public override CharNames charName => CharNames.Baran;
        public override SkillNames skillName => SkillNames.EarthCracker;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName
                         ).WipeFX2;
        }
        public override void BaseApply()
        {
            base.BaseApply();
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO); 

            if(targetDyna != null )
            CombatService.Instance.colTargetDynas = 
                GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

           // Debug.Log("perk1 base" + desc);
        
        }

        public override void ApplyFX1()
        {
           // Debug.Log("perk1FX1" + desc);
        }

        public override void ApplyFX2()
        {
          //  Debug.Log("perk1FX2" + desc);
        }

        public override void ApplyFX3()
        {
          //  Debug.Log("perk1 FX3" + desc);
        }

        public override void ApplyMoveFX()
        {
          //  Debug.Log("perk MoveFX" + desc);
        }

        public override void ApplyVFx()
        {
           // Debug.Log("perk1 + VFX" + desc);
        }

        public override void DisplayFX1()
        {
            str0 = $"75%<style=Earth> Earth </style>to adj targets";
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

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.SplintersOfEarth;

//        public override PerkType perkType => PerkType.A1;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "SplintersOfEarth";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }
//        List<DynamicPosData> adjDynas = new List<DynamicPosData>();
//        float damageExtra = 50f; 
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                             .Find(t => t.skillName == skillName);

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
//            SkillService.Instance.SkillWipe += skillController.allPerkBases.Find(t => t.skillName == skillName
//            && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).WipeFX2;
//        }
//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);
//            skillController.allSkillBases.Find(t => t.skillName == skillName 
//                                    && t.skillLvl == SkillLvl.Level0).RemoveFX2(); // earth dmg
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            // get first target on the same lane
//            DynamicPosData targetDyna = GridService.Instance.gridView
//              .GetDynaFromPos(skillModel.targetPos[0].pos, skillModel.targetPos[0].charMode);
//            adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);
//        }          

//        public override void BaseApply()
//        {

//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            foreach (DynamicPosData dyna in adjDynas)
//            {
//                CharController target = dyna.charGO.GetComponent<CharController>();
//                target.dmgController.ApplyDamage(charController, DamageType.MagicalEarth,damageExtra , false);

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

//        public override void SkillEnd()
//        {

//        }
//        public override void Tick()
//        {

//        }


//        public override void DisplayFX1()
//        {
//            str1 = $"{damageExtra}%<style=Earth> Earth </style>dmg to Adj";
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
//            SkillService.Instance.SkillApply -= ApplyFX1;

//        }

//        public override void RemoveFX2()
//        {
//            SkillService.Instance.SkillApply -= ApplyFX2;

//        }

//        public override void RemoveFX3()
//        {

//        }

//        public override void RemoveFX4()
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
