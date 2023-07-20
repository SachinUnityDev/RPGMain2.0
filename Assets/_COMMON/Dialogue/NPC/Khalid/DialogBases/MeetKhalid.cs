using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class MeetKhalid : IDialogue
    {
        public DialogueNames dialogueNames => DialogueNames.MeetKhalid; 

        public bool ApplyChoices(int choiceIndex, float value)
        {
            Debug.Log("Choice applied" + choiceIndex + ", " + value);
            switch (choiceIndex)
            {
                case 1:
                    return false; 
                    
                case 2:
                    // get abbas char Set class to Abbas the Skirmisher
                    SetAbbasClass();
                    return true;
                    
                case 3:
                    return false;
                    
                default:
                    break;
            }
            return false;
        }

        void SetAbbasClass()
        {
            CharController charController = CharService.Instance
                                            .GetCharCtrlWithName(CharNames.Abbas);

            CharModel charModel = charController.charModel;
            charModel.classType = ClassType.Skirmisher; 
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