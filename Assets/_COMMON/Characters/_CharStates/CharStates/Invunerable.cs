//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Common
//{
//    public class Invunerable : CharStatesBase
//    {
//        public override CharStateName charStateName => CharStateName.Invulnerable;
//        CharController charController;
//        public GameObject causedByGO;
      
//        public override int castTime { get; set; }
//        public override float dmgPerRound => 1.0f;
        
//        public override StateFor stateFor => StateFor.Mutual;
//        public int timeElapsed { get; set; }

//        float dmgPercentInit;
//        float hits;
//        private void Start()
//        {
//            charController = GetComponent<CharController>();
//            if (charController.charModel.Immune2CharStateList.Contains(charStateName))
//                Destroy(this);
//            //  OnCharStateStart(-1, TimeFrame.None);

//        }

//    }



//}
