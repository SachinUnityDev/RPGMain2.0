using Intro;
using Quest;
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
            SetAsLastScene(SceneName.CORE);
        }
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneLoaded;
        }
        
        public void SetAsLastScene(SceneName lastScene)
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
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.ToString()));
            if (lastScene != SceneName.CORE)
             StartCoroutine(UnloadAsyncOperation(sceneName));            
        }
        IEnumerator UnloadAsyncOperation(SceneName sceneName)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(lastScene.ToString());

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
          //  SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.ToString()));
            SetAsLastScene(sceneName);

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
        void OnSceneLoaded(Scene current, Scene next)
        {
            // end here 
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

        void OnCombatLoadPressed()
        {


        }

        SceneName GetSceneName(Scene scene)
        {
            switch (scene.name)
            {
                case "Intro":
                    return SceneName.TOWN;
                case "Town":
                    return SceneName.TOWN;
                case "Quest":
                    return SceneName.QUEST;
                case "Combat":
                    return SceneName.COMBAT;
                default:
                    return SceneName.INTRO;
            }
        }

    }
}