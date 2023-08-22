using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Common
{
    public class MeetGreybrow : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.MeetGreybrow; 

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true; 
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            // unLock tahir
            BuildingIntService.Instance.ChgNPCState(BuildingNames.Tavern, NPCNames.Tahir, NPCState.UnLockedNAvail, true);
            WelcomeService.Instance.welcomeView.RevealWelcomeTxt("Talk to Tahir the Mirrorman");
        }
    }
}