using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class FasterThanEyeCanSee : PerkBase
    {
        public override PerkNames perkName => PerkNames.FasterThanEyeCanSee;

        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "%30 Confuse, 1 rd";

        public override CharNames charName => CharNames.Cahyo;

        public override SkillNames skillName => SkillNames.WristSpin;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 30f;
        public override float chance { get => _chance; set => _chance = value; }
    
        public override void ApplyFX1()
        {
             foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
             {
                    if(chance.GetChance())
                    dyna.charGO.GetComponent<CharController>().charStateController
                        .ApplyCharStateBuff(CauseType.CharSkill, (int)skillName, charController.charModel.charID
                                             , CharStateName.Blinded, skillModel.timeFrame, skillModel.castTime);
             }
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
            str0 = $"30 % chance, <style=States>Confused</style>, {skillModel.castTime} rd ";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
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