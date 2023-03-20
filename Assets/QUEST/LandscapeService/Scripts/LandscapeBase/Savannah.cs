using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Savannah : LandscapeBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Savannah;
        //-6 Fort Origin  .. to be controller is landscape controller
        public override void OnLandscapeInit()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInParty)
            { 
                int charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, StatsName.fortOrg, -6, TimeFrame.Infinity, 1, false);
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