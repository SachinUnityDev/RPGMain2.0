using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;


namespace Common
{


    public class Sewers : LandscapeBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Sewers;
      //	-1 morale
        public override void OnLandscapeInit()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInParty)
            {
                int charID = charCtrl.charModel.charID;
                charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)LandscapeNames.Field
                            , charID, AttribName.morale, -1, TimeFrame.Infinity, 1, false);
            }
        }



        //   "%40 start next combat with High poison, 
        //   %60 Nausea"	
        public override void TrapNegative()
        {
          
        }

        //"%70 +2 morale until eoq
       
        public override void TrapPositive()
        {
            
        }
    }
}