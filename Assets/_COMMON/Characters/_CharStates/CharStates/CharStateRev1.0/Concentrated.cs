﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Common
{

    public class Concentrated : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Concentrated;
      
        public override CharController charController { get; set; }
        public override int charID { get; set; }

        public override StateFor stateFor => StateFor.Mutual; 
        public override int castTime { get;protected set; }

        public override void StateApplyFX()
        {
            CharService.Instance.ApplyBuffOnPartyExceptSelf(CauseType.CharState, (int)charStateName
                       , charID, AttribName.focus, +1, timeFrame, castTime, true, CharMode.Ally);

             charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                   , charID, AttribName.morale, -1, timeFrame, castTime, true);

            charController.charStateController
                .ApplyImmunityBuff(CauseType.CharState, (int)charStateName
                   , charID, CharStateName.Confused,  timeFrame, castTime);
            charController.OnAttribCurrValSet += Tick2;
        }

        void Tick2(AttribModData charModData)  //  change Stat subscribe 
        {
            if (charModData.attribModified != AttribName.focus)
                return;

            float maxL = charController.GetAttrib(AttribName.focus).maxLimit;
            if (charController.GetAttrib(AttribName.luck).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }

        public override void StateApplyVFX()
        {
           
        }

        public override void StateDisplay()
        {
            //+1 Focus on allies .... - 1 Morale on self   Immune to Confused
            str0 = "+1 Focus on allies";
            charStateCardStrs.Add(str0);
            str1 = "-1 Morale on self";
            charStateCardStrs.Add(str1);
            str2 = "Immune to<style=States> Confused </style>";
            charStateCardStrs.Add(str2);
        }
    }
}


//public override CharStateName charStateName => CharStateName.Concentrated;
//public override CharStateModel charStateModel { get; set; }
//public override CharController charController { get; set; }
//public override int charID { get; set; }

//public override StateFor stateFor => StateFor.Mutual;

//public override int castTime { get; protected set; }

//public override void StateApplyFX()
//{
//    charController.SetStat(StatsName.focus, 12);
//    foreach (CharController c in CharService.Instance.allyInPlayControllers)
//    {
//        if (c.charModel.charID != charController.charModel.charID)
//        {
//            Debug.Log("Applied buff Once");
//            c.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
//                , charID, StatsName.focus, 1, charStateModel.timeFrame, charStateModel.castTime, true);
//        }
//    }
//    if (charController != null)
//    {
//        charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
//                  , charID, StatsName.morale, -1, charStateModel.timeFrame, charStateModel.castTime, true);
//        charController.SetCurrStat(CauseType.CharState, (int)charStateName, charID,
//            StatsName.focus, 12, true);
//    }


//    CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.Confused);
//    charController.OnStatCurrValSet += Tick2;
//}

//public override void StateApplyVFX()
//{

//}

//public override void StateDisplay()
//{
//    str0 = "<style=States> Concentrated </style>";

//    str1 = $"+1<style=Attributes> Focus </style>for allies";

//    str2 = $"-1<style=Attributes> Morale </style>for self";

//    str3 = "Immune to<style=States> Confused </style>";
//    SkillServiceView.Instance.skillCardData.descLines.Add(str1);

//    str0 = "<style=Morale> ";
//    add to charStateModel..use this during skill Init
//    relieve charController from string duty ...

//}

//void Tick2(CharModData charModData)  //  change Stat subscribe 
//{
//    if (charModData.statModified != StatsName.focus)
//        return;

//    float maxL = charController.GetStatChanceData(StatsName.focus).maxLimit;
//    if (charController.GetStat(StatsName.focus).currValue < maxL)  // Exit condition 
//    {
//        EndState();
//    }
//}

//public override void EndState()
//{
//    base.EndState();
//    CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Confused);
//}
//**************************************8
////CharController charController;
////public GameObject causedByGO;
////public override int castTime { get; set; }
////public override float dmgPerRound => 1.0f;
////public override CharStateName charStateName => CharStateName.Concentrated;
////public override StateFor stateFor => StateFor.Mutual;
////public int timeElapsed { get; set; }
////float buffer;
////private void Start()
////{
////    charController = GetComponent<CharController>();
////    charID = charController.charModel.charID;
////    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
////        Destroy(this);
////    StateInit(-1, TimeFrame.None, null);
////}
////// teammates has +1 focus.... threshold of 3 for incoming focus decreasing attacks.....	-1 morale to self
////public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
////{
////    foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
////    {
////        if (c != charController)
////        {
////            c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 1);
////        }
////    }
////    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -1);
////    buffer = 0;
////    charController.OnStatChanged += Tick2;
////    charController.OnStatChanged += FocusThreshold;

////}
//////StartEND : Focus 12(max) ...Focus drops below 12

////protected override void Tick()
////{



////}


////void Tick2(CharModData charModData)  //  change Stat subscribe 
////{
////    if (charModData.statModfified != StatsName.focus)
////        return;

////    float maxL = charController.GetStatChanceData(StatsName.focus).maxLimit;
////    if (charController.GetStat(StatsName.focus).currValue < maxL)  // Exit condition 
////    {
////        EndState();
////    }
////}

////void FocusThreshold(CharModData charModData)
////{
////    if (charModData.statModfified != StatsName.focus)
////        return;

////    buffer += charModData.modifiedVal;  // -1. -1 , -2 // 12 11=> 12  => 12-4 => 8 
////    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -charModData.modifiedVal, false);
////    if (Mathf.Abs(buffer) >= 3)
////    {
////        charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, buffer, false); // nullfiy value changed
////    }
////}

////public override void EndState()
////{
////    foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
////    {
////        if (c != charController)
////        {
////            c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -1, false);
////        }
////    }
////    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 1, false);

////    charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 

////    charController.OnStatChanged -= Tick2;
////    charController.OnStatChanged -= FocusThreshold;

////    Destroy(this);
////}