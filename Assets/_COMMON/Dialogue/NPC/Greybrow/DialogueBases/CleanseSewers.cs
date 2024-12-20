using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CleanseSewers : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.CleanseSewers;

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return false; 
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            CharService.Instance.UnLockChar(CharNames.Cahyo);
            QuestMissionService.Instance.On_QuestStart(QuestNames.RatInfestation);

        }
    }
}