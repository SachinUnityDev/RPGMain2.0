using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using Common;

namespace Intro
{
    
    public class LoadingPanelController : MonoBehaviour, IPanel
    {

        [SerializeField] CanvasGroup IntroPanelOut;
        [SerializeField] CanvasGroup TownPanelIn; 

        [Header("Ref GO s")]
        [SerializeField] GameObject circularArrow; 
        [SerializeField] GameObject dotsParent;

        [Header("Text ref")]
        [SerializeField] TextMeshProUGUI desc; 
        [SerializeField] List<string> loadingLines = new List<string>();

        Scene scene; 
        void Start()
        {

            DontDestroyOnLoad(this.gameObject);
        }
        public void Load()
        {
            gameObject.SetActive(true);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            LoadScene(SceneNames.Town);
        }

        public void UnLoad()
        {
            UnLoadScene();
        }

        public void Init()
        {

        }
        public void LoadScene(SceneNames sceneName)
        {
            StartAnims();
            Application.backgroundLoadingPriority = ThreadPriority.Low;
            scene = SceneManager.GetActiveScene();
           // StartCoroutine(LoadingCoroutine(sceneName));
        }
        
        public void Fade(CanvasGroup Out, CanvasGroup In)
        {
            Out.DOFade(0f, 0.4f);
            In.DOFade(1f, 0.4f);
        }
        public void UnFade(CanvasGroup Out, CanvasGroup In)
        {
            Out.DOFade(1f, 0.4f);
            In.DOFade(0f, 0.4f);
        }

        public void UnLoadScene()
        {
            // on text revealer complete and fade screen on do this 
            SceneManager.UnloadSceneAsync(scene.name);
        }

        IEnumerator LoadingCoroutine(SceneNames sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync((int)sceneName, LoadSceneMode.Additive);
         
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress);
                Debug.Log("Progress" + progress);
                yield return null;
                operation.allowSceneActivation = true;
               // SceneManager.SetActiveScene(scene);
            }
        }
        void StartAnims()
        {
            circularArrow.transform.DORotate(new Vector3(0f, 0f, -360f), 2f, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear)
                ;

            //dotsParent.transform.DOScale(0, 5f)
            //         .SetLoops(-1, LoopType.Yoyo); 
                        
            
            // loading yo yo 
            // rotation 



        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Load();
            }
        }


    }
    

    public enum SceneNames
    {
        Intro, 
        Town, 
        QuestWalk, 
        Combat, 



    }


}


