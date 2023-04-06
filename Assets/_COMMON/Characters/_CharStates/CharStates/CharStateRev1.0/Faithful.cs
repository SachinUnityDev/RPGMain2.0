using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace Common
{

    public class Faithful : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Faithful;

        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }

        public override StateFor stateFor => StateFor.Heroes;

        public override int castTime { get; protected set;}
        //Immune to Fortitude attacks for 3 rds
        //after 3 rds go back to origin	
        //+2 Focus, haste, Luck, Morale, Dodge and +(2-2) Armor + 20 resistances once per combat
        public override void StateApplyFX()
        {
            castTime = 2;
            int buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.focus, 2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.morale, 2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.luck, 2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                , charID, AttribName.haste, 2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
          , charID, AttribName.dodge, 2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuffOnRange(CauseType.CharState, (int)charStateName
            , charID, AttribName.armor, 2, 2, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.earthRes, 20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.waterRes, 20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.fireRes, 20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            buffID = charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                 , charID, AttribName.airRes, 20, charStateModel.timeFrame, charStateModel.castTime, true);
            allBuffIds.Add(buffID);

            charController.ClampStatToggle(StatName.fortitude, true);
        }
        public override void EndState()
        {
            base.EndState();
            charController.ClampStatToggle(StatName.fortitude, false);
        }
        public override void StateApplyVFX()
        {
            
        }

        public override void StateDisplay()
        {
            str0 = "2 Utility Stats, 2 Dodge";
            charStateModel.charStateCardStrs.Add(str0);

            str1 = "2-2 Armor, 20 Elemental Res";
            charStateModel.charStateCardStrs.Add(str1);

        }
    }
}

