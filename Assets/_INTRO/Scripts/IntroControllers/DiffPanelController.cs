using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

namespace Intro
{
    public class DiffPanelController : MonoBehaviour, IPanel
    {
        [SerializeField] Button diffContinueBtn;

        [SerializeField] Toggle mortalBloodToggle;
        [SerializeField] Toggle quickViewToggle;

        void Start()
        {
            diffContinueBtn.onClick.AddListener(OnContinueBtnPressed);
            mortalBloodToggle.onValueChanged.AddListener(OnMortalBloodSelect);
            quickViewToggle.onValueChanged.AddListener(OnQuickStartSelect);

        }
        public void Init()
        {


        }
        public void Load()
        {
            gameObject.SetActive(true);
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
        void OnContinueBtnPressed()
        {
            UnLoad(); 
        }

        void OnMortalBloodSelect(bool isMB)
        {
            GameService.Instance.gameController.SetMortalBlood(isMB);
        }
        void OnQuickStartSelect(bool isQuickStart)
        {
            GameService.Instance.gameController.SetQuickStart(isQuickStart);
        }
    }
}

