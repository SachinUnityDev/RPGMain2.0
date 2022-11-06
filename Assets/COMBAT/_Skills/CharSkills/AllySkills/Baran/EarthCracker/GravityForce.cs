using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Combat
{
    public class GravityForce : PerkBase
    {
        public override PerkNames perkName => PerkNames.GravityForce;
        public override PerkType perkType => PerkType.A2;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override PerkSelectState state { get => _state; set => _state = value; }
        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SplintersOfEarth };

        public override string desc => "(PR: A1)/n  Rooted on initial target/n On collateral targets, -1 luck, 2 rds /n 50% dmg on initial target if rooted";

        public override CharNames charName => CharNames.Baran;

        public override SkillNames skillName => SkillNames.EarthCracker;

        public override SkillLvl skillLvl => SkillLvl.Level2;

        private float _chance = 60f;
        public override float chance { get => _chance; set => _chance = value; }

        List<DynamicPosData> colDynaCopy = new List<DynamicPosData>(); 
  
        public override void BaseApply()
        {
            base.BaseApply();            
            DynamicPosData targetDyna = GridService.Instance.GetDyna4GO(targetGO);
            colDynaCopy.Clear(); 
            if (targetDyna != null)
            {
                CombatService.Instance.colTargetDynas =
                   GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

                colDynaCopy.AddRange(CombatService.Instance.colTargetDynas);                   
            }               
        }
        public override void ApplyFX1()
        {
            if (CharStatesService.Instance.HasCharState(targetGO, CharStateName.Rooted))
                targetController
                    .ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.luck, 2);

            if (chance.GetChance())
            CharStatesService.Instance.ApplyCharState(targetGO, CharStateName.Rooted
                                     , charController, CauseType.CharSkill, (int)skillName);


        }
        public override void SkillEnd()
        {
            base.SkillEnd();
            //colDynaCopy.ForEach(t => t.charGO.GetComponent<CharController>()
            //  .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
            //  , StatsName.luck, 1, TimeFrame.EndOfRound, skillModel.castTime, true));
            CharStatesService.Instance.RemoveCharState(targetGO, CharStateName.Rooted);
        }
        public override void ApplyFX2()
        {
            colDynaCopy.ForEach(t => t.charGO.GetComponent<CharController>()
              .buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID
              , StatsName.luck, 1, TimeFrame.EndOfRound, skillModel.castTime, true));
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
            str0 = $"<style=States> Rooted </style>on main target, {skillModel.castTime} rds";
            SkillServiceView.Instance.skillCardData.descLines.Add(str0);
        }

        public override void DisplayFX2()
        {
            str1 = $"+50%<style=Physical> Physical </style>on main target if already rooted";
            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX3()
        {
            str2 = $"-1<style=Attributes> Luck </style>on col targets, {skillModel.castTime} rds";
            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
        }

        public override void DisplayFX4()
        {
        }
  
    }
}


//public override CharNames charName => CharNames.Baran;

//        public override SkillNames skillName => SkillNames.EarthCracker;

//        public override SkillLvl skillLvl => SkillLvl.Level2;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }

//        public override PerkNames perkName => PerkNames.GravityForce;

//        public override PerkType perkType => PerkType.A2;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.SplintersOfEarth };

//        public override string desc => "Gravity force";

//        private float _chance = 60f;
//        public override float chance { get => _chance; set => _chance = value; }

//        DynamicPosData targetDyna;
//        List<DynamicPosData> adjDynas = new List<DynamicPosData>(); 
//        public override void SkillInit()
//        {
//            skillModel = SkillService.Instance.allSkillModels
//                                           .Find(t => t.skillName == skillName);

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
//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            // get first target on the same lane
//            targetDyna = GridService.Instance.gridView
//                     .GetDynaFromPos(skillModel.targetPos[0].pos, skillModel.targetPos[0].charMode);
//            adjDynas = GridService.Instance.gridController.GetAllAdjDynaOccupied(targetDyna);

//        }

//        public override void BaseApply()
//        {
//            CombatEventService.Instance.OnEOR += Tick;
//        }
//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            if (_chance.GetChance())
//            {
//                CharStatesService.Instance.SetCharState(targetDyna.charGO, CharStateName.Rooted);
//                targetDyna.charGO.GetComponent<CharController>().dmgController
//                    .ApplyDamage(charController, DamageType.Physical, 50f, false); 

//            }

//        }

//        public override void ApplyFX2()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyAlly()) return;
//            foreach (DynamicPosData dyna in adjDynas)
//            {
//                CharController target = dyna.charGO.GetComponent<CharController>();
//                target.ChangeStat(StatsName.luck, -1, 0, 0); 
//            }
//        }
//        public override void ApplyFX3()
//        {
//        }

//        public override void ApplyFX4()
//        {
//        }    

//        public override void DisplayFX1()
//        {
//            str1 = $"{chance}%<style=States> Root </style>";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {
//            str2 = $"-1 Luck Adj, 2 rds";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
//        }

//        public override void DisplayFX3()
//        {
//            str3 = $"+50%<style=Physical> Physical </style>, if Rooted";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str3);
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
//            SkillService.Instance.SkillApply -= ApplyFX3;

//        }

//        public override void RemoveFX4()
//        {
//        }

//        public override void SkillEnd()
//        {
//        }  

//        public override void Tick()
//        {
//        }

//        public override void WipeFX1()
//        {
//            SkillService.Instance.SkillHovered += DisplayFX1;

//        }

//        public override void WipeFX2()
//        {
//            SkillService.Instance.SkillHovered += DisplayFX2;

//        }

//        public override void WipeFX3()
//        {
//            SkillService.Instance.SkillHovered += DisplayFX3;

//        }

//        public override void WipeFX4()
//        {
//        }

