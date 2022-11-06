//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Common;
//using System.Linq; 

//namespace Combat
//{
//    public class AnimalTrap : SkillBase
//    {
//        public override CharNames charName => CharNames.Abbas;

//        public override SkillNames skillName => SkillNames.AnimalTrap;
//        public override SkillLvl skillLvl => SkillLvl.Level0;
//        private PerkSelectState _state = PerkSelectState.Clickable;
//        public override PerkSelectState skillState { get => _state; set => _state = value; }
//        public override PerkNames perkName => PerkNames.None;

//        public override PerkType perkType => PerkType.None;

//        public override List<PerkNames> preReqList => new List<PerkNames>() { PerkNames.None };

//        public override string desc => "Animal Trap";

//        private float _chance = 40f;
//        public override float chance { get => _chance; set => _chance = value; }

//        float usage = 0; bool isNotRooted = false; 
//        public override void SkillInit()
//        {
//            if (SkillService.Instance.allSkillModels.Any(t => t.skillName == skillName)) return;

//            SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);
//            skillData = skillDataSO.allSkills.Find(t => t.skillName == skillName);

//            skillModel = new SkillModel(skillData);
//            SkillService.Instance.allSkillModels.Add(skillModel);
//            skillModel.castPos = new List<int>() { 4, 5, 6, 7 };

//            charController = CharacterService.Instance.GetCharCtrlWithName(charName);
//            charGO = SkillService.Instance.GetGO4Skill(charName);
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

//            skillModel.targetPos.Clear();
//            for (int i = 1; i < 8; i++)
//            {            
//                CellPosData cellPosData = new CellPosData(charController.charModel.charMode.FlipCharMode(), i);
//                skillModel.targetPos.Add(cellPosData);              
//            }
//            GridService.Instance.HLTargetTiles(skillModel.targetPos); // overriden by next skill 

//        }

//        bool HasUsedMax()
//        {
//            if (usage < skillModel.maxUsagePerCombat)
//                return true;
//            else
//                return false; 
//        }

//        public override void BaseApply()
//        {
//            usage++;
//            targetGO = SkillService.Instance.currentTargetDyna.charGO;
//            targetController = targetGO.GetComponent<CharController>();

//            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
//            charController.ChangeStat(StatsName.stamina, -skillModel.staminaReq, 0, 0);

//        }
//        public override void ApplyFX1()
//        {

//            CombatEventService.Instance.OnEOR += ApplyFX1_EOR; 
//        }

//        void ApplyFX1_EOR()
//        {

//            if (HasUsedMax() || appliedOnce || IsTargetMyAlly()) return;

//            if (targetController.charModel.raceType == RaceType.Animal)
//                CharStatesService.Instance.SetCharState(targetGO, CharStateName.Rooted);
//            else
//            {
//                targetController.ChangeStat(StatsName.initiative, -3, 0, 0);
//                CombatEventService.Instance.OnEOR += Tick;
//                isNotRooted = true; 
//            }

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
//            str0 = "<margin=1.2em>Ranged";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str0);

//            str1 = $"If animal<style=States> Rooted </style>";
//            SkillServiceView.Instance.skillCardData.descLines.Add(str1);
//        }

//        public override void DisplayFX2()
//        {
//            str2 = $"Else -3 Haste";
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
//            appliedOnce = true;

//        }

//        public override void PreApplyFX()
//        {
//            appliedOnce = false;

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
//            if (IsTargetMyAlly() ) return;

//            if (isNotRooted)
//             targetController.ChangeStat(StatsName.initiative, -2, 0, 0);
           

//            CombatEventService.Instance.OnEOR -= Tick;
         
//        }

     

//        public override void Tick()
//        {
//            if (roundEnd >= skillModel.cd)
//                SkillEnd();
//            roundEnd++;
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

