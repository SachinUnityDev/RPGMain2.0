using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public interface IDialogue
    {
        public DialogueNames dialogueNames { get;  }
        public void ApplyChoices(int choiceIndex, float value);
        public void ApplyInteraction(int interactionNum, float value);

        public void OnDialogueEnd(); 
    }
}
