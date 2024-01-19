using System.Collections;
using System.Collections.Generic;
using UnityEngine;



 namespace Common
{
    public class Frail : TempTraitBase
    {
        public override TempTraitName tempTraitName => TempTraitName.Frail;

        public override void OnApply()
        {   
           int buffID =  charController.buffController.ApplyBuff(CauseType.TempTrait, (int)tempTraitName,
                                                         charID, AttribName.vigor, -3, TimeFrame.Infinity, -1, false);
            allBuffIds.Add(buffID); 
        }
        public override void OnEndConvert()
        {
            base.OnEndConvert();
            List<float> chances = new List<float>() { 30f, 30f, 40f };

            switch (chances.GetChanceFrmList())
            {
                case 0:
                    charController.tempTraitController
                        .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.Weakdraw);
                    break;
                case 1:
                    charController.tempTraitController
                        .ApplyTempTrait(CauseType.TempTrait, (int)tempTraitName, charID, TempTraitName.WeakGrip);
                    break;
                case 2:
                    
                    break;
                default:

                    break;
            }

        }
    }


}

