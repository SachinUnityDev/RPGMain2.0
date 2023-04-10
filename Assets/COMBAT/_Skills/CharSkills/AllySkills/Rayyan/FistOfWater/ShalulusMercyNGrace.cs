using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class ShalulusMercyNGrace : PerkBase
    {
        public override PerkNames perkName => PerkNames.ShalulusMercyAndGrace;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "180% --> 140% Water /n Heal ally party for 5-10 /n Clears Burning and Soaked on ally party /n Once per combat --> 4 rds cd /n (If Morale 12, heals for 10-20)";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.FistOfWater;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public List<CharController> allCharControllers = new List<CharController>();
        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            for (int i = 1; i < 8; i++)
            {            
                    CellPosData cellPosData = new CellPosData(CharMode.Ally, i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        skillModel.targetPos.Add(cellPosData);
                        allCharControllers.Add(dyna.charGO.GetComponent<CharController>());
                    }                
            }
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            skillModel.damageMod = 140f; 
        }

        public override void ApplyFX1()
        {
            AttribData statData = charController.GetAttrib(AttribName.morale);
            if(statData.currValue < 12)
            {
                allCharControllers.ForEach(t => t.damageController
                 .ApplyDamage(charController, CauseType.CharSkill, (int)SkillNames.FistOfWater, DamageType.Heal
                                                                    , UnityEngine.Random.Range(6, 12)));
            }else
            {
                allCharControllers.ForEach(t => t.damageController
                        .ApplyDamage(charController, CauseType.CharSkill, (int)SkillNames.FistOfWater, DamageType.Heal
                                        , UnityEngine.Random.Range(12, 24)));
            }       
        }
        public override void ApplyFX2()
        {
            allCharControllers.ForEach(t => charController.charStateController.ClearDOT(CharStateName.BurnLowDOT)); 
        }

        public override void ApplyFX3()
        {
            allCharControllers.ForEach(t => t.charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                       , charController.charModel.charID, CharStateName.Soaked));                            
        }

        public override void ApplyMoveFX()
        {
        }

        public override void ApplyVFx()
        {
        }

        public override void DisplayFX1()
        {
            str0 = $"<style=Heal>Heal</style> 6-12";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);

            str1 = $"If <style=Attribute>Morale</style> 12, <style=Heal>Heal</style> 12-24";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            // add clear 
            // add soaked 
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

        public override void PostApplyFX()
        {
        }

        public override void PreApplyFX()
        {
        }
    }
}

