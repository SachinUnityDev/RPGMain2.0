using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class EnemyOfFire : PerkBase
    {
        public override PerkNames perkName => PerkNames.EnemyOfFire;
        public override PerkType perkType => PerkType.B2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.FringesOfIce };

        public override string desc => "(PR : B1) /n + 30 Fire Res Enemies who cast Fire Skills on ally /n, receive 6-12 Water dmg";

        public override CharNames charName => CharNames.Rayyan;

        public override SkillNames skillName => SkillNames.ColdTouch;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
     
        public override void BaseApply()
        {
            base.BaseApply();
            CombatEventService.Instance.OnDmgDelivered += Retaliation4FireDmg; 
        }
        public override void ApplyFX1()
        {
            if(targetController != null  && IsTargetAlly())
            {
                targetController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
                    , StatsName.fireRes, 20f, TimeFrame.EndOfRound, skillModel.castTime, true); 
            }
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
            //targetController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.fireRes
            //                , -20f);
            CombatEventService.Instance.OnDmgDelivered -= Retaliation4FireDmg;
        }

        public void Retaliation4FireDmg(DmgData dmgData)
        {
            if(dmgData.dmgRecievedType == DamageType.Fire)
            {
                if (dmgData.targetController == targetController)
                {                  
                        dmgData.striker.damageController.ApplyDamage(charController, CauseType.CharSkill, (int)skillName
                                                                                            , DamageType.Water, 60f, false);                    
                }
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
            str1 = $"+20<style=Fire> Fire Res </style>, {skillModel.castTime} rds";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"Attackers with Fire Skills recieve +60<style=Water> Water </style>";
            SkillService.Instance.skillCardData.descLines.Add(str2);
        }

        public override void DisplayFX3()
        {
        }

        public override void DisplayFX4()
        {
        }

    }
}

