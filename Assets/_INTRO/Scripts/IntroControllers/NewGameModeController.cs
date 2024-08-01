using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common; 

namespace Intro
{
    public class NewGameModeController : MonoBehaviour, IPanel
    {
        [SerializeField] GameObject CampaignBtn;
        [SerializeField] GameObject UpHillBtn;

        Vector3 initPos = new Vector3(0f, -215f, 0f);
        Vector3 finalPosLeft = new Vector3(-700, 227, 0);
        Vector2 finalPosRight = new Vector3(700, 227, 0);

        public void Init()
        {
            MoveUI(initPos, UpHillBtn.gameObject);
            MoveUI(initPos, CampaignBtn.gameObject);
        }


        void Start()
        {
            CampaignBtn.GetComponent<Button>().onClick.AddListener(OnCampaignBtnPressed);     

        }
        public void Load()
        {           

            gameObject.SetActive(true);
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, true);
            IntroServices.Instance.Fade(gameObject, 1.0f);
            UIControlServiceGeneral.Instance.SetMaxSiblingIndex(gameObject);
            IntroServices.Instance.ChangeSortingLayer("Default");
            //IntroServices.Instance.FadeOutEntenNEmesh(0.8f, 0.008f);
            IntroServices.Instance.AnimateEntenNEmesh();
            MoveUI(finalPosLeft, CampaignBtn.gameObject);
            MoveUI(finalPosRight, UpHillBtn.gameObject);
        }

        public void UnLoad()
        {
            IntroServices.Instance.Fade(gameObject, 0.0f);
            IntroServices.Instance.AnimateEntenNEmesh();
            GameObject mainPanel = IntroServices.Instance.GetPanel("MainMenu");
            Debug.Log("main Menu " + mainPanel.name);
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => IntroServices.Instance.Fade(gameObject, 0.0f));
            seq.AppendCallback(() => IntroServices.Instance.Fade(mainPanel, 0.0f));
            seq.AppendCallback(() => gameObject.SetActive(false));
            seq.AppendCallback(() => mainPanel.SetActive(false));
            seq.AppendCallback(() => IntroServices.Instance.LoadNext());
            seq.Play();
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);

        }
        public void Unload2Main()
        {
            IntroServices.Instance.Fade(gameObject, 0.0f);
            IntroServices.Instance.AnimateEntenNEmesh();
            IntroServices.Instance.FadeInEntenNEmesh(1,1);
            GameObject mainPanel = IntroServices.Instance.GetPanel("MainMenu");           
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() => MoveUI(initPos, CampaignBtn.gameObject)); 
            seq.AppendCallback(() => MoveUI(initPos, UpHillBtn.gameObject));
            seq.AppendCallback(() => IntroServices.Instance.Fade(gameObject, 0.0f));
            seq.AppendCallback(() => IntroServices.Instance.Fade(mainPanel, 1.0f));
            seq.AppendCallback(() => gameObject.SetActive(false));
            seq.AppendCallback(() => mainPanel.SetActive(true));
            seq.AppendCallback(() => IntroServices.Instance.LoadMainMenu());
            seq.Play();
            UIControlServiceGeneral.Instance.ToggleInteractionsOnUI(this.gameObject, false);
            IntroServices.Instance.AnimateEntenNEmesh();
            IntroServices.Instance.FadeInEntenNEmesh(1, 1);
            IntroServices.Instance.ChangeSortingLayer("FrontEnvironment");

        }
        void MoveUI(Vector3 pos, GameObject GO)
        {
            GO.transform.DOLocalMove(pos, 0.8f);
        }

        void OnCampaignBtnPressed()
        {

            Debug.Log("CAMPAIGN BTN PRESSED");
            UnLoad();

        }

    
    }


}

