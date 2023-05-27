using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Field : LandscapeBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Field;
        int buffID;
        protected override void OnLandscapeEnter(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return; 
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {  
                int charID = charCtrl.charModel.charID;
                buffID = 
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.airRes, -12, TimeFrame.Infinity, 1, false); 
            }
        }
        protected override void OnLandscapeExit(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return;
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                int charID = charCtrl.charModel.charID;
                charCtrl.buffController.RemoveBuff(buffID); 
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