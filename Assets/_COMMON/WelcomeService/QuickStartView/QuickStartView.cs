using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Common; 

namespace Intro
{
    public class QuickStartView : MonoBehaviour, IPanel
    {
        [SerializeField] QuickStartPg1View quickStartPg1View;
        [SerializeField] QuickStartPg2View quickStartPg2View;

        public void Init()
        {
            InitQuickStart();
        }

        public void InitQuickStart()
        {
            quickStartPg1View.QuickStartPg1Init(this);
            quickStartPg2View.QuickStartPg2Init(this);
        }

        public void ShowPg1()
        {
            quickStartPg1View.gameObject.SetActive(true);
            quickStartPg2View.gameObject.SetActive(false);
        }
        public void ShowPg2()
        {
            quickStartPg1View.gameObject.SetActive(false);
            quickStartPg2View.gameObject.SetActive(true);         
        }
        public void Load()
        {
            gameObject.SetActive(true);
            InitQuickStart();
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);

            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
        }

        public void UnLoad()
        {
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);
            IntroServices.Instance.Fade(gameObject, 0.0f);
            IntroServices.Instance.LoadNext();
            gameObject.SetActive(false);
        }
    }
}