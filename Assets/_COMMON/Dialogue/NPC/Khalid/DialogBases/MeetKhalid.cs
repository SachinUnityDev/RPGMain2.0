using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class MeetKhalid : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.MeetKhalid; 

        public void ApplyChoices(int choiceIndex, float value)
        {
            Debug.Log("Choice applied" + choiceIndex+ ", " + value);
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            Debug.Log("Interaction applied" + interactionNum + ", " + value);
        }

        public void OnDialogueEnd()
        {
            Debug.Log("DIALOGUES END");
        }
    }
}