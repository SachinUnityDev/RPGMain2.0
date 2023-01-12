using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class FuryOfPain : PerkBase
    {
        public override PerkNames perkName => PerkNames.FuryOfPain; 

        public override PerkType perkType => PerkType.A1;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

        public override string desc => "If self hp< 40% on cast, gains Enraged after 2 rds /n No Luck and Haste handicap./n If Luck 12; gains +3 fort origin until eoq ";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.IgnorePain;

        public override SkillLvl skillLvl => SkillLvl.Level1;


        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
        bool subscribed = false; 

        public override void SkillHovered()
        {
            base.SkillHovered();
            SkillService.Instance.SkillWipe += skillController.allSkillBases.Find(t => t.skillName == skillName).WipeFX3;

        }

        public override void SkillSelected()
        {
            base.SkillSelected();
            SkillService.Instance.SkillFXRemove += skillController.allSkillBases.Find(t => t.skillName == skillName).RemoveFX1;

        }


        public override void ApplyFX1()
        {
            StatData hpStatData = charController.GetStat(StatsName.health);
            float hpPercent = hpStatData.currValue / hpStatData.maxLimit;
            if (hpPercent < 0.4f)
            {
                CharStatesService.Instance.ApplyCharState(targetGO, CharStateName.Enraged
                                     , charController, CauseType.CharSkill, (int)skillName);
            }
        }

        public override void SkillEnd()
        {
            base.SkillEnd();
            CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Enraged);
        }

        public override void ApplyFX2()
        {
            StatData luckStat = charController.GetStat(StatsName.luck);
            if (luckStat.currValue == 12 && !subscribed)
            {
                charController.charModel.fortitudeOrg += 2; 

               // charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.fortitude, +2, false);
                QuestEventService.Instance.OnEOQ += fortEOQ;
                subscribed = true;
            }
        }

        void fortEOQ()
        {
            //  charController.ChangeStat(CauseType.CharSkill, (int)skillName, charController, StatsName.fortitude, -2, false);
            charController.charModel.fortitudeOrg -= 2;
            QuestEventService.Instance.OnEOQ -= fortEOQ;
            subscribed = false;
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
        //        "(add) If he is below 40% hp when cast, he gets enraged for 2 rds.
        //(subtract) -50% healing until EoC
        //If Luck 12, he receive +2 fort origin until eoq upon cast. 
        public override void DisplayFX1()
        {
            str0 = $"+6<style=Attributes> Acc </style>";
            SkillService.Instance.skillCardData.descLines.Add(str0);

            str1 = $"If Luck 12, +2<style=Attributes> Fort org </style>, until eoq";
            SkillService.Instance.skillCardData.descLines.Add(str1);

        }

        public override void DisplayFX2()
        {
            str2 = $"If self HP < 40%<style=States> Enraged </style>,{skillModel.castTime} rds";
            SkillService.Instance.skillCardData.descLines.Add(str2);
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
    }

}


//        public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.IgnorePain;

//        public override SkillLvl skillLvl => SkillLvl.Level1;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.FuryOfPain;

//        public override PerkType perkType => PerkType.A1;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Fury of Pain";

//        private float _chance = 0f;
//        public override float chance { get => _chance; set => _chance = value; }

//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                              .Find(t => t.skillName == skillName);
//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            skillController = SkillService.Instance.currSkillMgr;
//            charGO = SkillService.Instance.GetGO4Skill(charName);
//            // skillModel Updates
//            skillModel.damageMod = 120f;

//        }
//        public override void SkillHovered()
//        {
//            SkillInit();
//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            Debug.Log("skill hovered" + perkName);
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillHovered += DisplayFX3;

//            SkillService.Instance.SkillWipe += skillController.allPerkBases.Find(t => t.skillName == skillName
//            && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).WipeFX1;
//        }

//        public override void SkillSelected()
//        {
//            SkillController skillController = SkillService.Instance.currSkillMgr;

//            skillController.allPerkBases.Find(t => t.skillName == skillName && t.skillLvl == SkillLvl.Level1
//            && t.state == PerkSelectState.Clicked).RemoveFX1();
//            Debug.Log("pressurised water is selected" + skillController.allPerkBases.Find(t => t.skillName == skillName
//                       && t.skillLvl == SkillLvl.Level1 && t.state == PerkSelectState.Clicked).perkName);

//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            SkillService.Instance.SkillApply += ApplyFX3;
//            SkillService.Instance.SkillApply += ApplyFX4;
//        }
//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
//        }
//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            targetController.dmgController.ApplyDamage(CharacterService.Instance.GetCharCtrlWithName(charName)
//                             , DamageType.MagicalWater, 120f, false);
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
//        }

//        public override void DisplayFX2()
//        {
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
//            SkillService.Instance.SkillApply -= ApplyFX1;

//        }

//        public override void RemoveFX2()
//        {
//            SkillService.Instance.SkillApply -= ApplyFX2;

//        }

//        public override void RemoveFX3()
//        {
//        }

//        public override void RemoveFX4()
//        {
//        }

//        public override void SkillEnd()
//        {
//        }    

//        public override void Tick()
//        {
//            if (roundEnd >= 2)
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


