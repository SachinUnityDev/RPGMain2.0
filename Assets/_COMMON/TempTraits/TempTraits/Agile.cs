using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class Agile : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Agile; 
       // +2 Dodge	+2 Acc
        public override void OnApply()
        {
            int charID = charController.charModel.charID;
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.dodge, 2, TimeFrame.Infinity, -1, true);
           
            charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, StatsName.acc, 2, TimeFrame.Infinity, -1, true);

        }

        public override void OnEnd()
        {
           
        }
    }
}


//public override void ApplyTempTrait(CharacterController _charController )
//{
//    charController = _charController;
//    if(charController!= null)
//    {
//        int  valueChange = Random.Range(1, 3);  // 1,2 in data 
//        charController.ChangeStat(StatsName.Dodge,valueChange,0,0);
//        valueChange = Random.Range(1, 3);  //1,2 in data 
//        charController.ChangeStat(StatsName.acc, valueChange, 0, 0);                  
//    }   

//}

//public override bool ChkCharImmunityfromThis(CharacterController _charController)
//{
//    // character controller should stock what its immune too... 
//    return false;
//}
