﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace Combat
{
    public class AimTheWindPipe : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.KrisLunge;

        public override SkillLvl skillLvl => SkillLvl.Level3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkNames perkName => PerkNames.AimTheWindpipe;

        public override PerkType perkType => PerkType.A3;

        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "Aim the wind pipe";
        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        public override PerkSelectState state { get; set; }

        public override void BaseApply()
        {
            targetGO = SkillService.Instance.currentTargetDyna.charGO;
            targetController = targetGO.GetComponent<CharController>();
            CombatEventService.Instance.OnEOR += Tick;
            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
            skillModel.cd = 3;
        }
        public override void DisplayFX1()
        {
            str1 = $"cd increased 3 rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy>half<style=Stamina> Stamina</style>";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }
        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //float stamina = targetController.GetStat(StatsName.stamina).baseValue;
            //targetController.SetStat(StatsName.stamina, stamina / 2, true);

        }

        public override void DisplayFX3()
        {
            str3 = $"<style=Enemy><style=Fortitude> fortitude</style>, +6-8";
            SkillService.Instance.skillModelHovered.descLines.Add(str3);
        }
        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //targetController.ChangeStat(StatsName.fortitude, (float)Random.Range(6, 9), 0, 0);
        }

        public override void ApplyFX3()
        {

        }
        public override void Tick()
        {

        }
        public override void RemoveFX1()
        {

        }

        public override void RemoveFX2()
        {

        }

        public override void RemoveFX3()
        {

        }


        public override void SkillEnd()
        {

        }



        public override void DisplayFX4()
        {
        }

        public override void WipeFX1()
        {
        }

        public override void WipeFX2()
        {
        }

        public override void WipeFX3()
        {
        }

        public override void WipeFX4()
        {
        }

        public override void ApplyVFx()
        {
         
        }

        public override void ApplyMoveFX()
        {
          
        }
    }

}

