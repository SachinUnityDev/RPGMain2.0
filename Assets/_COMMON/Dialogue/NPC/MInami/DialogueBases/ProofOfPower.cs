using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class ProofOfPower : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.ProofOfPower; 

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