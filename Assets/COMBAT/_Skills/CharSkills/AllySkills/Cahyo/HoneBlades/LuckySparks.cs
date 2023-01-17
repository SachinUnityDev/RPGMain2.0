﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class LuckySparks : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level3;      
        public override PerkSelectState state { get;set; }
        public override PerkNames perkName => PerkNames.LuckySparks;
        public override PerkType perkType => PerkType.B3;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Lucky Sparks";        
        public override float chance { get; set;  }
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                      .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);
        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();

        //    SkillServiceView.Instance.skillCardData.skillModel = skillModel;
        //    Debug.Log("skill hovered" + perkName);
        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //}



        //public override void SkillSelected()
        //{
        //    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

        //    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
        //        return;

        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //}
        //public override void BaseApply()
        //{
        //    targetGO = charGO;
        //    targetController = charController;
        //    CombatEventService.Instance.OnEOR += Tick;
        //}

        public override void DisplayFX1()
        {
            str1 = $"<style=Allies> Luck +4, 2 rd";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void ApplyFX1()
        {
           // targetController.ChangeStat(StatsName.luck, +2, 0, 0);
        }

        public override void SkillEnd()
        {
            //CombatEventService.Instance.OnEOR -= Tick;
            //targetController.ChangeStat(StatsName.luck, +2, 0, 0);
            //roundEnd = 0;

        }



        public override void Tick()
        {
            //if (roundEnd >= skillModel.castTime)
            //    SkillEnd();
            //roundEnd++;
        }

        public override void ApplyFX2()
        {
        }

        public override void ApplyFX3()
        {
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
        public override void RemoveFX1()
        {
        }

        public override void RemoveFX2()
        {
        }

        public override void RemoveFX3()
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

