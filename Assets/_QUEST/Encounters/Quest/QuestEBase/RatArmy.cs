using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Quest
{
    public class RatArmy : QuestEbase
    {
        public override QuestENames questEName => QuestENames.RatArmy;
        public override void QuestEInit(QuestEModel questEModel)
        {
            this.questEModel = questEModel;
            questMode = QuestMissionService.Instance.currQuestMode;
            choiceAStr = "Fight them";
            switch (questMode)
            {
                case QuestMode.None:
                    break;
                case QuestMode.Stealth:
                    choiceBStr = " Try to escape";
                    break;
                case QuestMode.Exploration:
                    choiceBStr = "Just walk past";
                    break;
                case QuestMode.Taunt:
                    choiceBStr = "Unleash a war cry";
                    break;
                default:
                    break;
            }
        }
        #region A
        public override void OnChoiceASelect()
        {
           
            //Start combat vs Rat army pack(Your party Flat footed for 3 rds)
            resultStr = "You were caught off guard by their sheer numbers." +
                        " Now it's too late to turn back. You have only one way out: Fight!";
            buffstr =  "Party debuff: Flatfooted, 3 rds";
        }
        #endregion

        #region B
        public override void OnChoiceB_Stealth()
        {
           
            //50% Debuff party: -3 Fort Orign eoq	50% Fight vs Rat pack 2
            float chance = 50f;
            if (chance.GetChance())
            {
                foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
                {
                    c.buffController.ApplyBuff(CauseType.QuestEncounter, (int)questEName,
                        -1, AttribName.fortOrg, -3, TimeFrame.EndOfQuest, 1, false); 
                }
                resultStr = "You kicked a couple of them out of the way and made a run for it. Despite the overwhelming odds," +
                            " you resolved to muddle through the chaos and find a way out." +
                            " However, it was a terrifying experience.";
                buffstr = "Party debuff: -3 Fort Org eoq";
            }
            else
            {
                // Fight vs Rat pack 2
                resultStr = "You fought off a couple of them" +
                            ", but it seems there is no choice but to continue fighting your way through.";

                buffstr = "";
            }
        }
        public override void OnChoiceB_Exploration()
        {
           
            //20% Party Buff: +1 Willpower eoq	80% Fight vs Rat army pack
            float chance = 20f;
            if (chance.GetChance())
            {
                foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
                {
                    c.buffController.ApplyBuff(CauseType.QuestEncounter, (int)questEName,
                        -1, AttribName.willpower, +1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "That was a smart move to act calm and move on as if nothing had happened." +
                            " Your nerves were being tested, and you have done well.";
                buffstr = "Party buff: +1 Wp eoq";
            }
            else
            {
                // Fight vs Rat army pack
                resultStr = "As you tried to walk past, they gathered in front of you and blocked your way." +
                            "There is only one way out!";
                buffstr = "";
            }
        }
        public override void OnChoiceB_Taunt()
        {
         
            //Start combat vs Rat army pack(Apply debuff on enemy party: Flat footed for 3 rds)

            resultStr = "You unleashed a loud war cry, staggering the rats." +
                        " Their numbers might be high, but now they know who they are facing!";
            buffstr = "Enemy party debuff: Flatfooted, 3 rds";

        }
        #endregion
        public override void QuestEContinuePressed()
        {
            
        }

   
    }
}