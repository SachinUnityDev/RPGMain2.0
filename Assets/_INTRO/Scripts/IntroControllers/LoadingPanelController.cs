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

        [Header("TBR")]         
        [SerializeField] Transform dotsParent;

        [Header("Text ref")]
        [SerializeField] TextMeshProUGUI desc; 
        [SerializeField] List<string> loadingLines = new List<string>();

        Scene scene;

        int count = 1; 
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
            //LoadScene(SceneNames.Town);
            LoadSceneSeq();
        }

        void LoadSceneSeq()
        {
            Sequence loadSeq = DOTween.Sequence();
            loadSeq
                   .AppendCallback(()=>ShowDots())
                   .AppendInterval(0.2f)
                   //.AppendCallback(()=> GameService.Instance.sceneController.LoadScene(GameScene.Town))
                    ;
            loadSeq.Play().SetLoops(20);
        }

        void LoadDotAnim()
        {

            //StartCoroutine(DotCoroutine());
        //    Sequence loadDotSeq = DOTween.Sequence();
        //    loadDotSeq
        //        .AppendCallback(()=> ShowDots())
        //        .AppendInterval(0.01f)
        //        //.AppendCallback(() => ShowDots())
        //        //.AppendInterval(0.4f)
        //        //.AppendCallback(() => ShowDots())
        //        //.AppendInterval(0.4f)
        //        ;
        //    loadDotSeq.Play().SetLoops(20);
        }

        //IEnumerator DotCoroutine()
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    ShowDots();
        //}

        void ShowDots()
        {
            if (count == 3) // clear all 
            {
                for (int i = 0;i < count; i++)
                {
                    dotsParent.GetChild(i).gameObject.SetActive(false);
                }
                count = 1;
                return; 
            } 
                
            for (int i = 0; i < count; i++)
            {
                dotsParent.GetChild(i).gameObject.SetActive(true); 
            }
            count++; 
        }
        public void UnLoad()
        {
           // UnLoadScene();
        }

        public void Init()
        {

        }
        //public void LoadScene(SceneNames sceneName)
        //{
        //    StartAnims();
        //    Application.backgroundLoadingPriority = ThreadPriority.Low;
        //    scene = SceneManager.GetActiveScene();
        //    StartCoroutine(LoadingCoroutine(sceneName));
        //}
        
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

        //public void UnLoadScene()
        //{
        //    // on text revealer complete and fade screen on do this 
        //    SceneManager.UnloadSceneAsync(scene.name);
        //}

        //IEnumerator LoadingCoroutine(SceneNames sceneName)
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
       
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Load();
            }
        }

        void StartAnims()
        {
            //circularArrow.transform.DORotate(new Vector3(0f, 0f, -360f), 2f, RotateMode.LocalAxisAdd)
            //  .SetLoops(-1, LoopType.Restart)
            //  .SetRelative()
            //  .SetEase(Ease.Linear)
            //  ;
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


