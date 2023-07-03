using Combat;
using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


namespace Common
{

    public class GuideServices : MonoSingletonGeneric<GuideServices>
    {
        public ExclaimHLController exclaimHLController;
        public HelpController helpController;
        private void Start()
        {
            exclaimHLController = GetComponent<ExclaimHLController>();
            helpController = GetComponent<HelpController>();
        }
        public HelpName GetDefaultHelp()
        {
            GameState gameState = GameService.Instance.gameModel.gameState;

            HelpName helpName = HelpName.None; 

            switch (gameState)
            {
                case GameState.None:
                    break;
                case GameState.InTown:
                    helpName = HelpName.TownScreen; 
                    break;
                case GameState.InQuest:
                    helpName = HelpName.QuestPrep; // to be discussed
                    break;
                case GameState.InCombat:
                    helpName = HelpName.Combat;
                    break;
                case GameState.InCamp:
                    //helpName = HelpName;
                    break;
                case GameState.InJobs:
                    break;
                case GameState.InIntro:
                    break;
                case GameState.InMapInteraction:
                                       
                    break;
                default:
                    break;
            }
            return helpName;

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                //exclaimHLController.exclaimHLView.ShowGuideMarkInSeq(GuideMarkLoc.QuestJurnoBtn
                //    , GuideMarkLoc.MapBtn);                

                helpController.ShowHelp();
            }
        }
    }
}