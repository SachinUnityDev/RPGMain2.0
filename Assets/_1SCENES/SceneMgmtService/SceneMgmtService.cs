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
            switch (gameScene)
            {
                case GameScene.None:
                    Debug.LogError(" None Scene request Send"); 
                    break;
                case GameScene.INTRO:
                    StartCoroutine(sceneMgmtController.LoadScene(SceneName.INTRO));
                    break;
                case GameScene.InTown: 
                    StartCoroutine(sceneMgmtController.LoadScene(SceneName.TOWN));
                    break;
                case GameScene.InQuestRoom:
                    StartCoroutine(sceneMgmtController.LoadScene(SceneName.QUEST));
                    break;
                case GameScene.InCombat:
                    StartCoroutine(sceneMgmtController.LoadScene(SceneName.COMBAT));
                    break;
                case GameScene.InCore:
                    break;
                case GameScene.InCamp:
                    break;
                case GameScene.InJobs:
                    break;
                case GameScene.InMapInteraction:                    
                    StartCoroutine(sceneMgmtController.LoadScene(SceneName.TOWN));
                    SceneManager.activeSceneChanged += OpenMapE; 
                    break;
                default:
                    break;
            }

        }

        void OpenMapE(Scene oldScene, Scene newScene)
        {
            GameScene gameScene = sceneMgmtController.GetGameSceneNameFrmSceneName(newScene); 
            if (gameScene != GameScene.InTown)            
                return;             
            MapService.Instance.mapView.GetComponent<IPanel>().Load();
            SceneManager.activeSceneChanged -= OpenMapE;
        }


    }
}

