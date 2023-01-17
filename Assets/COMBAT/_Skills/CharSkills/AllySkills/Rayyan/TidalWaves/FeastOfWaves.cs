using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class FeastOfWaves : PerkBase
    {
        public override CharNames charName => CharNames.Rayyan;
        public override PerkNames perkName => PerkNames.FeastOfWaves;
        public override PerkType perkType => PerkType.A3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SweepingWaves, PerkNames.DontMindTheWaves };

        public override string desc => "Feast of Waves";   
        public override SkillNames skillName => SkillNames.TidalWaves;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        //public override void AddTargetPos()
        //{
        //    if (skillModel != null )
        //    Debug.Log("Inside the feast of waves " + skillModel.targetPos.Count); 
        //}

        public override void ApplyFX1()
        {
            foreach (CellPosData cell in skillModel.targetPos)
            {
                if(cell.charMode == CharMode.Ally)
                {
                    DynamicPosData dyna = GridService.Instance.gridView.GetDynaFromPos(cell.pos, cell.charMode);
                    float healVal = Random.Range(4f, 10f);

                    dyna.charGO.GetComponent<CharController>().damageController.ApplyDamage(charController,
                                                                        CauseType.CharSkill, (int)skillName, DamageType.Heal, healVal, false);

                    CharStatesService.Instance.ClearDOT(dyna.charGO, CharStateName.BurnHighDOT); 
                }

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
            str1 = $"<style=Allies><style=Heal> Heal </style> 4-10";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Allies><style=Burn> Clear Burn </style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
            
        }

        public override void DisplayFX4()
        {
           
        }


 
    }


}

