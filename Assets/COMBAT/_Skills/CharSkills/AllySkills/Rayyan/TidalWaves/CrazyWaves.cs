using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class CrazyWaves : PerkBase
    {
        public override PerkNames perkName => PerkNames.CrazyWaves;
        public override PerkType perkType => PerkType.B1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "this is crazy waves... ";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.TidalWaves;

        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        bool isFront;
        bool dmgInc = false; 
        public override void SkillHovered()
        {
            base.SkillHovered();

            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX2;
        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;
            Debug.Log("skillName FX REMOVE >>>>" + skillName);
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveMoveFX;
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveVFX;
        }

        public override void AddTargetPos()
        {
            if (skillModel == null) return;
            skillModel.targetPos.Clear();
            CombatService.Instance.mainTargetDynas.Clear(); 
            CombatService.Instance.mainTargetDynas.AddRange(GridService.Instance.GetFirstDiamond(CharMode.Enemy));

            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                CellPosData cell = new CellPosData(dyna.charMode, dyna.currentPos);
                skillModel.targetPos.Add(cell);

            }
            Debug.Log("Crazy waves ADD TARGETS");
        }

        public override void BaseApply()
        {
            base.BaseApply();
            if (!dmgInc)
            {
                skillModel.damageMod += 30f;
                dmgInc= true;
            }
            
        }


        public override void ApplyFX1()
        {
            if (currDyna != null)
            {
                CombatService.Instance.mainTargetDynas.ForEach(t => t.charGO.GetComponent<CharController>().damageController.ApplyDamage(charController
                                            , CauseType.CharSkill, (int)skillName
                                            , DamageType.Water, skillModel.damageMod, skillModel.skillInclination));
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
            SkillService.Instance.skillFXMoveController.MultiTargetRangeFX(PerkType.None);
        }
        public override void ApplyMoveFX()
        {
            Debug.Log("Move Crazy waves");
            GridService.Instance.ShuffleCharSet(CombatService.Instance.mainTargetDynas);
        }

        public override void DisplayFX1()
        {
            str0 = $"{skillModel.damageMod}% <style=Water> Water </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Move> Shuffle </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }

        public override void DisplayFX3()
        {
            
        }

        public override void DisplayFX4()
        {
            
        }

   

    
    }
}
