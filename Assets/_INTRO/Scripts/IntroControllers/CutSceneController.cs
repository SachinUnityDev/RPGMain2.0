using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Video;
using Common; 
namespace Intro
{
    public class CutSceneController : MonoBehaviour, IPanel
    {
        [SerializeField] VideoPlayer video;
        [SerializeField] RawImage rawImg;

        [Header(" On Esc anim")]

        [SerializeField] OnEscAnim onEscAnim;
        [SerializeField] bool animPlaying = false; 
        [SerializeField] float startTime =0f;
        [SerializeField] float playTime = 0.5f;
        [SerializeField] bool endReached = false;
        public void Init()
        {
            InitVideo();
            rawImg.DOFade(0.0f, 0.01f) ; 
        }

        public void Load()
        {
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
          
            gameObject.SetActive(true);
            video.gameObject.SetActive(true);
            if (!video.isPrepared)
            {
                video.Prepare(); // Prepare the video asynchronously
         
                video.prepareCompleted += VideoPrepared; // Subscribe to the prepare completed event
            }
            else
            {
                StartVideo(); // Start playing the video if it's already prepared
            }
        }

        public void UnLoad()
        {           
            Debug.Log("Cut Scene unload");
            // get next panel and Set it active 
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);

            video.gameObject.SetActive(false);
     
            IntroServices.Instance.Fade(gameObject, 0.4f);
            IntroServices.Instance.LoadNext();
        }
        void VideoPrepared(VideoPlayer vp)
        {
            StartVideo();
            video.prepareCompleted -= VideoPrepared;
        }
        void StartVideo()
        {
            Sequence playSeq = DOTween.Sequence();
            playSeq
                .AppendCallback(() => { video.time = 0; })
                
                 .AppendInterval(1f)
                .AppendCallback(() => IntroAudioService.Instance.StopAllBGSound(0.25f))
                .AppendCallback(() => IntroAudioService.Instance.PlayBGSound(BGAudioClipNames.VideoDialogue))
                .AppendCallback(()=> video.Play())
                .Append(rawImg.DOFade(1.0f, 1f))
                ;

            playSeq.Play();
        }



        public void InitVideo()
        {
            video.time = 0f;
            video.DORestart();
            video.Prepare();

            video.loopPointReached += EndReached;
            video.Play();
        }

        void EndReached(VideoPlayer vp)
        {
            animPlaying= false; endReached = true; 
            vp.Stop();
            UnLoad();
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {  
                if(endReached) return;
                if (!animPlaying)
                {
                    startTime = Time.time;                  
                    onEscAnim.ResetAnim();
                    animPlaying= true;
                }
                else
                { 
                    float val = ((Time.time - startTime)/playTime);
                    Debug.Log("VALUE" + val);
                    onEscAnim.PlayAnim(val);
                    if (val >= 0.9f)
                    {
                        EndReached(video);                        
                    }
                }
                onEscAnim.gameObject.SetActive(animPlaying);
            }
            if (Input.GetKeyUp(KeyCode.Escape) ||endReached)
            {               
                animPlaying= false;                
                onEscAnim.ResetAnim();
                onEscAnim.gameObject.SetActive(animPlaying);
            }            
        }
    }
}

