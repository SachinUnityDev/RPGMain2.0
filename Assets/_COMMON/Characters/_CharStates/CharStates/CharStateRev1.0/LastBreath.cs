using Combat;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Common
{
    //Lose Hp instead of Stm upon using skill	Block Stamina gain/loss for 1 rd
    public class LastBreath : CharStatesBase  
    {
        // start condition stamina 0 
        public override CharStateName charStateName => CharStateName.LastBreath;      
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Heroes; 
        public override int castTime { get; protected set; }
        int roundCount = 0; 
        public override void StateApplyFX()
        {
            castTime = 1; 
            SkillService.Instance.OnSkillUsed += OnSkillUsed;
            charController.ClampStatToggle2Val(StatName.stamina, true,0);
        }

        void OnSkillUsed(SkillEventData skillEventData)
        {
           // Lose Hp instead of Stm upon using skill
            if (skillEventData.strikerController.charModel.charID != charController.charModel.charID)
                return; 
            // get hp lost 
            int staminaReq = skillEventData.skillModel.staminaReq;
            charController.ChangeStat(CauseType.CharSkill, (int)skillEventData.skillModel.skillName, charController.charModel.charID
                     , StatName.stamina, staminaReq);

            charController.ChangeStat(CauseType.CharSkill, (int)skillEventData.skillModel.skillName, charController.charModel.charID
                     , StatName.health, -staminaReq);
        }
        public override void EndState()
        {
            base.EndState();
            charController.ClampStatToggle(StatName.stamina, false);
            SkillService.Instance.OnSkillUsed -= OnSkillUsed;
        }

        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "Stamina gain blocked";
            charStateCardStrs.Add(str0);

            str1 = "Lose Hp instead of Stm on skill use";
            charStateCardStrs.Add(str1);
        }
    }
}