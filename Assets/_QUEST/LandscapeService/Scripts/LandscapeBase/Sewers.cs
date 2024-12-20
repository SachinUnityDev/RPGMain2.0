using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class Sewers : LandscapeBase
    {
        public override LandscapeNames landscapeName => LandscapeNames.Sewers;
      //	-1 morale
        protected override void OnLandscapeEnter(LandscapeNames landscapeName)
        {
            if (this.landscapeName != landscapeName) return;
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                int charID = charCtrl.charModel.charID;
                charCtrl.landscapeController.ApplyLandscapeBuff(CauseType.Landscape, (int)LandscapeNames.Sewers
                            , LandscapeNames.Sewers, AttribName.morale, -1);
            }
        }
        //   "%40 High poison, 
        //   %60 Nausea"	
        public override void TrapNegative()
        {
            float chance = 40f; 
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                if (chance.GetChance())
                {
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Landscape, (int)landscapeName
                    , charCtrl.charModel.charID, CharStateName.Poisoned, TimeFrame.EndOfRound, 6); 
                }
                else
                {
                    charCtrl.tempTraitController.ApplyTempTrait(CauseType.Landscape
                                             , (int)landscapeName, 1, TempTraitName.Nausea); 
                }
            }
        }
        //"%70 +2 morale until eoq
        public override void TrapPositive()
        {
            float chance = 70f;
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                if (chance.GetChance())
                {
                    charCtrl.buffController.ApplyBuff(CauseType.Landscape, (int)landscapeName
                                            , 1, AttribName.morale, +2, TimeFrame.EndOfQuest, 1, true); 
                }
            }
        }

        protected override void OnLandscapeExit(LandscapeNames landscapeName)
        {
         
        }
    }
}