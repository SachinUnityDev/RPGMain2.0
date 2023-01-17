using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class NoMatterWhoYouAre : PerkBase
    {
        public override CharNames charName => CharNames.Cahyo;
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level1;
        public override PerkSelectState state { get; set; }
        public override PerkNames perkName => PerkNames.NoMatterWhoYouAre;
        public override PerkType perkType => PerkType.B1;
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };
        public override string desc => "No matter who you are !";        
        public override float chance { get; set; }
        float dmgMinDecr, dmgMaxDecr = 0;
        //public override void SkillInit()
        //{
        //    skillModel = SkillService.Instance.allSkillModels
        //                                    .Find(t => t.skillName == skillName);

        //    charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    skillController = SkillService.Instance.currSkillMgr;
        //    charGO = SkillService.Instance.GetGO4Skill(charName);
        //}
        //public override void SkillHovered()
        //{
        //    SkillInit();
        //    SkillServiceView.Instance.skillCardData.skillModel = skillModel;
        //    SkillService.Instance.SkillHovered += DisplayFX1;
        //}
        //public override void SkillSelected()
        //{
        //    DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

        //    if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
        //    {
        //        skillModel.isActive = false;
        //        return;
        //    }

        //    List<DynamicPosData> allEnemies = GridService.Instance.GetAllOccupiedbyCharMode(CharMode.Enemy);
        //    if (allEnemies.Count == 1)
        //    {
        //        targetController = allEnemies[0].charGO.GetComponent<CharController>();
        //    }
        //    SkillService.Instance.SkillApply += BaseApply;
        //    SkillService.Instance.SkillApply += ApplyFX1;
        //}
        //public override void BaseApply()
        //{
        //    targetGO = SkillService.Instance.currentTargetDyna.charGO;
        //    if (targetGO != null)
        //        targetController = targetGO.GetComponent<CharController>();
        //    CombatEventService.Instance.OnEOR += Tick;
        //}

        public override void DisplayFX1()
        {
            str1 = $"<style=Enemy><style=States> Despair</style> solo enemy";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
            //CharStatesService.Instance.SetCharState(targetController.gameObject, CharStateName.Despaired);
        }

        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;

            //StatData moraleSD = charController.GetStat(StatsName.morale);
            //if (moraleSD.baseValue == 12)
            //{
            //    StatData dmgSD = targetController.GetStat(StatsName.damage);
            //    dmgMinDecr = dmgSD.minLimit * 0.3f;
            //    dmgMaxDecr = dmgSD.maxLimit * 0.3f;
            //    targetController.ChangeStat(StatsName.damage, 0, -dmgMinDecr, -dmgMaxDecr);
            //}
        }
        public override void SkillEnd()
        {
            //CombatEventService.Instance.OnEOR -= Tick;
            //charController.ChangeStat(StatsName.damage, 0, dmgMinDecr, dmgMaxDecr);
            //roundEnd = 0;
        }


        public override void Tick()
        {
            //if (roundEnd >= skillModel.castTime)
            //    SkillEnd();
            //roundEnd++;
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

