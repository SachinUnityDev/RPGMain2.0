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

        public void Init()
        {
        
        }

        public void Load()
        {
            gameObject.SetActive(true);
            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            video.gameObject.SetActive(true);
            SoundServices.Instance.StopAllBGSound(0.25f);
            SoundServices.Instance.PlayBGSound(BGAudioClipNames.VideoDialogue);
        }

        public void UnLoad()
        {           
            Debug.Log("Cut Scene unload");
            // get next panel and Set it active 
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);

            video.gameObject.SetActive(false);
            SoundServices.Instance.StopAllBGSound(0.25f);
            IntroServices.Instance.Fade(gameObject, 0.4f);
            IntroServices.Instance.LoadNext();
        }


        private void OnEnable()
        {
            InitVideo(); 
        }
        void InitVideo()
        {
            video.DORestart();
            video.Prepare();
            StartCoroutine(VideoPreparing());
            video.loopPointReached += EndReached;

            // video.waitForFirstFrame = true;            
            video.Play();
        }
        IEnumerator VideoPreparing()
        {
            while (!video.isPrepared)
            {
                yield return null;
            }
        }
        void EndReached(VideoPlayer vp)
        {
            vp.Stop();
            UnLoad();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnLoad();
            }
        }

    }


}

//videoPlayer.loopPointReached += EndReached;
//And then the EndReached method
//Code (CSharp):
//void EndReached(UnityEngine.Video.VideoPlayer vp)
//{
//    vp.playbackSpeed = vp.playbackSpeed / 10.0F;
//}