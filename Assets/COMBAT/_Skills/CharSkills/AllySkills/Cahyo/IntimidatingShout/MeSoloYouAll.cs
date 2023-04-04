using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class MeSoloYouAll : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get ;set ; }
        public override PerkNames perkName => PerkNames.MeSoloYouAll;
        public override PerkType perkType => PerkType.A1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "Me solo you all ";        
        public override float chance { get; set; }

        AttribData initStatData;
        float dmgMinIncr, dmgMaxIncr, armourMinIncr, armourMaxIncr;

        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                      .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);
        //    initStatData = charController.GetStat(StatsName.initiative);
        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();
        //    SkillServiceView.Instance.skillCardData.skillModel = skillModel;
        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //    // if he is only one remaining..... 

        //}

        //public override void SkillSelected()
        //{
        //    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

        //    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
        //    {
        //        skillModel.isActive = false;
        //        return;
        //    }

        //    List<DynamicPosData> allAllies = GridService.Instance.GetAllOccupiedbyCharMode(CharMode.Ally);
        //    if (allAllies.Count != 1)
        //    {
        //        skillModel.isActive = false;
        //        return;
        //    }
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //}
        //public override void BaseApply()
        //{
        //    targetGO = SkillService.Instance.currentTargetDyna.charGO;
        //    targetController = targetGO.GetComponent<CharController>();
        //    CombatEventService.Instance.OnEOR += Tick;
        //}

        public override void DisplayFX1()
        {
            str1 = $"<style=Performer>if solo, +20% Armor&Dmg";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);

            str2 = $"<style=Performer>(if Init 12, +40% Armor&Dmg)";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }
        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

            //StatData dmgSD = charController.GetStat(StatsName.damage);
            //StatData armorSD = charController.GetStat(StatsName.armor);


            //if (initStatData.baseValue >= 12)
            //{
            //    dmgMinIncr = dmgSD.minLimit * 0.4f; dmgMaxIncr = dmgSD.maxLimit * 0.4f;
            //    armourMinIncr = armorSD.minLimit * 0.4f; armourMaxIncr = armorSD.maxLimit * 0.4f;

            //}
            //else
            //{
            //    dmgMinIncr = dmgSD.minLimit * 0.2f; dmgMaxIncr = dmgSD.maxLimit * 0.2f;
            //    armourMinIncr = armorSD.minLimit * 0.2f; armourMaxIncr = armorSD.maxLimit * 0.2f;

            //}

            //charController.ChangeStat(StatsName.damage, 0, dmgMinIncr, dmgMaxIncr);
            //charController.ChangeStat(StatsName.armor, 0, armourMinIncr, armourMaxIncr);
        }

        public override void ApplyFX2()
        {

        }

        public override void Tick()
        {
            //if (roundEnd >= skillModel.castTime)
            //    SkillEnd();
            //roundEnd++;
        }

        public override void SkillEnd()
        {
            //CombatEventService.Instance.OnEOR -= Tick;
            //charController.ChangeStat(StatsName.damage, 0, -dmgMinIncr, -dmgMaxIncr);
            //charController.ChangeStat(StatsName.armor, 0, -armourMinIncr, -armourMaxIncr);

            //roundEnd = 0;
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

        public override void PostApplyFX()
        {

        }

        public override void PreApplyFX()
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

