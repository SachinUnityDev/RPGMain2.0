using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Common; 

namespace Intro
{
    public class StoryPanelController : MonoBehaviour, IPanel
    {

        TextMeshProUGUI continueBtnColor; 

        public void Init()
        {
            continueBtnColor = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            IntroServices.Instance.FadeTxt(gameObject.transform.GetChild(1).gameObject, 0.0f, 0.1f);
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }

        public void OnRevealCompleted()
        {
            gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            IntroServices.Instance.FadeTxt(gameObject.transform.GetChild(1).gameObject, 1.0f, 0.4f);
          
        }

     
        public void Load()
        {
            gameObject.SetActive(true);
            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            transform.GetChild(0).gameObject.SetActive(true);// text revealer
            IntroServices.Instance.MoveEntenNEmesh();
            SoundServices.Instance.PlayBGSound(BGAudioClipNames.PoetryMusic);
        }

        public void UnLoad()
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => transform.GetChild(0).gameObject.SetActive(false));
            seq.AppendInterval(2.5f);
            seq.AppendCallback(() => IntroServices.Instance.Fade(gameObject, 0.0f));
            seq.AppendCallback(() => IntroServices.Instance.LoadNext());
            seq.AppendCallback(() => gameObject.SetActive(false));
            seq.AppendCallback(() => IntroServices.Instance.EntenNEmeshToggleActive(false));

            seq.Play();
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);

        }




    }

}

