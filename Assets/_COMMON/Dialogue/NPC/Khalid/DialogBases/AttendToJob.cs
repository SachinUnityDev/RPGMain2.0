using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{


    public class AttendToJob : IDialogue
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

        }
    }
}