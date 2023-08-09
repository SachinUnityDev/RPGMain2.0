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
            WelcomeService.Instance.welcomeView.RevealWelcomeTxt("End Day by clicking the button on bottom right");
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, true);
            CalendarService.Instance.OnChangeTimeState += (TimeState timeState) => LockAgainOnDayEnd_Debt();
            BuildingIntService.Instance.OnBuildUnload += StartJob; 
        }

        void StartJob(BuildingModel buildModel, BuildView buildView)
        {
            if(buildModel.buildingName == BuildingNames.House)
            {
                JobService.Instance.StartJob(JobNames.WoodCutting); 
                BuildingIntService.Instance.OnBuildUnload-= StartJob;   
            }
        }
        void LockAgainOnDayEnd_Debt()
        {
            WelcomeService.Instance.welcomeView.RevealWelcomeTxt("Exit the House to attend to your job");
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, false);
            CalendarService.Instance.OnChangeTimeState -= (TimeState timeState) => LockAgainOnDayEnd_Debt();
        }
    }
}