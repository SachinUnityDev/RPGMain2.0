using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 namespace Common
{
    public class NothingToLose : PermaTraitBase
    {
        //  immune to negative mental traits

        public override PermaTraitName permaTraitName => PermaTraitName.NothingToLose;

        public override void ApplyTrait()
        {
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                           , charID, TempTraitName.Thanatophobia, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Herpethophobia, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Ailurophobia, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Musophobia, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Hemophobia, TimeFrame.Infinity, 1);
            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Arachnophobia, TimeFrame.Infinity, 1);

            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.Claustrophobia, TimeFrame.Infinity, 1);

            charController.tempTraitController.ApplyImmunityBuff(CauseType.PermanentTrait, (int)permaTraitName
                                            , charID, TempTraitName.XXX, TimeFrame.Infinity, 1);
        }


    }
}

