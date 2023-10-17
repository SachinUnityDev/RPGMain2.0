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
            CombatService.Instance.mainTargetDynas.Clear(); 
            if (targetDyna != null)
            {
                skillModel.targetPos.Clear();
                skillModel.targetPos.Add(new CellPosData(CharMode.Enemy, targetDyna.currentPos));
                CombatService.Instance.mainTargetDynas.Add(targetDyna); 
            }
        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.castPos.Clear();
            skillModel.castPos.AddRange(new List<int>() { 4, 5, 6, 7 });

          //  SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX1;
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
            
        }

        public override void DisplayFX2()
        {
            str2 = $"+40%<style=Physical> Physical </style>on targets back row";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);

        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Move> Move </style>forward 2";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }

        public override void DisplayFX4()
        {
         
        }
    }
}

