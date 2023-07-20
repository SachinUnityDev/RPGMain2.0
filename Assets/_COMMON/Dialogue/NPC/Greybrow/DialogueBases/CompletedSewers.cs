using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{


    public class CompletedSewers : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.CompletedSewers; 

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true;
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            
        }
    }
}