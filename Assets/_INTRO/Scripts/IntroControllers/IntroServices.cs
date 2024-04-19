using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Spine.Unity;
using UnityEngine.Rendering;
using System.Linq;
using Common;

namespace Intro
{
    public class IntroServices : MonoSingletonGeneric<IntroServices>
    {

        public IntroController introController;
        public IntroViewController introViewController; 
        
        public List<GameObject> allPanels = new List<GameObject>();
        public int currPanel;

        public bool StartWithGate = true; 

        [SerializeField] SkeletonAnimation Enten;
        [SerializeField] SkeletonAnimation Emesh;
        
        void Start()
        {
            introController = GetComponent<IntroController>();
           StartIntro();        
        }

        public GameObject GetPanel(string strTag)
        {
            return allPanels.Find(t => t.name == strTag); 
        }

        public void MoveEntenNEmesh()
        {            
            Enten.gameObject.transform.DOLocalMoveX(5, 0.4f);
            Emesh.gameObject.transform.DOLocalMoveX(-5, 0.4f);
        }
        public void FadeOutEntenNEmesh(float alpha, float animSpeed)
        {
          
            StartCoroutine(FadeOutSpine(Enten, alpha, animSpeed));
            StartCoroutine(FadeOutSpine(Emesh, alpha, animSpeed));
         
        }

        public void EntenNEmeshToggleActive(bool val)
        {
            Enten.gameObject.SetActive(val);
            Emesh.gameObject.SetActive(val);    

        }
        public void FadeInEntenNEmesh( float alpha, float animSpeed)
        {
            StartCoroutine(FadeInSpine(Enten, alpha, animSpeed));
            StartCoroutine(FadeInSpine(Emesh, alpha, animSpeed));
            EntenNEmeshToggleActive(true);
        }

        public void AnimateEntenNEmesh()
        {           
            Enten.state.ClearTracks();
            Enten.AnimationName = "animation";
         
            Emesh.state.ClearTracks();
            Emesh.AnimationName = "animation";
        }
        public void LoadNext()
        {
            if (currPanel >= allPanels.Count) return;
            if (allPanels[currPanel].name == "StoryPanel")
            {
                if(GameService.Instance.gameController.isQuickStart)                
                    currPanel++;                
                else                
                    currPanel +=2;                
            }
            else            
                currPanel++;
            

            if (currPanel < allPanels.Count)
            {
                Debug.Log("Load next played" + currPanel);
                allPanels[currPanel].GetComponent<CanvasGroup>().interactable = true;
                allPanels[currPanel].GetComponent<CanvasGroup>().blocksRaycasts = true;
              
                allPanels[currPanel].GetComponent<IPanel>().Load();
            }                        
        }

        public void StartIntro()
        {           
            GameEventService.Instance.On_IntroStart();


            FadeOutEntenNEmesh(0.0f, 1f);
            allPanels.ForEach(t => t.GetComponent<IPanel>().Init());
            allPanels.ForEach(t => t.SetActive(false));
            allPanels.ForEach(t => t.GetComponent<CanvasGroup>().interactable = false);
            allPanels.ForEach(t => t.GetComponent<CanvasGroup>().blocksRaycasts = false);

            if (StartWithGate)
            {
                currPanel = 0; 
                allPanels[currPanel].GetComponent<IPanel>().Load();
            }
            else
            {
                currPanel = 1;
                allPanels[currPanel].GetComponent<IPanel>().Load();
            }
                

        }

        public void ApplyVignette(float value)
        {
         //   PostProcessingVol vol = PostProcessingGO.GetComponent<PostProcessingVol>()

        }
        public void ChangeSortingLayer()
        {
            Enten.GetComponent<MeshRenderer>().sortingLayerName = "Default";
            Emesh.GetComponent<MeshRenderer>().sortingLayerName = "Default";
        }

        IEnumerator FadeOutSpine (SkeletonAnimation skeleton, float endValue, float animSpeed)
        {
            Color color = skeleton.Skeleton.GetColor();
            for (float alpha = 1f; alpha >= endValue; alpha -= animSpeed)
            {
                color.a = alpha;
                skeleton.Skeleton.SetColor(color);
                yield return null;
            }
        }
 
        IEnumerator FadeInSpine(SkeletonAnimation skeleton, float endValue, float animSpeed)
        {
            Color color = skeleton.Skeleton.GetColor();

            for (float alpha =0f; alpha <= endValue; alpha += animSpeed)
            {
                color.a = alpha;
                skeleton.Skeleton.SetColor(color);
                yield return null;
            }
        }

        public void Fade(GameObject GO, float alpha)
        {           
            Image[] imgs = GO.transform.GetComponentsInChildren<Image>();
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i].DOFade(alpha, 1.0f);
                if (imgs[i].GetComponent<Button>() != null)
                {
                    if (alpha > 0.5f)
                    {                      
                        imgs[i].GetComponent<Image>().DOFade(1.0f, 0.5f);
                        imgs[i].GetComponent<Button>().enabled = true;
                    }
                    else
                    {
                        imgs[i].GetComponent<Image>().DOFade(0.0f, 0.5f)
                            .OnComplete(() => imgs[i].GetComponent<Button>().enabled = false);

                    }
                }

           }
           // GO.GetComponent<Image>().DOFade(0.8f, 0.2f);
            //FadeTxt(GO, alpha);

           // Debug.Log("Alpha in fade " + GO.name + "alpha " + alpha);
        }

        public void FadeTxt(GameObject GO, float alpha, float animSpeed)
        {           
            TextMeshProUGUI[] txts = GO.transform.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < txts.Length; i++)
            {
                txts[i].DOFade(alpha, animSpeed);
            }
        }
    }



}
