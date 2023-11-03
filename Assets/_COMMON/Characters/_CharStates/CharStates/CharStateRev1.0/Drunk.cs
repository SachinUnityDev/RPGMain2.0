using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Drunk : CharStatesBase
    {
        //-2 Focus	-2 Acc	-2 Dodge	+6 fort origin	
        //can not cheat death -(cheat death chance is divded into die
        //and lose fortitude chances on Last Drop of Blood effects)

        public override CharStateName charStateName => CharStateName.Drunk;       
        public override StateFor stateFor => StateFor.Heroes;
        public override int castTime { get; protected set; }
        public override float chance { get; set; }
        public override void StateApplyFX()
        {

            CalendarService.Instance.OnStartOfCalDay += OnEndofTheNight;
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
            , charID, AttribName.focus, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.acc, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
               , charID, AttribName.dodge, -2, timeFrame, castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
               , charID, AttribName.fortOrg, 6, timeFrame, castTime, true);
            allBuffIds.Add(buffID);



        }
        void OnEndofTheNight(int day)
        {
            EndState();
        }
        public override void EndState()
        {
            base.EndState();
            CalendarService.Instance.OnStartOfCalDay -= OnEndofTheNight;
        }


        public override void StateApplyVFX()
        {
            
        }
        public override void StateDisplay()
        {
            str0 = "-2 Focus, -2 Acc, -2 Dodge";
            allStateFxStrs.Add(str0);

            str1 = "+6 Fort Origin";
            allStateFxStrs.Add(str1);
        }
    }
}