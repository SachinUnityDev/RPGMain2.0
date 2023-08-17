using Common;
using Interactables;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services.Description;
using UnityEngine;


namespace Town
{
    public class WelcomeService : MonoSingletonGeneric<WelcomeService>
    {
        
        public WelcomeController welcomeController;
        [Header("TBR")]
        public WelcomeView welcomeView;
        [Header("NTBR")]
        [SerializeField]GameObject cornerBtns;

        public bool isWelcomeRun = false;
        [SerializeField] int welcomeRunEndDay; 

        void Start()
        {
            
        }

        public void InitWelcome()
        {
            isWelcomeRun = true;
            welcomeController = GetComponent<WelcomeController>();
            cornerBtns = GameObject.FindGameObjectWithTag("TownBtns");
            cornerBtns.SetActive(false);
            welcomeView.InitWelcomeView();           
        }
        public void InitWelcomeNormal()
        {
            isWelcomeRun = false;
            welcomeController = GetComponent<WelcomeController>();
          
            // town btns unlocked
            cornerBtns.SetActive(true);
            welcomeView.InitWelcomeView();

           
            BuildingIntService.Instance.ChgCharState(BuildingNames.Tavern, CharNames.Cahyo, NPCState.UnLockedNAvail);

            CalendarService.Instance.OnStartOfCalDay -= GoVisitTemple2dayGap;
            CalendarService.Instance.OnStartOfCalDay += GoVisitTemple2dayGap;
            welcomeRunEndDay = CalendarService.Instance.dayInGame;

            //            Interactions unlocked:
            //House
            //endday, provision, chest, cure sickness

            //Tavern
            //trophy, buy service, bounties, endday

            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.EndDay, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.Provision, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.Chest, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.CureSickness, true);
            BuildingIntService.Instance.houseController.UnLockBuildIntType(BuildInteractType.Purchase, true);

            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.Trophy, true);
            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.BuyDrink, true);
            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.Bounty, true);
            BuildingIntService.Instance.tavernController.UnLockBuildIntType(BuildInteractType.EndDay, true);            
        }

        void GoVisitTemple2dayGap(int day)
        {
            if(day >= welcomeRunEndDay)
            {
                BuildingIntService.Instance
                    .UnLockDiaInt(BuildingNames.House, NPCNames.Khalid, DialogueNames.GoVisitTemple, true);

                CalendarService.Instance.OnStartOfCalDay -= GoVisitTemple2dayGap;
            }
        }


    }
}