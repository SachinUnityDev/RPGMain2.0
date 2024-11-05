using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Intro;


namespace Common
{
    public class SceneMgmtService : MonoSingletonGeneric<SceneMgmtService>
    {
        //    private CancellationTokenSource cts;

        //public Scene currScene;
        //public Scene newScene;
        public SceneMgmtController sceneMgmtController;

        private void OnEnable()
        {
            sceneMgmtController = GetComponent<SceneMgmtController>();
        }

        public void LoadGameScene(GameScene gameScene)
        {
            switch (gameScene)
            {
                case GameScene.None:
                    Debug.LogError(" None Scene request Send"); 
                    break;
                case GameScene.InIntro:
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
                    break;
                default:
                    break;
            }

        }


    }
}

