//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat; 

//namespace Common
//{

//    public class Faithful : CharStatesBase
//    {
//        CharController charController;
//        public GameObject causedByGO;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Faithful;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);
//            if (charController.charModel.InCharStatesList.Contains(charStateName))
//                return;
//            timeElapsed = 0; 
//            StateInit(-1, TimeFrame.None, null);

//        }
//       // Immune to Fortitude attacks for 3 rds and after 3 rds go back to origin
//       //+2 Focus, Init, Luck, Morale, Dodge and +(2-2) Armor + 20 resistances	3 fort origin until eoq once per combat


//        public override void StateInit(int _castTime, TimeFrame _timeFrame, GameObject _causedByChar = null)
//        {
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 2);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.dodge, 2);

//            charController.ChangeStatRange(CauseType.CharState, (int)charStateName, charID, StatsName.armor,2, 2);

//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.airRes, 20);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.waterRes, 20);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fireRes, 20);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.earthRes, 20);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.lightRes, 20);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.darkRes, 20);

//            charController.charModel.fortitudeOrg = 3;
//            CombatEventService.Instance.OnEOC += TickEOC;
//            CombatEventService.Instance.OnEOC += TickEOR;
//            charController.OnStatChanged += Immune2Fort;  
//        }

//        void Immune2Fort(CharModData charModData)
//        {
//            if (charModData.statModfified != StatsName.fortitude) return;
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID
//                , StatsName.fortitude, -charModData.modifiedVal, false);
//        }

//        void TickEOC()
//        {
//            EndState();
//        }

//        void TickEOR()
//        {
//            timeElapsed++;
//            if (timeElapsed > 3)
//            {
//                EndState();
//            }
//        }


//        public override void EndState()
//        {

//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.focus, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.luck, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.morale, 3);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.dodge, 3);

//            charController.ChangeStatRange(CauseType.CharState, (int)charStateName, charID, StatsName.armor, 3, 3);

//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.airRes, 30);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.waterRes, 30);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.fireRes, 30);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.earthRes, 30);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.lightRes, 30);
//            charController.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.darkRes, 30);

//            charController.charModel.fortitudeOrg = 0;
//            CombatEventService.Instance.OnEOC -= TickEOC;
//            CombatEventService.Instance.OnEOC -= TickEOR;
//            charController.OnStatChanged -= Immune2Fort;

//            charController.charModel.InCharStatesList.Remove(charStateName);  // added by charStateService 
//            Destroy(this);
//        }
//    }
//}