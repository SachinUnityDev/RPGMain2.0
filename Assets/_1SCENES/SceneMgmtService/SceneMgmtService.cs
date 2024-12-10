using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Intro;
using Town;


namespace Common
{
    public class SceneMgmtService : MonoSingletonGeneric<SceneMgmtService>
    {
        public Action<GameScene> OnGameSceneLoaded;

        public SceneMgmtController sceneMgmtController;

        private void OnEnable()
        {
            sceneMgmtController = GetComponent<SceneMgmtController>();
        }
        public void LoadGameScene(GameScene gameScene)
        {
            UIControlServiceGeneral.Instance.CloseAllPanels();// clears Panel list
            sceneMgmtController.UpdateSceneCount(gameScene);
            switch (gameScene)
            {
                case GameScene.INTRO:
                    StartCoroutine(sceneMgmtController.LoadScene(GameScene.INTRO));
                    break;
                case GameScene.TOWN: 
                    StartCoroutine(sceneMgmtController.LoadScene(GameScene.TOWN));
                    break;
                case GameScene.QUEST:
                    StartCoroutine(sceneMgmtController.LoadScene(GameScene.QUEST));
                    break;    
                case GameScene.COMBAT:
                    StartCoroutine(sceneMgmtController.LoadScene(GameScene.COMBAT));
                    break;
                case GameScene.CORE:
                    break;
                case GameScene.CAMP:
                    break;
                case GameScene.JOBS:
                    break;
                case GameScene.MAPINTERACT:                    
                    StartCoroutine(sceneMgmtController.LoadScene(GameScene.TOWN));
                    SceneManager.activeSceneChanged += OpenMapE; 
                    break;
                default:
                    break;
            }
            
        }

        void OpenMapE(Scene oldScene, Scene newScene)
        {
            GameScene gameScene = sceneMgmtController.GetGameSceneNameFrmSceneName(newScene); 
            if (gameScene != GameScene.TOWN)            
                return;             
            MapService.Instance.mapView.GetComponent<IPanel>().Load();
            SceneManager.activeSceneChanged -= OpenMapE;
        }
    }
}

