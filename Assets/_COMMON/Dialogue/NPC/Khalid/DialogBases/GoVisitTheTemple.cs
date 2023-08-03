using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{


    public class GoVisitTheTemple : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.GoVisitTemple;

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