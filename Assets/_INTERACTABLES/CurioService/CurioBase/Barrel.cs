using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactables; 

namespace Quest
{


    public class Barrel : CurioBase
    {
        public override CurioNames curioName => CurioNames.Barrel;

        //        "Empty barrel, waste of time."
        //"Some like it hot!"
        //"The night is dark and full of barrels!"
        public override void InitCurio()
        {

        }
        public override void CurioInteractWithoutTool()
        {
            //            20 % nothing happens
            //45 % buff 1
            //35 % buff 2
            List<float> chances = new List<float>() { 20f, 45f, 35f }; 
            
            if (chances.GetChanceFrmList() ==0 ) 
            {
                /// nothing happens
                resultStr = "Empty barrel, waste of time.";
            }
            else if (chances.GetChanceFrmList() == 1)
            {
                Fx1();                
            }
            else
            {
                Fx2();                
            }
        }

        public override void CurioInteractWithTool()
        {
            float chance = 50f;
            if (chance.GetChance())
            {
                Fx1();
            }
            else
            {
                Fx2();
            }
        }


        //+1-2 light res, +1-3 Fire+Air res permanently	-12-24 Thirst	-1-2 Willpower until eoq
        void Fx1()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
              
                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                    AttribName.lightRes, UnityEngine.Random.Range(1,3));

                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                   AttribName.fireRes, UnityEngine.Random.Range(1, 3));

                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                   AttribName.airRes, UnityEngine.Random.Range(1, 3));

                charCtrl.buffController.ApplyBuff(CauseType.Curios, (int)curioName
                                              , charCtrl.charModel.charID, AttribName.willpower
                                              , UnityEngine.Random.Range(-1,-3), TimeFrame.EndOfQuest,1,false);

            }
            resultStr = "Some like it hot!";
        }
        //+1-2 dark res, +1-3 Water+Earth res permanently	-12-24 Thirst	-1-2 Willpower until eoq
        void Fx2()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {

                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                    AttribName.darkRes, UnityEngine.Random.Range(1, 3));

                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                   AttribName.waterRes, UnityEngine.Random.Range(1, 3));

                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                   AttribName.earthRes, UnityEngine.Random.Range(1, 3));

                charCtrl.buffController.ApplyBuff(CauseType.Curios, (int)curioName
                                              , charCtrl.charModel.charID, AttribName.willpower
                                              , UnityEngine.Random.Range(-1, -3), TimeFrame.EndOfQuest, 1, false);

            }
            resultStr = "The night is dark and full of barrels!";
        }
  
    }
}