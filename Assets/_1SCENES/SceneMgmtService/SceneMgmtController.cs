using Intro;
using Quest;
using System;
using System.Collections;
using System.Collections.Generic;
using Town;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Common
{
    public class SceneMgmtController : MonoBehaviour
    {
        

        [SerializeField] GameObject SceneTransitPrefab;
        [SerializeField] GameObject sceneTransitGO;

        public SceneTransitView sceneTransitView;

        [SerializeField] bool sceneTransitStarted = false;


        [Header(" Scene ref")]
        [SerializeField] SceneName newScene;
        [SerializeField] SceneName lastScene; 

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneLoaded; 
            UpdateSceneName(SceneName.CORE);
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
        }
        
        public void UpdateSceneName(SceneName lastScene)
        {
            this.lastScene = lastScene;              
        }

        public IEnumerator LoadScene(SceneName sceneName)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
            StartSceneTransit();
            while (!async.isDone)
            {
                Debug.Log("Loading Scene");
                yield return null;               
            }
            SceneMgmtService.Instance.On_GameSceneLoaded(SceneName2GameScene(sceneName));
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.ToString()));
            lastScene = newScene; 
            newScene = sceneName;            
            if (!(lastScene == SceneName.CORE))
                StartCoroutine(UnloadAsyncOperation(lastScene)); 
           
        }
        IEnumerator UnloadAsyncOperation(SceneName sceneName)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName.ToString());

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
           
        }
        public void StartSceneTransit()
        {
            if (sceneTransitStarted) return;
            if(sceneTransitGO == null)
            sceneTransitGO = Instantiate(SceneTransitPrefab);
            sceneTransitGO.SetActive(true);
            sceneTransitView = sceneTransitGO.GetComponent<SceneTransitView>();

            RectTransform sceneTransitRect = sceneTransitGO.GetComponent<RectTransform>();

            sceneTransitRect.anchorMin = new Vector2(0, 0);
            sceneTransitRect.anchorMax = new Vector2(1, 1);
            sceneTransitRect.pivot = new Vector2(0.5f, 0.5f);
            sceneTransitRect.localScale = Vector3.one;
            sceneTransitRect.offsetMin = new Vector2(0, 0); // new Vector2(left, bottom);
            sceneTransitRect.offsetMax = new Vector2(0, 0); // new Vector2(-right, -top);
            sceneTransitStarted = true;
            sceneTransitView.StartAnim();
        }

        // Method to be removed evenually
        void OnSceneLoaded(Scene current, Scene next)
        {
            // end here 
           // UIControlServiceGeneral.Instance.CloseAllPanels();

            if(sceneTransitGO!= null)
            {
                if(next.isLoaded)
                {   
                    if (next.name == "QUEST")
                        QRoomService.Instance.On_QuestSceneStart(QuestNames.RatInfestation);

                    sceneTransitView = sceneTransitGO.GetComponent<SceneTransitView>();                
                    sceneTransitView.EndAnim();
                    sceneTransitStarted = false;
                }
            }
        }

        GameScene SceneName2GameScene(SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.INTRO:
                    return GameScene.InIntro;
                case SceneName.TOWN:
                    return GameScene.InTown;    

                case SceneName.QUEST:
                    return GameScene.InQuestRoom;

                case SceneName.COMBAT:
                    return GameScene.InCombat;

                case SceneName.CORE:
                    return GameScene.InCore;

                default:
                    return GameScene.None;  
            }
        }

      

    }
}