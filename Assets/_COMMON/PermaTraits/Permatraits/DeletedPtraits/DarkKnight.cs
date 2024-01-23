//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Common
//{
//    public class DarkKnight : PermaTraitBase
//    {
//        // Can't be ambushed at night

//        // +2 init at night

//        // Start is called before the first frame update
          
//        public override PermaTraitName permaTraitName => PermaTraitName.DarkKnight;
//        public override void ApplyTrait(CharController charController)
//        {
//           base.ApplyTrait(charController);   
//            //CalendarEventService.Instance.OnStartOfTheNight += AddInitNight;
//            //CalendarEventService.Instance.OnStartOfTheDay += AddInitDay;
//            // TO BE FIXED ON REVISION WITH SEMIH 

//            //QuestEventService.Instance.dayNightController.ONStartOfDay += AddInit;
//            //QuestEventService.Instance.dayNightController.ONStartOfNight += NoAmbush;
//            //QuestEventService.Instance.dayNightController.ONStartOfDay += NoAmbush;
           
//        }

//        void AddInitNight()
//        {
//            charController.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.haste, 2);
//        }
//        void AddInitDay()
//        {
//            charController.ChangeAttrib(CauseType.PermanentTrait, (int)permaTraitName, charID, AttribName.haste, -2);
//        }

       
//    }
//}

