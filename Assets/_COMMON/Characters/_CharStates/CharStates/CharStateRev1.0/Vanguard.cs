using Combat;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

namespace Common
{
    public class Vanguard : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Vanguard;     
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {
            //+1 AP if starts turn on pos 1,2,3,
            //  +5 -10 fort 
            // + 2 armor Max at 1,2,3
            CombatEventService.Instance.OnCharOnTurnSet += ExtraAPFor123;
            SkillService.Instance.OnSkillUsed += OnGuardSkill; 
        }

        void OnGuardSkill(SkillEventData skillEventData)
        {
            if (skillEventData.strikerController.charModel.charID != charID) return;
            if (skillEventData.skillModel.skillInclination == SkillInclination.Guard)
            {
                charController.ChangeStat(CauseType.CharState, (int)charStateName
                                            ,charID, StatName.fortitude, UnityEngine.Random.Range(5, 11));
            }
            return; 
        }
        void ExtraAPFor123(CharController charController)
        {
            if (charController.charModel.charID != charID) return;
            DynamicPosData dyna = GridService.Instance.GetDyna4GO(charController.gameObject);
            if (dyna.currentPos == 1 || dyna.currentPos == 2 || dyna.currentPos == 3)
            {
                charController.combatController.actionPts++;
            }
            List<int> allPos = new List<int>() { 1, 2, 3 };

           int buffID =   charController.buffController.ApplyPosBuff(allPos, CauseType.CharState, (int)charStateName
                                    , charID, AttribName.armorMax, 2f, timeFrame, castTime, true); 
            allBuffIds.Add(buffID);
                       
        }
        public override void EndState()
        {
            base.EndState();
            CombatEventService.Instance.OnCharOnTurnSet -= ExtraAPFor123;
            SkillService.Instance.OnSkillUsed -= OnGuardSkill;
        }
        public override void StateApplyVFX()
        {
        }

        public override void StateDisplay()
        {
            str0 = "On use Guard skills: Gain 5-10 Fort";
            allStateFxStrs.Add(str0);
            str1 = "If on frontrow: +2 Max Armor";
            allStateFxStrs.Add(str1);
            str2 = "If on frontrow at sot: +1 AP";
            allStateFxStrs.Add(str2);
        }
    }
}