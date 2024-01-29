using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class GravityForce : PerkBase
    {
        public override PerkNames perkName => PerkNames.GravityForce;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SplintersOfEarth };

        public override string desc => "(PR: A1)/n  Rooted on initial target/n On collateral targets, -1 luck, 2 rds /n 50% dmg on initial target if rooted";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 60f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> colDynaCopy = new List<DynamicPosData>(); 

        bool dmgInc =false;
        public override void BaseApply()
        {
            base.BaseApply();            
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);

            if (targetDyna.charGO.GetComponent<CharStateController>().HasCharState(CharStateName.Rooted))
            {
                if(!dmgInc)
                {
                    skillModel.damageMod += 50f;
                    dmgInc = true;
                }
            }
                

            colDynaCopy.Clear(); 
            if (targetDyna != null)
            {
                CombatService.Instance.colTargetDynas =
                   GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

                colDynaCopy.AddRange(CombatService.Instance.colTargetDynas);                   
            }               
        }
        public override void ApplyFX1()
        {
            if(dmgInc) 
                if (chance.GetChance())
                charController.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName,
                charController.charModel.charID, CharStateName.Rooted, skillModel.timeFrame, skillModel.castTime);

            dmgInc = false;
        }

        public override void ApplyFX2()
        {
            colDynaCopy.ForEach(t => t.charGO.GetComponent<CharController>()
              .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
              , AttribName.luck, -1, skillModel.timeFrame, skillModel.castTime, false));
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
            str0 = $"60% <style=States>Rooted</style> on initial target";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = $"+50% Dmg vs initial target if <style=States>Rooted</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {
            str2 = $"-1 Luck on adj targets";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX4()
        {
        }
        public override void InvPerkDesc()
        {
            perkDesc = "60% <style=States>Rooted</style> on initial target";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = $"+50% Dmg vs initial target if <style=States>Rooted</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);

            perkDesc = $"-1 Luck on adj targets";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }
}

