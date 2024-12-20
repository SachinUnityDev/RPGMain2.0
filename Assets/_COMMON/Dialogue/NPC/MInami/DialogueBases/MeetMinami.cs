using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class MeetMinami : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.MeetMinami;

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true; 
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
            CharService.Instance.UnLockChar(CharNames.Rayyan);
            QuestMissionService.Instance.On_ObjEnd(QuestNames.ThePowerWithin, ObjNames.VisitTemple);
            BuildingIntService.Instance.ChgCharState(BuildingNames.Temple, CharNames.Rayyan
                                                                        , NPCState.UnLockedNAvail, true);
            BuildingIntService.Instance
                 .UnLockDiaInBuildChar(BuildingNames.Temple, CharNames.Rayyan, DialogueNames.MeetRayyan, true);
            
        }
    }
}