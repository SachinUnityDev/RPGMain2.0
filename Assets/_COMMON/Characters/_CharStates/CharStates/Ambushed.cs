//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Combat;

//namespace Common
//{

//    public class Ambushed : CharStatesBase
//    {
//        CharController charController;
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
//        public override CharStateName charStateName => CharStateName.Ambushed;
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        Start is called before the first frame update
//        void Start()
//        {
//            charController = gameObject.GetComponent<CharController>();
//            charID = charController.charModel.charID;
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);

//            StateInit(1, TimeFrame.EndOfCombat);
//        }

//        public override void StateInit(int _castTime, TimeFrame timeFrame, GameObject charGo = null)
//        {

//            castTime = _castTime; timeElapsed = 0;
//            CombatEventService.Instance.OnEOC += Tick;

//            foreach (CharController ch in CharacterService.Instance.allyInPlayControllers)
//            {
//                ch.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, -1);
//            }
//        }

//        protected override void Tick()
//        {
//            timeElapsed++;
//            if (timeElapsed >= castTime)
//                EndState();

//        }

//        public override void EndState()
//        {
//            foreach (CharController ch in CharacterService.Instance.allyInPlayControllers)
//            {
//                ch.ChangeStat(CauseType.CharState, (int)charStateName, charID, StatsName.haste, 1);
//            }
//            charController.charModel.InCharStatesList.Remove(charStName);
//            CombatEventService.Instance.OnEOC -= EndState;
//            Destroy(this);   // remains for only one EOC ; 
//        }


//    }
//}