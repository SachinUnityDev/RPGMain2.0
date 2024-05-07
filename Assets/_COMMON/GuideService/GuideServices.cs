using Combat;
using System.Collections;
using System.Collections.Generic;
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
            GameScene gameScene = GameService.Instance.currGameModel.gameScene;

            HelpName helpName = HelpName.None; 

            switch (gameScene)
            {
                case GameScene.None:
                    break;
                case GameScene.InTown:
                    helpName = HelpName.TownScreen; 
                    break;
                case GameScene.InQuestRoom:
                    helpName = HelpName.QuestPrep; // to be discussed
                    break;
                case GameScene.InCombat:
                    helpName = HelpName.Combat;
                    break;
                case GameScene.InCamp:
                    //helpName = HelpName;
                    break;
                case GameScene.InJobs:
                    break;
                case GameScene.InIntro:
                    break;
                case GameScene.InMapInteraction:
                                       
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