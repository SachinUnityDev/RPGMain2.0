using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class FeelOfImpact : PerkBase
    {
        public override PerkNames perkName => PerkNames.FeelOfImpact;
        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.CrazyWaves };

        public override string desc => "this is feel the impact";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.TidalWaves;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }

      //  public override List<DynamicPosData> targetDynas => new List<DynamicPosData>();

  
        public override void ApplyFX1()
        {
            if (currDyna != null)
            {
                if (currDyna.currentPos == 4)
                {
                    targetController.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                                    ,DamageType.Water, 50f, false); 
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

        public override void DisplayFX1()
        {
            str1 = $"+50%<style=Water> Water </style>on pos 4";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
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

        public override void ApplyMoveFX()
        {
        }
    }


}

