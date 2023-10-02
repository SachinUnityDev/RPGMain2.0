using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Combat
{
    public class InitimidatingShout : SkillBase
    {

        private CharNames _charName;
        public override CharNames charName { get => _charName; set => _charName = value; }
        public override SkillNames skillName => SkillNames.IntimidatingShout;
        public override SkillLvl skillLvl => SkillLvl.Level0;

        public override StrikeTargetNos strikeNos => StrikeTargetNos.Multiple;
        public override string desc => "This is intimading shout";


        private float _chance = 0f;
        public override float chance { get => _chance; set => _chance = value; }
       

        List<DynamicPosData> targetsDynas = new List<DynamicPosData>();
        public override void PopulateTargetPos()
        {

        }
        public override void SkillSelected()
        {

            skillModel.targetPos.Clear();
            DynamicPosData selfDyna = GridService.Instance.GetDyna4GO(charGO);
            CellPosData selfPos = new CellPosData(selfDyna.charMode, selfDyna.currentPos);
            skillModel.targetPos.Add(selfPos);
            for (int i = 1; i < 8; i++)
            {
                CellPosData cellPosData = new CellPosData(CharMode.Enemy, i);
                skillModel.targetPos.Add(cellPosData);
            }

            GridService.Instance.HLTargetTiles(skillModel.targetPos); // overriden by next skill 
        }

        public override void BaseApply()
        {

            CombatEventService.Instance.OnEOR += Tick;
            targetsDynas = GridService.Instance.GetAllOccupiedbyCharMode(CharMode.Enemy);


            skillModel.lastUsedInRound = CombatService.Instance.currentRound;
            charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatName.stamina, -skillModel.staminaReq);
        }
        public override void DisplayFX1()
        {
            str0 = "<margin=1.2em>Buff, Debuff";
            SkillService.Instance.skillModelHovered.descLines.Add(str0);

            str1 = $"<style=Performer>+3 Acc and Dodge";
            SkillService.Instance.skillModelHovered.descLines.Add(str1);
        }
        public override void ApplyFX1()
        {

            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.acc, +3
                , TimeFrame.EndOfRound, skillModel.castTime, true); 
             
            charController.buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.dodge, +3
                , TimeFrame.EndOfRound, skillModel.castTime, true);
        }

        public override void DisplayFX2()
        {
            str2 = $"<style=Enemy> -2 Morale";
            SkillService.Instance.skillModelHovered.descLines.Add(str2);
        }

        public override void ApplyFX2()
        {
        
            foreach (var dyna in targetsDynas)
            {
                dyna.charGO.GetComponent<CharController>().buffController.ApplyBuff(CauseType.CharSkill, (int)skillName, charID, AttribName.morale, -2
                    , TimeFrame.EndOfRound, skillModel.castTime, true);

            }

        }
      
        public override void SkillEnd()
        {
            //if (IsTargetMyEnemy())
            //{
            //    foreach (var dyna in targetsDynas)
            //    {
            //        dyna.charGO.GetComponent<CharController>().ChangeStat(CauseType.CharSkill, (int)skillName, charID
            //                        , StatsName.morale, -2);

            //    }
            //}
            //else if (IsTargetMyAlly())
            //{
            //    charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.acc, -3);
            //    charController.ChangeStat(CauseType.CharSkill, (int)skillName, charID, StatsName.dodge, -3);
            //}

          

        }
        public override void ApplyFX3()
        {

        }

    
        public override void DisplayFX3()
        {

        }

        public override void DisplayFX4()
        {

        }

        public override void ApplyVFx()
        {
          
        }

    
        public override void PopulateAITarget()
        {
          
        }

        public override void ApplyMoveFx()
        {
        }
    }

}
