using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quest
{
    public class SpidaBoy : QuestEbase
    {
        public override QuestENames questEName => QuestENames.Spidaboy;

        public override void QuestEInit(QuestEModel questEModel)
        {
            this.questEModel = questEModel;
            questMode = QuestMissionService.Instance.currQuestMode;
            choiceAStr = "Join the fray";
            switch (questMode)
            {
                case QuestMode.None:
                    break;
                case QuestMode.Stealth:
                    choiceBStr = "Sneak past";
                    break;
                case QuestMode.Exploration:
                    choiceBStr = "Watch the fight";
                    break;
                case QuestMode.Taunt:
                    choiceBStr = "Startle the rats";
                    break;
                default:
                    break;
            }
        }

        public override void OnChoiceASelect()
        {
           
            //"Start combat vs Rat pack 2 (Spider Lvl2 is in your party)
            //If fightEnd positive, meetDialog starts ....If else, nothing"
            resultStr = "You couldn't bear to watch the poor spider being beaten" +
                        "and darted forward to protect it from the rats.";

            buffstr = "";

        }
        public override void OnChoiceB_Stealth()
        {          

            float chance = 60f;
            if (chance.GetChance())
            {
                foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
                {
                    c.buffController.ApplyBuff(CauseType.QuestEncounter, (int)questEName,
                        -1, AttribName.luck, 1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "You didn't care about the outcome of the fight and moved on." +
                            "No one noticed you as you walked past.";
                buffstr = "Party buff: +1 Luck eoq"; 
            }
            else
            {
                foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
                {
                    c.buffController.ApplyBuff(CauseType.QuestEncounter, (int)questEName,
                        -1, AttribName.luck, -1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "Not standing against cruelty is, indeed, a form of cruelty." +
                             " The soul of the fallen spider will haunt you throughout this quest.";
                buffstr = "Party debuff: -1 Luck eoq";
            }
        }
        public override void OnChoiceB_Exploration()
        {            
            float chance = 60f;
            if (chance.GetChance())
            {
                foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
                {
                    c.buffController.ApplyBuff(CauseType.QuestEncounter, (int)questEName,
                        -1, AttribName.focus, 1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "You watched the fight with full concentration. It was a decent display." +
                            "The rats stared at you but went on their way, continuing in their own glorious manner.";
                buffstr = "Party buff: +1 Focus eoq";
            }
            else
            {
                foreach (CharController c in CharService.Instance.allCharsInPartyLocked)
                {
                    c.buffController.ApplyBuff(CauseType.QuestEncounter, (int)questEName,
                        -1, AttribName.morale, -1, TimeFrame.EndOfQuest, 1, false);
                }
                resultStr = "The rats killed the spider and tore its body into pieces." +
                        " One of them even used a broken spider leg to pick its teeth.You feel demoralized.";
                buffstr = "Party debuff: -1 Morale eoq";
            }
        }
        public override void OnChoiceB_Taunt()
        {         
            //"meetSpider dialog starts
            //After that Spider joins your party"

            resultStr = "You taunted the rats and watched them back off in a terrified manner." +
                        " Spida is thankful – it's obvious from his smiling face." +
                        " He gives you a meaningful stare, as if he wants to say something.";
            buffstr = "Spida joined your party";
        }

        public override void QuestEContinuePressed()
        {
            
        }

   
    }
}