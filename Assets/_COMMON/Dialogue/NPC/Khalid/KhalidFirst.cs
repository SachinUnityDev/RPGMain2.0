using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common
{
    public class KhalidFirst : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.MeetKhalid;

        public void ApplyChoices(int choiceIndex, float value)
        {
            Debug.Log("APPLY CHOICES " + choiceIndex); 
        }
        public void ApplyInteraction(int interactionNum, float value)        
        {
            Debug.Log("APPLY INT " + interactionNum);
        }
        public void OnDialogueEnd()
        {
            Debug.Log("APPLY ON DIALOGUE END");
        }
    }
}
