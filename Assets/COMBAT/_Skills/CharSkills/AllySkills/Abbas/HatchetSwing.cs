//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Common;
//using System.Linq; 
//namespace Combat
//{

//    public class HatchetSwing : SkillBase
//    {
//        public override CharNames charName => CharNames.Abbas;

//        public override SkillNames skillName => SkillNames.HatchetSwing;

//        public override SkillLvl skillLvl => SkillLvl.Level0;

//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.None;

//        public override PerkType perkType => PerkType.None;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Hatchet Swing";


//        private float _chance = 40f;
//        public override float chance { get => _chance; set => _chance = value; }

//        public override void SkillInit()
//        {
//            if (SkillService.Instance.allSkillModels.Any(t => t.skillName == skillName)) return;

//            SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);
//            skillData = skillDataSO.allSkills.Find(t => t.skillName == skillName);

//            skillModel = new SkillModel(skillData);
//            SkillService.Instance.allSkillModels.Add(skillModel);

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            charGO = SkillService.Instance.GetGO4Skill(charName);

//            skillModel.castPos = new List<int>() { 2, 3, 4,5,6,7 }; 

//        }

//        public override void SkillHovered()
//        {
//            SkillInit();

//            SkillServiceView.Instance.skillCardData.skillModel = skillModel;
//            SkillService.Instance.SkillHovered += DisplayFX1;
//            SkillService.Instance.SkillHovered += DisplayFX2;
//            SkillService.Instance.SkillHovered += DisplayFX3;
//        }


//        public override void SkillSelected()
//        {
//            DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

//            if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
//                return;

//            SkillService.Instance.SkillApply += BaseApply;
//            SkillService.Instance.SkillApply += ApplyFX1;
//            SkillService.Instance.SkillApply += ApplyFX2;
//            SkillService.Instance.SkillApply += ApplyFX3;
//            SkillService.Instance.SkillApply += PostApplyFX;

//            CharMode currCharMode = charController.charModel.charMode; 
//            skillModel.targetPos.Clear();
//            for (int i = 1; i <= 4; i++)
//            {
                
//                    CellPosData cellPosData = new CellPosData(currCharMode.FlipCharMode(), i);
//                    skillModel.targetPos.Add(cellPosData);                
//            }
//            GridService.Instance.HLTargetTiles(skillModel.targetPos); // overriden by next skill 
//        }
//        public override void BaseApply()
//        {
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();
//            CombatEventService.Instance.OnEOR += Tick;

//            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
//            charController.ChangeStat(StatsName.stamina, -skillModel.staminaReq, 0, 0);
//        }

//        public override void ApplyFX1()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

//            if (targetController)
//                targetController.dmgController.ApplyDamage(charController, DamageType.Physical
//                                                        ,skillModel.damageMod, false);
//        }

//        public override void ApplyFX2()
//        {
//            if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

//            if (targetController)
//            {
//                if (chance.GetChance())
//                    CharStatesService.Instance.SetCharState(targetGO, CharStateName.BleedLowDOT);
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
//            str0 = "<margin=1.2em>Ranged";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str0);

//            str1 = $"{skillModel.damageMod}%<style=Physical> Physical,</style> dmg";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {
//            str2 = $"{chance}%<style=Bleed> Low Bleed</style>";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str2);
//        }

//        public override void DisplayFX3()
//        {            

//        }

//        public override void DisplayFX4()
//        {           
//        }

//        public override void PostApplyFX()
//        {
//            appliedOnce = false;

//        }

//        public override void PreApplyFX()
//        {
//            appliedOnce = true;

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
//            SkillService.Instance.SkillHovered -= DisplayFX1;

//        }

//        public override void WipeFX2()
//        {
//            SkillService.Instance.SkillHovered -= DisplayFX2;

//        }

//        public override void WipeFX3()
//        {
//            SkillService.Instance.SkillHovered -= DisplayFX3;

//        }

//        public override void WipeFX4()
//        {
//        }
//    }



//}
