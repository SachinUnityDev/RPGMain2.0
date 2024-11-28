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
                case GameScene.TOWN:
                    helpName = HelpName.TownScreen; 
                    break;
                case GameScene.QUEST:
                    helpName = HelpName.QuestPrep; // to be discussed
                    break;
                case GameScene.COMBAT:
                    helpName = HelpName.Combat;
                    break;
                case GameScene.CAMP:
                    //helpName = HelpName;
                    break;
                case GameScene.JOBS:
                    break;
                case GameScene.INTRO:
                    break;
                case GameScene.MAPINTERACT:
                                       
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