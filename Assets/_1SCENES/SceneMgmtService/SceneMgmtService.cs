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


        //public IEnumerator LoadScene(SceneSeq sceneNames)
        //{
        //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNames.ToString(), LoadSceneMode.Additive);
        //    // wait until the asynchronous scene fully loads
        //    while (!asyncLoad.isDone)
        //        yield return null;

        //  //  Debug.Log("Main Menu scene loaded!");
        //    // MainMenu.instance.Initialize();// Init the scene loaded using switch case

        //}



        //if (cts == null)
        //{
        //    currentScene = SceneManager.GetActiveScene();
        //    newScene= SceneManager.GetSceneByName(sceneName);
        //    cts = new CancellationTokenSource();
        //    try
        //    {
        //        await PerformSceneLoading(cts.Token, sceneName);
        //    }
        //    catch (OperationCanceledException ex)
        //    {
        //        if (ex.CancellationToken == cts.Token)
        //        {
        //            //Perform operation after cancelling
        //            Debug.Log("Task cancelled");
        //        }
        //    }
        //    finally
        //    {
        //        cts.Cancel();
        //        cts = null;
        //    }
        //}
        //else
        //{
        //    //Cancel Previous token
        //    cts.Cancel();
        //    cts = null;
        //}
        //}
        //Actual Scene loading
        //private async Task PerformSceneLoading(CancellationToken token, string sceneName)
        //{
        //    token.ThrowIfCancellationRequested();
        //    if (token.IsCancellationRequested)
        //        return;

        //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        //    asyncOperation.allowSceneActivation = false;
        //   // await Task.Run(() => {
        //        while (true)
        //        {
        //            token.ThrowIfCancellationRequested();
        //            if (token.IsCancellationRequested)
        //                return;

        //            if (asyncOperation.progress >= 0.9f)
        //                break;
        //        }
        //        Debug.Log(" scene load completed" + sceneName); 

        //    //}); 


        //    asyncOperation.allowSceneActivation = true;
        //    cts.Cancel();
        //    token.ThrowIfCancellationRequested();

        //    //added this as a failsafe unnecessary
        //    if (token.IsCancellationRequested)
        //        return;
        //}


    }
}

//IEnumerator LoadingCoroutine(SceneSeq sceneName)
//{
//    AsyncOperation operation = SceneManager.LoadSceneAsync((int)sceneName, LoadSceneMode.Additive);

//    operation.allowSceneActivation = false;
//    while (!operation.isDone)
//    {
//        float progress = Mathf.Clamp01(operation.progress);
//        Debug.Log("Progress" + progress);
//        yield return null;
//        operation.allowSceneActivation = true;
//        SceneManager.SetActiveScene(scene);
//    }
//}