using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class QuickToReact : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.HoneBlades;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.QuickToReact;
        public override PerkType perkType => PerkType.A1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Quick To react";    
        public override float chance { get; set; }
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                       .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);
        //}

        //public override void SkillHovered()
        //{
        //    SkillInit();

        //    SkillService.Instance.skillCardData.skillModel = skillModel;

        //    SkillService.Instance.SkillHovered += DisplayFX1;

        //}



        //public override void SkillSelected()
        //{
        //    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

        //    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
        //        return;

        //    SkillService.Instance.SkillApply += BaseApply;

        //}
        public override void BaseApply()
        {
            skillModel.cd = 2;
        }

        public override void DisplayFX1()
        {
            str1 = $"<style=Allies> cd now {skillModel.cd} rd";
            SkillService.Instance.skillCardData.descLines.Add(str1);
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
        public override void SkillEnd()
        {
        }



        public override void Tick()
        {
        }

        public override void WipeFX1()
        {
            SkillService.Instance.SkillHovered -= DisplayFX1;

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

