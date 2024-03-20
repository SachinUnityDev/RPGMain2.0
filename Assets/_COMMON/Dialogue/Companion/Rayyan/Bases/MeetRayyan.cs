using Common;
using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Common
{
    public class MeetRayyan : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.MeetRayyan;

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true; 
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            QuestMissionService.Instance.On_ObjEnd(QuestNames.ThePowerWithin, ObjNames.RequestRayyan);            
        }
    }
}