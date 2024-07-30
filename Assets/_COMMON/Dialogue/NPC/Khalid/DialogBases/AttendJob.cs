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
            Sequence seq = DOTween.Sequence();
            seq
                .AppendCallback(() => QuestMissionService.Instance.On_ObjEnd(QuestNames.LostMemory, ObjNames.AttendToJob))
               .AppendInterval(4f)
               .AppendCallback(() => QuestMissionService.Instance.On_QuestStart(QuestNames.ThePowerWithin))
               .AppendInterval(4f)
               .AppendCallback(() => ShowWelcomeBoxN(QuestNames.LostMemory))
                ;
            seq.Play();            
            BuildingIntService.Instance.UnLockABuild(BuildingNames.Marketplace, true);
        }
        void ShowWelcomeBoxN(QuestNames questName)
        {
            WelcomeService.Instance.InitWelcomeComplete();
        }
    }
}