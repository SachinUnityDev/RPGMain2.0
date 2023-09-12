using Quest;
using System.Collections;
using System.Collections.Generic;
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
        private void OnEnable()
        {
          
            SceneManager.activeSceneChanged += OnSceneLoaded; 
        }
        private void OnDisable()
        {
            //loadQ.onClick.RemoveAllListeners();
            //loadC.onClick.RemoveAllListeners();

            SceneManager.activeSceneChanged -= OnSceneLoaded;
        }

        void OnQuestLoadPressed()
        {
            SceneManager.LoadSceneAsync("QUEST"); 
            StartSceneTransit();
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
                    sceneTransitView = sceneTransitGO.GetComponent<SceneTransitView>();
                    Debug.Log("Scene Transist to begin end anim");
                    sceneTransitView.EndAnim();
                    sceneTransitStarted = false;
                    QRoomService.Instance.On_QuestSceneStart(QuestNames.RatInfestation); 

                }
            }
            // toggle off camera, event System and canvas  of previous scene
            // toggle on ...
            // allowscene activation  here 
        }

        void OnCombatLoadPressed()
        {
            SceneMgmtService.Instance.LoadNewScene("COMBAT");
            StartSceneTransit();
        }
        



    }
}