using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Lightfooted : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.Lightfooted;
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
                    c.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                        , charID, StatsName.haste, 1, charStateModel.timeFrame, charStateModel.castTime, true);
                }
            }

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.luck, -1, charStateModel.timeFrame, charStateModel.castTime, true);

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.Rooted);
            charController.OnStatCurrValSet += Tick2;
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Lightfooted </style>"; 

            str1 = "+1<style=Attributes> Haste </style>for allies";

            str2 = "-1<style=Attributes> Luck </style>for self";

            str3 = "Immune to <style=States> Rooted </style>"; 

            //SkillServiceView.Instance.skillCardData.descLines.Add(str1);

            // str0 = "<style=Morale> "; 
            // add to charStateModel .. use this during skill Init
            // relieve charController from string duty ...

        }

        void Tick2(CharModData charModData)  //  change Stat subscribe 
        {
            if (charModData.statModified != StatsName.haste)
                return;

            float maxL = charController.GetStatChanceData(StatsName.haste).maxLimit;
            if (charController.GetStat(StatsName.haste).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }

        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Rooted);
        }



    }
}


//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Pioneer;
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

//        //teammates has +1 init..... threshold of 3 for incoming init decreasing attacks.....	-1 luck for self


//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//            {
//                if (c != charController)
//                {
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 1);                   
//                }
//            }
//            buffer = 0; 
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -1);
//            charController.OnStatChanged += ThresholdInit;
//            charController.OnStatChanged += Tick2;

//        }

//        void ThresholdInit(CharModData charModData)   // TO BE REVISITED 
//        {
//            if (charModData.statModfified != StatsName.haste)
//                return;

//            buffer += charModData.modifiedVal;  // -1. -1 , -2 // 12 11=> 12  => 12-4 => 8 
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste
//                            , -charModData.modifiedVal, false);
//            if (Mathf.Abs(buffer) >= 3)
//            {
//                charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, buffer, false); // nullfiy value changed
//            }           

//        }
//        protected override void Tick()
//        {



//        }


//        //Init 12(max) Init drops below 12

//        void Tick2(CharModData charModData)  //  change Stat subscribe 
//        {
//            if (charModData.statModfified != StatsName.haste)
//                return;

//            float maxL = charController.GetStatChanceData(StatsName.haste).maxLimit;
//            if (charController.GetStat(StatsName.focus).currValue < maxL)  // Exit condition 
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
//                    c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -1, false);
//                }
//            }
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 1, false);
//            charController.OnStatChanged -= ThresholdInit;
//            charController.OnStatChanged -= Tick2;

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 


//            Destroy(this);
//        }