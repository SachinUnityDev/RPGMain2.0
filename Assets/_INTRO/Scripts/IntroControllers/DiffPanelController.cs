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
        void Start()
        {
            diffContinueBtn.onClick.AddListener(OnContinueBtnPressed); 
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

    }


}

