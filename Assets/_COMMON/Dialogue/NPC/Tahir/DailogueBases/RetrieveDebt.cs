using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class RetrieveDebt : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.RetrieveDebt;

        public bool ApplyChoices(int choiceIndex, float value)
        {
            Debug.Log("choice " + choiceIndex + ", " + value);  
            return false;
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
        
        }

        public void OnDialogueEnd()
        {
        
        }
    }
}