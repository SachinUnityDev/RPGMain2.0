using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{

    public class LuckyDuck : CharStatesBase
    {
        public override CharStateName charStateName => CharStateName.LuckyDuck;
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
                        , charID, StatsName.luck, 1, charStateModel.timeFrame, charStateModel.castTime, true);
                }
            }

            charController.buffController.ApplyBuff(CauseType.CharState, (int)charStateName
                       , charID, StatsName.haste, -1, charStateModel.timeFrame, charStateModel.castTime, true);

            CharStatesService.Instance.AddImmunity(charController.gameObject, CharStateName.Feebleminded);
            charController.OnStatCurrValSet += Tick2;
        }

        public override void StateApplyVFX()
        {

        }

        public override void StateDisplay()
        {
            str0 = "<style=States> Lucky Duck </style>";
            charStateModel.charStateCardStrs.Add(str0); 
            str1 = $"+1<style=Attributes> Luck </style>for allies";
            charStateModel.charStateCardStrs.Add(str1);
            str2 = $"-1<style=Attributes> Haste </style>for self";
            charStateModel.charStateCardStrs.Add(str2);
            str3 = "Immune to <style=States> Feebleminded </style>";
            charStateModel.charStateCardStrs.Add(str3);  
        }

        void Tick2(CharModData charModData)  //  change Stat subscribe 
        {
            if (charModData.statModified != StatsName.luck)
                return;

            float maxL = charController.GetStatChanceData(StatsName.luck).maxLimit;
            if (charController.GetStat(StatsName.luck).currValue < maxL)  // Exit condition 
            {
                EndState();
            }
        }

        public override void EndState()
        {
            base.EndState();
            CharStatesService.Instance.RemoveImmunity(charController.gameObject, CharStateName.Feebleminded);
        }
    }
}
//CharController charController;
//public override int castTime { get; set; }
//public override float dmgPerRound => 1.0f;
//public override CharStateName charStateName => CharStateName.LuckyDuck;
//public override StateFor stateFor => StateFor.Mutual;
//public int timeElapsed { get; set; }
//float buffer;
//void Start()
//{
//    charController = gameObject.GetComponent<CharController>();
//    charID = charController.charModel.charID;
//    if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//        Destroy(this);

//    StateInit(-1, TimeFrame.None, null);

//}
////teammates has +1 luck threshold of 3 for incoming luck decreasing attacks	-1 init for self

//public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject charGO = null)
//{
//    foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//    {
//        if (c != charController)
//        {
//            c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 1);
//        }
//    }
//    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -1);

//    buffer = 0;
//    charController.OnStatChanged += Tick2;
//    charController.OnStatChanged += LuckThreshold;


//}
////t hreshold of 3 for incoming luck decreasing attacks
//void LuckThreshold(CharModData charModData)
//{
//    if (charModData.statModfified != StatsName.luck)
//        return;

//    buffer += charModData.modifiedVal;  // -1. -1 , -2 // 12 11=> 12  => 12-4 => 8 
//    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -charModData.modifiedVal, false);
//    if (Mathf.Abs(buffer) >= 3)
//    {
//        charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, buffer, false); // nullfiy value changed
//    }

//}
////  Luck 12(max) Luck drops below 12
//void Tick2(CharModData charModData)  // end condition 
//{
//    if (charModData.statModfified != StatsName.luck) return;

//    float maxL = charController.GetStatChanceData(StatsName.luck).maxLimit;
//    if (charController.GetStat(charModData.statModfified).currValue < maxL)
//    {
//        EndState();
//    }

//}

//public override void EndState()
//{
//    foreach (CharController c in CharacterService.Instance.allyInPlayControllers)
//    {
//        if (c != charController)
//        {
//            c.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, -1, false);
//        }
//    }
//    charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 1, false);

//    charController.OnStatChanged -= Tick2;
//    charController.OnStatChanged -= LuckThreshold;

//    charController.charModel.InCharStatesList.Remove(charStateName);

//    Destroy(this);   // remains for only one EOC ; 
//}