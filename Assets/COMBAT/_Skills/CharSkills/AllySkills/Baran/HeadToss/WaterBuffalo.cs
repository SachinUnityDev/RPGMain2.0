using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Combat
{
    public class WaterBuffalo : PerkBase
    {
        public override PerkNames perkName => PerkNames.WaterBuffalo;
        public override PerkType perkType => PerkType.B3;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.CrackTheNose };

        public override string desc => "(PR: B1 + B2) /n + 40 Self Earth Res and Water Res, 2 rds /n  Clear any DoT on self";

        public override CharNames charName => CharNames.Baran;
        public override SkillNames skillName => SkillNames.HeadToss;
        public override SkillLvl skillLvl => SkillLvl.Level3;

        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        
        public override void ApplyFX1()
        {
            if (targetController)
            {
                charController.buffController.ApplyBuff(CauseType.CharSkill,(int)skillName, charID, StatsName.waterRes, +40f
                    , TimeFrame.EndOfRound, skillModel.castTime, true);
                charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, +40f
                   , TimeFrame.EndOfRound, skillModel.castTime, true);

            }
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
             //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.waterRes, -40f, false);
             //charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.earthRes, -40f, false);

        }
        public override void ApplyFX2()
        {
            CharStatesService.Instance.ClearDOT(charGO, CharStateName.BleedLowDOT);
            CharStatesService.Instance.ClearDOT(charGO, CharStateName.PoisonedLowDOT);
            CharStatesService.Instance.ClearDOT(charGO, CharStateName.BurnLowDOT);


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
            str0 = $"<style=Performer> +40<style=Water> Water Res </style>and<style=Earth> Earth Res </style>, 2 rds";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"<style=Performer> Clear DoT";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
     
        }

        public override void DisplayFX4()
        {
        }

       
    }
}


//}

//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.HeadToss;

//        public override SkillLvl skillLvl => SkillLvl.Level3;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.WaterBuffalo;

//        public override PerkType perkType => PerkType.B3;

//        public override List<PerkNames> preReqList => new List<PerkNames>()
//                                { PerkNames.PiercingHorn};

//        public override string desc => "water buffalo";

//        private float _chance = 60f;
//        public override float chance { get => _chance; set => _chance = value; }
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                                  .Find(t => t.skillName == skillName);
//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;   
//        }



//        public override void SkillSelected()
//        {
//            SkillController skillController = SkillService.Instance.currSkillMgr;
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//        }
//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

//            charController.ChangeStat(StatsName.waterRes, +40f, 0, 0);
//            charController.ChangeStat(StatsName.earthRes, +40f, 0, 0);


//        }

//        public override void ApplyFX2()
//        {
//        }

//        public override void ApplyFX3()
//        {
//        }

//        public override void ApplyFX4()
//        {
//        }



//        public override void DisplayFX1()
//        {
//            str1 = $"<style=Ally> + 40 <style=Water>Water</style>, 2 rds";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {
//            str1 = $"<style=Ally> + 40 <style=Earth>Earth</style>, 2 rds";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX3()
//        {
//        }

//        public override void DisplayFX4()
//        {
//        }

//        public override void PostApplyFX()
//        {
//        }

//        public override void PreApplyFX()
//        {
//        }

//        public override void RemoveFX1()
//        {
//        }

//        public override void RemoveFX2()
//        {
//        }

//        public override void RemoveFX3()
//        {
//        }

//        public override void RemoveFX4()
//        {
//        }

//        public override void SkillEnd()
//        {
//            CombatEventService.Instance.OnEOR -= Tick; roundEnd = 0;        

//            charController.ChangeStat(StatsName.waterRes, -40f, 0, 0);
//            charController.ChangeStat(StatsName.earthRes, -40f, 0, 0);            
//        }



//        public override void Tick()
//        {
//            if (roundEnd >= skillModel.castTime)
//                SkillEnd();
//            roundEnd++;
//        }

//        public override void WipeFX1()
//        {
//        }

//        public override void WipeFX2()
//        {
//        }

//        public override void WipeFX3()
//        {
//        }

//        public override void WipeFX4()
//        {
//        }

