using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Common
{
    public class SceneMgmtService : MonoSingletonGeneric<SceneMgmtService>
    {
        private CancellationTokenSource cts;

        public Scene currentScene;
        public Scene newScene;
        public SceneMgmtController   sceneMgmtController;

        private void Awake()
        {
          sceneMgmtController = GetComponent<SceneMgmtController>();      
        }

        public async void LoadNewScene(string sceneName)
        {
            if (cts == null)
            {
                currentScene = SceneManager.GetActiveScene();
                newScene= SceneManager.GetSceneByName(sceneName);
                cts = new CancellationTokenSource();
                try
                {
                    await PerformSceneLoading(cts.Token, sceneName);
                }
                catch (OperationCanceledException ex)
                {
                    if (ex.CancellationToken == cts.Token)
                    {
                        //Perform operation after cancelling
                        Debug.Log("Task cancelled");
                    }
                }
                finally
                {
                    cts.Cancel();
                    cts = null;
                }
            }
            else
            {
                //Cancel Previous token
                cts.Cancel();
                cts = null;
            }
        }
        //Actual Scene loading
        private async Task PerformSceneLoading(CancellationToken token, string sceneName)
        {
            token.ThrowIfCancellationRequested();
            if (token.IsCancellationRequested)
                return;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
           // await Task.Run(() => {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    if (token.IsCancellationRequested)
                        return;

                    if (asyncOperation.progress >= 0.9f)
                        break;
                }
                Debug.Log(" scene load completed" + sceneName); 

            //}); 
            
            
            asyncOperation.allowSceneActivation = true;
            cts.Cancel();
            token.ThrowIfCancellationRequested();

            //added this as a failsafe unnecessary
            if (token.IsCancellationRequested)
                return;
        }


    }
}