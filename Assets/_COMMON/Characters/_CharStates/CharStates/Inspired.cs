using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{

    public class Inspired : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Inspired;
        public override CharStateModel charStateModel { get; set; }
        public override CharController charController { get; set; }
        public override int charID { get; set; }
        public override StateFor stateFor => StateFor.Mutual;
        public override int castTime { get; protected set; }

        public override void StateApplyFX()
        {
            foreach (CharController c in CharService.Instance.allyInPlayControllers)
            {
                if (c != charController)
                {
                    c.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, StatsName.morale, 1, charStateModel.timeFrame, charStateModel.castTime, true);
                }
            }

            charController.charStateController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.focus, -1, charStateModel.timeFrame, charStateModel.castTime, true);

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.Despaired);
            charController.OnStatCurrValSet += Tick2;
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Inspired </style>";

            str1 = $"+1<style=Attributes> Morale </style>for allies";

            str2 = $"-1<style=Attributes> Focus </style>for self";

            str3 = "Immune to <style=States> Despaired </style>";
            //SkillServiceView.Instance.skillCardData.descLines.Add(str1);

            // str0 = "<style=Morale> "; 
            // add to charStateModel .. use this during skill Init
            // relieve charController from string duty ...

        }

        void Tick2(CharModData charModData)  //  change Stat subscribe 
        {
            if (charModData.statModified != StatsName.morale)
                return;

            float maxL = charController.GetStatChanceData(StatsName.morale).maxLimit;
            if (charController.GetStat(StatsName.morale).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }

        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Despaired);
        }
    }
}


//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Inspired;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }
//        float buffer;
//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);
//            StateInit(-1, TimeFrame.None, null);
//        }
//       // teammates has +1 morale	-1 focus for self .....threshold of 3 for incoming morale decreasing attacks

//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//            {
//                if (c != charController)
//                {
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID,  StatsName.morale, 1);
//                }
//            }
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, -1);
//            buffer = 0; 
//            charController.OnStatChanged += Tick2;
//            charController.OnStatChanged += MoraleDecr; 
//        }
//        //StartEND : Focus 12(max) ...Focus drops below 12

//        protected override void Tick()
//        {



//        }
//      //  threshold of 3 for incoming morale decreasing attacks
//        void MoraleDecr(CharModData charModData)
//        {
//            if (charModData.statModfified != StatsName.morale)
//                return;

//            buffer += charModData.modifiedVal;  // -1. -1 , -2 // 12 11=> 12
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -charModData.modifiedVal, false);
//            if (Mathf.Abs(buffer) >= 3)   
//            {
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, buffer, false); // nullfiy value changed               
//            }
//        }

//       // Morale 12(max) Morale drops below 12

//        void Tick2(CharModData charModData)  //  change Stat subscribe 
//        {
//            if (charModData.statModfified != StatsName.morale)
//                return;

//            float maxL = charController.GetStatChanceData(StatsName.morale).maxLimit;
//            if (charController.GetStat(StatsName.morale).currValue < maxL)  // Exit condition 
//            {
//                EndState();
//            }

//        }
//        public override void EndState()
//        {
//            foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//            {
//                if (c != charController)
//                {
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, -1,false);
//                }
//            }
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 1, false);

//            charController.OnStatChanged -= Tick2;
//            charController.OnStatChanged -= MoraleDecr;

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 

//            Destroy(this);
//        }
