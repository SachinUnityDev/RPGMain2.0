using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;
namespace Combat
{

    public class HatchetSwing : SkillBase
    {
        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }

        public override SkillNames skillName => SkillNames.HatchetSwing;

        public override SkillLvl skillLvl => SkillLvl.Level0;

        private PerkSelectState _state = PerkSelectState.Clickable;
        public override string desc => "Hatchet Swing";

        private float _chance = 40f;
        public override float chance { get => _chance; set => _chance = value; }
        public override SkillModel skillModel { get; set; }
     

        public override StrikeTargetNos strikeNos { get; }

        //public override void SkillInit(SkillController1 skillController)
        //{
        //    base.SkillInit(skillController);
        //    //if (SkillService.Instance.allSkillModels.Any(t => t.skillName == skillName)) return;

        //    //SkillDataSO skillDataSO = SkillService.Instance.GetSkillSO(charName);
        //    //skillData = skillDataSO.allSkills.Find(t => t.skillName == skillName);

        //    //skillModel = new SkillModel(skillData);
        //    //SkillService.Instance.allSkillModels.Add(skillModel);

        //    //charController = CharacterService.Instance.GetCharCtrlWithName(charName);
        //    //charGO = SkillService.Instance.GetGO4Skill(charName);

        //    //skillModel.castPos = new List<int>() { 2, 3, 4, 5, 6, 7 };

        //}

        //public override void SkillHovered()
        //{
        //    //SkillInit();
        //    base.SkillHovered();
        //}


        //public override void SkillSelected()
        //{
        //    base.SkillSelected(); 
        //    if(GameService.Instance.gameModel.gameState == GameState.InCombat)
        //    {
        //        DynamicPosData currCharDyna = GridService.Instance.GetDyna4GO(charGO);

        //        if (!skillModel.castPos.Any(t => t == currCharDyna.currentPos))
        //            return;
        //        CharMode currCharMode = charController.charModel.charMode;
        //        skillModel.targetPos.Clear();
        //        for (int i = 1; i <= 4; i++)
        //        {
        //            CellPosData cellPosData = new CellPosData(currCharMode.FlipCharMode(), i);
        //            skillModel.targetPos.Add(cellPosData);
        //        }
        //        GridService.Instance.HLTargetTiles(skillModel.targetPos); // overriden by next skill 
        //    }
           
        //}
        //public override void BaseApply()
        //{
        //    targetGO = SkillService.Instance.currentTargetDyna.charGO;
        //    targetController = targetGO.GetComponent<CharController>();
        //    CombatEventService.Instance.OnEOR += Tick;

        //    skillModel.lastUsedInRound = CombatService.Instance.currentRound;
        // //   charController.ChangeStat(StatsName.stamina, -skillModel.staminaReq, 0, 0);
        //}

        public override void ApplyFX1()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

            //if (targetController)
            //    targetController.dmgController.ApplyDamage(charController, DamageType.Physical
            //                                            , skillModel.damageMod, false);
        }

        public override void ApplyFX2()
        {
            //if (CkhNSetInCoolDown() || appliedOnce || IsTargetMyEnemy()) return;

            //if (targetController)
            //{
            //    if (chance.GetChance())
            //        CharStatesService.Instance.SetCharState(targetGO, CharStateName.BleedLowDOT);
            //}
        }

        public override void ApplyFX3()
        {
        }

      


        public override void DisplayFX1()
        {
            str0 = "<margin=1.2em>Ranged";
            SkillService.Instance.skillCardData.descLines.Add(str0);

            str1 = $"{skillModel.damageMod}%<style=Physical> Physical,</style> dmg";
            SkillService.Instance.skillCardData.descLines.Add(str1);
        }

        public override void DisplayFX2()
        {
            str2 = $"{chance}%<style=Bleed> Low Bleed</style>";
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
          ///  appliedOnce = false;

        }

        public override void PreApplyFX()
        {
          //  appliedOnce = true;

        }

        public override void RemoveFX1()
        {
            SkillService.Instance.SkillApply -= ApplyFX1;

        }

        public override void RemoveFX2()
        {
            SkillService.Instance.SkillApply -= ApplyFX2;

        }

        public override void RemoveFX3()
        {
            SkillService.Instance.SkillApply -= ApplyFX3;

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
            SkillService.Instance.SkillHovered -= DisplayFX2;

        }

        public override void WipeFX3()
        {
            SkillService.Instance.SkillHovered -= DisplayFX3;

        }

        public override void WipeFX4()
        {
        }

        public override void PopulateTargetPos()
        {
           
        }

        public override void ApplyVFx()
        {
          
        }

        public override void ApplyMoveFx()
        {
          
        }

        public override void PopulateAITarget()
        {
           
        }
    }



}
