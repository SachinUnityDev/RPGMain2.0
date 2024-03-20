using Quest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class ProofOfPower : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.ProofOfPower; 

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true; 
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            QuestMissionService.Instance.On_ObjEnd(QuestNames.ThePowerWithin, ObjNames.GoBackToSoothsayer);
            // to INIT START OF NEW QUEST "A PLACE OF EVIL" 
        }
    }
}