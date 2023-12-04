using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class ThrowAndPick : PerkBase
    {
        public override PerkNames perkName => PerkNames.ThrowAndPick;

        public override PerkType perkType => PerkType.B2;

        public override PerkSelectState state { get; set; }

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.EdgyHatchet };

        public override string desc => "this is Throw and Pick";

        public override CharNames charName => CharNames.Abbas;

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        public override float chance { get; set; }
        DynamicPosData targetDyna;
        List<DynamicPosData> sameLaneTargets = new List<DynamicPosData>();
        public override void AddTargetPos()
        {
            if (skillModel != null)
            {
                skillModel.targetPos.Clear();
                CombatService.Instance.mainTargetDynas.Clear();
                
                CellPosData cellPos = new CellPosData(CharMode.Ally, currDyna.currentPos);                
            
                sameLaneTargets = GridService.Instance.GetInSameLaneOppParty(cellPos);

                if (sameLaneTargets.Count > 0)
                    targetDyna = sameLaneTargets[0];
                else
                    targetDyna = null;
                if(targetDyna!= null)
                {
                    skillModel.targetPos.Add(cellPos);
                    CombatService.Instance.mainTargetDynas.Add(targetDyna);
                }
            }
        }
        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.attackType = AttackType.Ranged;
            skillModel.damageMod = 145;
            skillModel.cd = 2; 
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

        public override void DisplayFX1()
        {
            str1 = "Target -> First on same lane";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
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
        public override void InvPerkDesc()
        {
            perkDesc = "90% -> 145% <style=Bleed>Physical</style> Dmg";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Attack type -> Ranged";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Target -> First on same lane";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Cd: 0 -> 2";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }




}
