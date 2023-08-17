using DG.Tweening;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;



namespace Common
{
    public class AttendJob : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.AttendJob; 

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true; 
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            QuestMissionService.Instance.OnQuestEnd -= ShowWelcomeBoxN;
            QuestMissionService.Instance.OnQuestEnd += ShowWelcomeBoxN;
            QuestMissionService.Instance.On_QuestEnd(QuestNames.LostMemory);
        }
        void ShowWelcomeBoxN(QuestNames questName)
        {
            WelcomeService.Instance.InitWelcomeNormal();
            QuestMissionService.Instance.OnQuestEnd -= ShowWelcomeBoxN;
        }
    }
}