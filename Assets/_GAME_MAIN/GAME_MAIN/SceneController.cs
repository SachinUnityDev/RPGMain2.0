using Intro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] Scene currScene;
        [SerializeField] Scene lastScene;
        [SerializeField] GameScene currGameScene;
        void Start()
        {

        }
        public void LoadScene(SceneSeq sceneSeq)
        {

            SceneMgmtController sceneMgmtController = FindObjectOfType<SceneMgmtController>();
            sceneMgmtController.StartSceneTransit();

            SceneManager.LoadSceneAsync((int)sceneSeq);

            //lastScene = currScene; 
            //currGameScene= newScene;
            //lastScene = SceneManager.GetActiveScene();
            //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)newScene, LoadSceneMode.Additive);
            //asyncLoad.allowSceneActivation = false;
            //StartCoroutine(LoadNewScene(newScene, asyncLoad));    
        }


        //IEnumerator LoadNewScene(GameScene newScene, AsyncOperation asyncLoad)
        //{         
   
            
        //    while (asyncLoad.progress < 0.9)
        //    {
        //        Debug.Log("Progress" + asyncLoad.progress); 
        //        yield return null;
        //    }
        //    if (asyncLoad.isDone)
        //    {
        //        asyncLoad.allowSceneActivation = true;
        //        currScene = SceneManager.GetSceneByBuildIndex((int)newScene);
        //        SceneManager.SetActiveScene(currScene);
        //        //GameEventService.Instance.On_TownEnter(LocationName.Nekkisari);
        //        //GameService.Instance.
        //        //    GameServiceInit(GameState.InTown, GameDifficulty.Easy, LocationName.Nekkisari);
        //        StartCoroutine(UnloadAsyncOperation());
        //    }
        //}

        IEnumerator UnloadAsyncOperation()
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(lastScene);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }
    }


}