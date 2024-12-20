using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;


namespace Common
{
    public class MeetKhalid : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.MeetKhalid; 

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
                                            .GetAllyController(CharNames.Abbas);        
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
            QuestMissionService.Instance.On_ObjEnd(QuestNames.LostMemory, ObjNames.TalkToKhalid);
            

            WelcomeService.Instance.welcomeView.RevealWelcomeTxt("End Day by clicking the button on bottom right");
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, true);
            CalendarService.Instance.OnChangeTimeState -= LockAgainOnDayEnd;
            CalendarService.Instance.OnChangeTimeState += LockAgainOnDayEnd;
            BuildingIntService.Instance.UnLockABuild(BuildingNames.Tavern, true); 
        }
        void LockAgainOnDayEnd(TimeState timeState)
        {
            string str = "Exit the House and enter the Tavern";
            WelcomeService.Instance.welcomeView.RevealWelcomeTxt(str);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, false);
            CalendarService.Instance.OnChangeTimeState -= LockAgainOnDayEnd;
            str = "";
        }

    
    }
}