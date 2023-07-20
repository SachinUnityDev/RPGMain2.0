using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class MeetCaptain : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.MeetCaptain; 

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