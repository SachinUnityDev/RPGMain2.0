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

           
        }
        public void Load()
        {
            gameObject.SetActive(true);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
           // GameService.Instance.sceneController.LoadScene(GameScene.Town);
            LoadSceneSeq();
           
        }

        void LoadSceneSeq()
        {

            Sequence loadSeq = DOTween.Sequence();
            loadSeq
                   .AppendCallback(()=> ShowDots())
                   .AppendInterval(1f)               
                    ;
            loadSeq.Play().SetLoops(-1);
        }

        //void StartLoadingAnimation()
        //{
        //    // Scale the image back and forth using DoTween
        //    dotsParent.(1, 1f)
        //        .SetEase(Ease.InOutQuad)
        //        .SetLoops(-1, LoopType.Yoyo);
        //}

        void ShowDots()
        {
            if (count > 3) // clear all 
            {
                for (int i = 0; i < 3; i++)
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
                SceneManager.SetActiveScene(scene);
            }
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


