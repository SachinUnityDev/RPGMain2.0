using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

//Target all enemies for 90% dmg. No push but shuffle enemies.


namespace Combat
{
    public class SweepingWaves : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override PerkNames perkName => PerkNames.SweepingWaves;
        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => " sweeping waves";
        public override SkillNames skillName => SkillNames.TidalWaves;
        public override SkillLvl skillLvl => SkillLvl.Level1;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        public override void AddTargetPos()
        {
            if (skillModel != null)
            {
                skillModel.targetPos.Clear();
                CombatService.Instance.mainTargetDynas.Clear();
                for (int i = 1; i < 8; i++)
                {
                    CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cellPosData.pos, cellPosData.charMode);
                    if (dyna != null)
                    {
                        CombatService.Instance.mainTargetDynas.Add(dyna);
                        skillModel.targetPos.Add(cellPosData);
                    }
                }
            }        
        }

        public override void SkillHovered()
        {
            base.SkillHovered();
            skillModel.damageMod = 90f;            
        }

     
        public override void BaseApply()
        {
            base.BaseApply();
            skillModel.damageMod = 90f;          
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

        public override void ApplyVFx()
        {
            
        }
        public override void ApplyMoveFX()
        {
           
        }
        public override void DisplayFX1()
        {
            str1 = "Target -> All enemies";
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
            perkDesc = "<style=Water>Water</style> Dmg: 120% -> 90%";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
            perkDesc = "Target -> All enemies";
            SkillService.Instance.skillModelHovered.AddPerkDescLines(perkDesc);
        }

    }

}

