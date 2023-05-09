using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Field : LandscapeBase
    {
            public override LandscapeNames landscapeName => LandscapeNames.Field;
            public override void OnLandscapeInit()
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {  
                    int charID = charCtrl.charModel.charID;
                    charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                                , charID, AttribName.airRes, -12, TimeFrame.Infinity, 1, false); 
                }
            }
            public override void TrapNegative()
            {

            }

            public override void TrapPositive()
            {

            }
    }
}