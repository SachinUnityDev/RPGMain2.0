using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class Coast : LandscapeBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Coast; 
        //-12 Cold Res  .. water earth and dark/// landscape debuff
        protected  override void OnLandscapeEnter(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return;
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                // change stat using buff controller
                int charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.waterRes, -12, TimeFrame.Infinity, 1, false);

                charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.earthRes, -12, TimeFrame.Infinity, 1, false);

                charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.darkRes, -12, TimeFrame.Infinity, 1, false);

            }
        }
        public override void TrapNegative()
        {
            
        }
        public override void TrapPositive()
        {
            
        }

        protected override void OnLandscapeExit(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return;
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                // change stat using buff controller
                int charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.waterRes, -12, TimeFrame.Infinity, 1, false);

                charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.earthRes, -12, TimeFrame.Infinity, 1, false);

                charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.darkRes, -12, TimeFrame.Infinity, 1, false);

            }
        }
    }
}