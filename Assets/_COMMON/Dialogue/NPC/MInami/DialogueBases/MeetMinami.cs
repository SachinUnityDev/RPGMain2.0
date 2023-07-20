using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class MeetMinami : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.MeetMinami;

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