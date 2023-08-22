using Quest;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
namespace Common
{


    public class DebtIsClear : IDialogue
    {
        public DialogueNames dialogueName => DialogueNames.DebtIsClear; 

        public bool ApplyChoices(int choiceIndex, float value)
        {
            return true;
        }

        public void ApplyInteraction(int interactionNum, float value)
        {
            
        }

        public void OnDialogueEnd()
        {
          
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, true);
            CalendarService.Instance.OnChangeTimeState -= LockAgainOnDayEnd_Debt;
            CalendarService.Instance.OnChangeTimeState += LockAgainOnDayEnd_Debt;
            BuildingIntService.Instance.OnBuildUnload -= StartJob;
            BuildingIntService.Instance.OnBuildUnload += StartJob;
            BuildingIntService.Instance.ChgNPCState(BuildingNames.Tavern, NPCNames.Tahir, NPCState.Locked, false);
            QuestMissionService.Instance.On_ObjStart(QuestNames.LostMemory, ObjNames.AttendToJob);
            WelcomeService.Instance.welcomeView.RevealWelcomeTxt("End Day by clicking the button on bottom right");
        }

        void StartJob(BuildingModel buildModel, BuildView buildView)
        {
            if(buildModel.buildingName == BuildingNames.House)
            {
                JobService.Instance.StartJob(JobNames.WoodCutting); 
                BuildingIntService.Instance.OnBuildUnload-= StartJob;   
            }
        }
        void LockAgainOnDayEnd_Debt(TimeState timeState)
        {
            if(BuildingIntService.Instance.buildName== BuildingNames.House) 
                WelcomeService.Instance.welcomeView.RevealWelcomeTxt("Exit the House to attend to your job");
            
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, false);
            CalendarService.Instance.OnChangeTimeState -= LockAgainOnDayEnd_Debt;
        }
    }
}