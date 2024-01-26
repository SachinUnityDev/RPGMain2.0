using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Combat
{
    public class Tsunami : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.Tsunami;
        public override SkillLvl skillLvl => SkillLvl.Level0;
        public override string desc => " tidal waves base";

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> firstRowChar = new List<DynamicPosData>();

        public override void PopulateTargetPos()
        {
            AnyWithCharMode(CharMode.Enemy);
        }
        public override void ApplyFX1()  // DAMAGE 
        {
            foreach (DynamicPosData targetDyna in CombatService.Instance.mainTargetDynas)
            {
                targetDyna.charGO.GetComponent<CharController>()
                    .damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                            , DamageType.Water, skillModel.damageMod, skillModel.skillInclination);
            }
        }



        public override void ApplyMoveFx()
        {
            GridService.Instance.ShuffleCharMode(CharMode.Enemy);
        }
        public override void ApplyFX2()
        {

        }

        public override void ApplyFX3()
        {
            foreach (DynamicPosData dyna in CombatService.Instance.mainTargetDynas)
            {
                dyna.charGO.GetComponent<CharController>().charStateController.ApplyCharStateBuff(CauseType.CharSkill, (int)skillName
                        , charController.charModel.charID, CharStateName.Soaked, skillModel.timeFrame, skillModel.castTime);
            }

        }

        public override void DisplayFX1()
        {
            str0 = $"{skillModel.damageMod}%<style=Water> Water </style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str0);
        }
        public override void DisplayFX2()
        {
            str1 = "Apply <style=States>Soaked</style> and<style=Move> Shuffle</style>";
            SkillService.Instance.skillModelHovered.AddDescLines(str1);
        }
        public override void DisplayFX3()
        {

        }
        public override void DisplayFX4()
        {
        }

        public override void ApplyVFx()
        {
            SkillService.Instance.skillFXMoveController.RangedStrike(PerkType.None, skillModel);
        }

        public override void PopulateAITarget()
        {
        }

    }
}