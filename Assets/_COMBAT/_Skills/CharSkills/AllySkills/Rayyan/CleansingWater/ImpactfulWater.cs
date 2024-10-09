using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq; 

namespace Combat
{
    public class ImpactfulWater : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override string desc => " Adj targets hit for %80 Water /n" +
                                        "/n Gain +3 Fortitude for every target hit ";
        public override PerkNames perkName => PerkNames.ImpactfulWater;
        public override List<PerkNames> preReqList => new List<PerkNames>() 
                                         { PerkNames.PressurizedWater, PerkNames.MurkyWaters };
        public override PerkType perkType => PerkType.A3;
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        int targetHit = 0;


        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.staminaReq = 7;
        }

        public override void ApplyFX1()
        {    
            targetHit= 0;
            if (IsTargetEnemy())
            {
                DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);

                List<DynamicPosData> allColTargets = GridService.Instance.gridController
                                                                .GetAllAdjDynaOccupied(targetDyna);

                allColTargets.ForEach(t => t.charGO.GetComponent<CharController>()
                        .damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                        , DamageType.Water, 80f, skillModel.skillInclination)); // skillInclination is magical

                Debug.Log("INCLI><><><"+ skillModel.skillInclination);

                targetHit = allColTargets.Count +1;
            }
        }
        public override void ApplyFX2()
        {
            //Debug.Log("Target Hit" + targetHit);
            for (int i = 0; i < targetHit; i++)
            {
                charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.fortitude, +3f);
            }
        }

        public override void ApplyFX3()
        {
          
        }

        public override void ApplyVFx()
        {

        }
        public override void ApplyMoveFX()
        {

        }
        public override void DisplayFX1()
        {
            str1 = "Hit adj targets 80% <style=Water>Water</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX2()
        {
            str2 = "Gain 3 <style=Fortitude>Fortitude</style> for every target hit";
            SkillService.Instance.skillModelHovered.AddDescLines(str2);
        }
        public override void DisplayFX3()
        {
          
        }
        public override void DisplayFX4()
        {
            
        }
        public override void InvPerkDesc()
        {
            perkDesc = "Hit adj targets 80% <style=Water>Water</style>";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Gain 3 <style=Fortitude>Fortitude</style> for every target hit";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Stm cost: 5 -> 7";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }



}

