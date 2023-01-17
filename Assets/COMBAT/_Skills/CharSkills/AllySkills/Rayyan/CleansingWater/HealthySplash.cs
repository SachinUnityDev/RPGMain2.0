using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Combat
{
    public class HealthySplash : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override SkillNames skillName => SkillNames.CleansingWater;
        public override SkillLvl skillLvl => SkillLvl.Level2;


        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override string desc => "This is healthy Splash";

        public override PerkNames perkName => PerkNames.HealthySplash;
        public override PerkType perkType => PerkType.B2;
        public SkillLvl perkLvl => SkillLvl.Level2;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.HealthyWater };
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        //  public override List<DynamicPosData> targetDynas => new List<DynamicPosData>();

      
        public override void ApplyFX1()
        {
            if (IsTargetAlly())
            {
                DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);

                List<DynamicPosData> allAdjOccupied = GridService.Instance.gridController
                                                                .GetAllAdjDynaOccupied(targetDyna);

                allAdjOccupied.ForEach(t => t.charGO.GetComponent<CharController>()
                        .damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName, DamageType.Heal, Random.Range(6f, 9f)));

            }
        }

        public override void ApplyFX2()
        {
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
            str1 = $"<style=Heal>Heal</style> adj targets  6-9 ";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
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

