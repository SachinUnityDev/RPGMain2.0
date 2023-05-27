using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Savannah : LandscapeBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Savannah;
        //-6 Fort Origin  .. to be controller is landscape controller
        protected List<int> allbuffID { get; set; } = new List<int>(); 
        protected override void OnLandscapeEnter(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return;
            int buffID = -1; 
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            { 
                int charID = charCtrl.charModel.charID;
                buffID =
                        charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.fortOrg, -6, TimeFrame.Infinity, 1, false);
                allbuffID.Add(buffID);
            }
        }

        protected override void OnLandscapeExit(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return;
            


        }
        public override void TrapNegative()
        {

        }

        public override void TrapPositive()
        {

        }
    }
}