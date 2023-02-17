using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using Common;



namespace Common
{
    public interface IPanel
    {
        void Load();
        void UnLoad();
        void Init();
    }
}


namespace Intro
{
    public class GateController : MonoBehaviour, IPanel
    {

        [SerializeField] TextMeshProUGUI GateTxtObj;
        //[SerializeField] SkeletonAnimation nightSkeleton;
        //[SerializeField] SkeletonAnimation Night2daySkeleton;
        public Animator gatesAnim;
        public Image GateImg;

        [SerializeField] Sprite GateDaySprite;
        [SerializeField] Sprite GateNightSprite;
        public void Init()
        {
            GateTxtObj.GetComponent<TextRevealer>().enabled = false;
           // Night2daySkeleton.gameObject.SetActive(false);
          
          
        }

        public void Load()
        {
            gameObject.SetActive(true);
            GateImg.sprite = GateDaySprite;
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            GateTxtObj.GetComponent<TextRevealer>().enabled = true;
            SoundServices.Instance.PlayBGSound(BGAudioClipNames.GatesAmbience);
        }

        public void UnLoad()
        {
           // IntroServices.Instance.Fade(gameObject, 0.0f);
            GateTxtObj.GetComponent<TextRevealer>().enabled = false;
            GateImg.sprite = GateNightSprite;
            //nightSkeleton.gameObject.SetActive(false);
            //Night2daySkeleton.gameObject.SetActive(true);
            gatesAnim.SetBool("Gates", true);
            StartCoroutine(WaitNExit(0.1f));

        }

   
     
        //IEnumerator FadeOutTxt()
        //{
        //    Color colortxt = GateTxtObj.GetComponent<TextMeshPro>().color;
        //    for (float alpha = 1f; alpha >= 0; alpha -= 0.008f)
        //    {
        //        colortxt.a = alpha;
        //        GateTxtObj.GetComponent<TextMeshPro>().color = colortxt;
        //        yield return null;
        //    }

        //}
    
        IEnumerator WaitNExit(float time)
        {

            yield return new WaitForSeconds(time);
    
           // StartCoroutine(IntroServices.Instance.Wait(2f));
           // gatesAnim.SetBool("Gates", false);
            // StartCoroutine(FadeOut(Night2daySkeleton));
            // StartCoroutine(FadeOutTxt());
            StartCoroutine(FadeOut(this.gameObject));
        }


        IEnumerator FadeOut(GameObject gameObject)
        {
            Color color = GateImg.color;
            
            for (float alpha = 1f; alpha >= 0; alpha -= 0.008f)
            {
                color.a = alpha;
                GateImg.color = color;                  
                yield return null;               
            }
            gameObject.SetActive(false);
            IntroServices.Instance.LoadNext();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && IntroServices.Instance.currPanel ==0)
            {
                
                UnLoad();     
            }
        }
        //IEnumerator FadeIn(SkeletonAnimation skeleton)
        //{
        //    Color color = skeleton.Skeleton.GetColor();
        //    for (float alpha = 0f; alpha <= 1; alpha += 0.008f)
        //    {
        //        color.a = alpha;
        //        skeleton.Skeleton.SetColor(color);

        //        yield return null;

        //    }
        //    //yield return  StartCoroutine(FadeOutTxt());
        //    // yield return StartCoroutine(FadeOut(daySkeleton));
        //    //  IntroServices.Instance.LoadNext();
        //}


    }




}
