using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;

namespace Common
{
    public class RetrieveDebt : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.RetrieveDebt;

        public bool ApplyChoices(int choiceIndex, float value)
        {
            Debug.Log("choice " + choiceIndex + ", " + value);  
            return false;
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
        
        }

        public void OnDialogueEnd()
        {
            BuildingIntService.Instance.UnLockDiaInBuildNPC(BuildingNames.House, NPCNames.Khalid, DialogueNames.DebtIsClear, true);
            QuestMissionService.Instance.On_ObjStart(QuestNames.LostMemory, ObjNames.GoBackToKhalid);
        }
    }
}