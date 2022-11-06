using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class EarthCracker : SkillBase
    {
        public override SkillModel skillModel { get ; set ; }

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.EarthCracker;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Single;
        public override string desc => "Earth Cracker..";

        private float _chance = 60f;
        public override float chance { get => _chance; set => _chance = value; }
       
        float damageExtra = 75f;

        List<DynamicPosData> sameLaneTargets = new List<DynamicPosData>();
        public override void PopulateTargetPos()
        {
            if (skillModel == null) return; 
            skillModel.targetPos.Clear();sameLaneTargets.Clear(); 

            sameLaneTargets = GridService.Instance.GetInSameLaneOppParty
                       (new CellPosData(CharMode.Ally, GridService.Instance.GetDyna4GO(charGO).currentPos));
            for (int i = 1; i < sameLaneTargets.Count; i++)
            {
                CombatService.Instance.colTargetDynas.Add(sameLaneTargets[i]);
            }
            
            if(sameLaneTargets.Count > 0)
            {
                CombatService.Instance.mainTargetDynas.Add(sameLaneTargets[0]);                     
                skillModel.targetPos.Add(new CellPosData(sameLaneTargets[0].charMode, sameLaneTargets[0].currentPos));
            }
        }

        //public override void BaseApply()
        //{
        //    base.BaseApply(); 
        //    targetGO = sameLaneTargets[0].charGO;
        //    targetController = targetGO.GetComponent<CharController>();

        //}

        public override void ApplyFX1()
        {
            targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Physical, skillModel.damageMod
                    , false);

          //  Debug.Log("Skill1" + desc);

        }

        public override void ApplyFX2()
        {           
            for (int i = 0; i < CombatService.Instance.colTargetDynas.Count; i++)
            {
                CharController target = CombatService.Instance.colTargetDynas[i].charGO.GetComponent<CharController>();
                target.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Earth, damageExtra, false);
            }
           // Debug.Log("Skill2" + desc);
        }

        public override void ApplyFX3()
        {         
            if (_chance.GetChance())
            {
                CharStatesService.Instance.ApplyCharState(targetGO, CharStateName.BleedLowDOT
                                     , charController, CauseType.CharSkill, (int)skillName);
            }

            //Debug.Log("Skill3" + desc);
        }

        public override void DisplayFX1()
        {
            str1 = $"{skillModel.damageMod}%<style=Physical> Physical</style>";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"{damageExtra}%<style=Earth> Earth </style>to targets behind on same lane";
            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            str3 = $"{chance}%<style=Bleed> Low Bleed </style>";
            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
        }
        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.SingleTargetRangeStrike(PerkType.None);

          //  Debug.Log("Skill VFX" + desc);
        }
        public override void PopulateAITarget()
        {
        }

        public override void ApplyMoveFx()
        {
           // Debug.Log("Move FX" + desc);
        }
    }

}

//public override void SkillSelected()
//{
//    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

//    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//        return;






//    GridService.Instance.HLTargetTiles(skillModel.targetPos);

//}