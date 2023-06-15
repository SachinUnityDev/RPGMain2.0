using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace Quest
{


    public class Fountain : CurioBase
    {
        public override CurioNames curioName => CurioNames.Fountain;
        public override void InitCurio()
        {

        }
        public override void CurioInteractWithoutTool()
        {
            // % 30 Fort buff
            // % 25 Stamina buff
            // % 25 Health buff
            // % 20 dirty water debuff
            List<float> chances = new List<float>() { 30f,25f, 25f, 20f};

            switch (chances.GetChanceFrmList())
            {
                case 0:
                    Fx1();break; 
                case 1:
                    Fx2(); break;
                case 2:
                    Fx3(); break;
                case 3:
                    Fx4(); break;
                default:
                    break;
            }
        }

        //3-6 Fort Origin until eoq   Full Thirst relief	+1 Fort Origin Permanently
        void Fx1()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                charCtrl.buffController.ApplyBuff(CauseType.Curios, (int)curioName
                                              , charCtrl.charModel.charID, AttribName.fortOrg
                                              , UnityEngine.Random.Range(3, 7), TimeFrame.EndOfQuest, 1, true);

                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                   AttribName.fortOrg, 1);
            }
            resultStr = "Only the bravest could drink this.";
            resultStr2 = "Fort Org buff, eoq\nFull Thirst relief\n+1 Fort Org";
        }
        //Recover full stamina, Full Thirst relief,	+1 Willpower Permanently
        void Fx2()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                    AttribName.willpower, 1);

            }
            resultStr = "Feels refreshing.";
            resultStr2 = "Full Stamina gain\nFull Thirst Relief\n+1 Willpower";
        }
        //Recover full health Full Thirst relief	+1 Vigor Permanently
        void Fx3()
        {
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                charCtrl.ChangeAttrib(CauseType.Curios, (int)curioName, charCtrl.charModel.charID,
                                    AttribName.vigor, 1);
            }
            resultStr = "Feels replenishing.";
            resultStr2 = "Full Health gain\nFull Thirst Relief\n+1 Vigor";
        }

        //Receive Diarrhea %50 or Nausea    %80	Receive Low Poisoned     Gain Sickness Immunity until eoq
        void Fx4()
        {
            float chance = 50f;
            TempTraitName tempTrait = TempTraitName.None;
            if (chance.GetChance())
                tempTrait = TempTraitName.Diarrhea; 
            else
                tempTrait = TempTraitName.Nausea;
            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                charCtrl.tempTraitController.ApplyTempTrait(CauseType.Curios, (int)curioName
                                                , charCtrl.charModel.charID, tempTrait);
            }

            chance = 80f;
            if (chance.GetChance())
            {
                foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
                {
                    charCtrl.charStateController.ApplyCharStateBuff(CauseType.Curios, (int)curioName
                                                    , charCtrl.charModel.charID, CharStateName.PoisonedLowDOT);
                }
            }  

            foreach (CharController charCtrl in CharService.Instance.allCharsInPartyLocked)
            {
                charCtrl.tempTraitController.ApplyTraitTypeImmunityBuff(CauseType.Curios, (int)curioName,
                charCtrl.charModel.charID, TempTraitType.Sickness, TimeFrame.EndOfQuest, 1); 
            }
            resultStr = "Dirty water makes you feel like vomit.";
            resultStr2 = "Sickness gained\nPoison chance\nSickness immunity, eoq";
        }
            
        public override void CurioInteractWithTool()
        {
            //% 30 Fort buff
            //% 35 stamina
            //% 35 Health
            List<float> chances = new List<float>() { 30f, 35f, 35f };

            switch (chances.GetChanceFrmList())
            {
                case 0:
                    Fx1(); break;
                case 1:
                    Fx2(); break;
                case 2:
                    Fx3(); break;               
                default:
                    break;
            }
        }
    }
}