using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Common;


namespace Town
{


    public class WoodGameView : MonoBehaviour
    {

        [SerializeField] GameObject GameSeqView;
        [SerializeField] GameObject MistakeCountView;

        [Header("Hit Ticks")]
        [SerializeField] GameObject hitsView;
        [SerializeField] Sprite hitsGreen;
        [SerializeField] Sprite hitsGreyed;


        [Header("Player Lvl")]
        [SerializeField] GameObject playerRankGO;

        [Header("TIMER")]
        [SerializeField] GameObject TimerGO;
        [SerializeField] float netTime;
        [SerializeField] float gameStartTime;


        [Header("PopUps")]
        [SerializeField] GameObject popUp;
        [SerializeField] GameObject LoadPopUp;


        [Header("Joob Exp Bar")]
        [SerializeField] GameObject jobBar;

        [SerializeField] WoodGameController1 woodGameController;

        [Header("Button UI Controlls")]
        bool isOptOpen, isFleeOpen;
        [SerializeField] Button optionsBtn;
        [SerializeField] Button fleeBtn;


        [Header("Exit options Buttons")]
        [SerializeField] Button continueBtn;
        [SerializeField] Button Back2town;

        [Header("START PANEL")]
        [SerializeField] GameObject startPanel;


        void Start()
        {
            isOptOpen = false;
            isFleeOpen = false;
            continueBtn.transform.parent.GetComponent<RectTransform>().DOScaleY(0, 0.1f);
            fleeBtn.GetComponent<RectTransform>().DOScaleX(0, 0.1f);

            continueBtn.onClick.AddListener(OnFleeBtnPressed);
            Back2town.onClick.AddListener(JobService.Instance.woodGameController.OnBack2TownPressed);

            popUp.transform.DOScale(0f, 0.1f);
            LoadPopUp.transform.DOScale(0f, 0.1f);
            // woodGameController = GetComponent<WoodCuttingController>();
            optionsBtn.onClick.AddListener(OnOptionsBtnPressed);
            fleeBtn.onClick.AddListener(OnFleeBtnPressed);
        }
        public void InGamePopUp(string textStr)
        {
            popUp.GetComponentInChildren<TextMeshProUGUI>().text = textStr;
            Sequence popUpSeq = DOTween.Sequence();
            popUpSeq.
                Append(popUp.transform.DOScale(1, 0.4f))
                .AppendInterval(0.75f)
                .Append(popUp.transform.DOScale(0, 0.4f));
            popUpSeq.Play();
        }
        public void LoadGamePopUp(string textStr)
        {
            LoadPopUp.GetComponentInChildren<TextMeshProUGUI>().text = textStr;
            Sequence popUpSeq = DOTween.Sequence();
            popUpSeq.
                Append(LoadPopUp.transform.DOScale(1, 0.4f))
                .AppendInterval(0.75f)
                .Append(LoadPopUp.transform.DOScale(0, 0.4f));
            popUpSeq.Play();
        }


        public void GameStartView(WoodGameData woodGameData)
        {
            PopulateGameSeq(woodGameData.gameSeq);
            PopulateMistakeHearts(woodGameData.totalMistakesAllowed, 0);
            PopulateHits(woodGameData.totalCorrectHits, 0);
            PopulateRank((int)woodGameData.woodGameRank);
            netTime = woodGameData.timeAvailable4Game;
            gameStartTime = Time.time;
            PopulateGameTimer(gameStartTime);
        }

        public void PopulateJobExp(WoodGameData woodGameData)
        {
            float ratio = (JobService.Instance.woodGameController.currGameJobExp - woodGameData.minJobExpR) / (woodGameData.maxJobExpR - woodGameData.minJobExpR);

            // based on min and max R for a given Rank 
            jobBar.transform.GetChild(1).GetComponent<Image>().fillAmount = ratio;

        }

        void PopulateHits(int netHits, int currHits)
        {
            for (int i = 0; i < netHits; i++)
            {
                if (i < currHits)
                {
                    hitsView.transform.GetChild(i).GetComponent<Image>().sprite = hitsGreen;
                }
                else
                {
                    hitsView.transform.GetChild(i).GetComponent<Image>().sprite = hitsGreyed;
                }
            }
        }
        public void PopulateGameView(WoodGameData woodGameData, int hits, int missHits)
        {
            PopulateGameSeq(woodGameData.gameSeq);
            PopulateMistakeHearts(woodGameData.totalMistakesAllowed, missHits);
            PopulateHits(woodGameData.totalCorrectHits, hits);
            PopulateRank((int)woodGameData.woodGameRank);
        }

        void PopulateRank(int Rank)
        {
            int count = playerRankGO.transform.childCount;

            for (int i = 0; i < count; i++)
            {
                if (i <= Rank)
                    playerRankGO.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(1.0f, 0.1f);
                else
                    playerRankGO.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.0f, 0.1f);
            }

        }

        public void PopulateGameSeq(int gameSeq)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i < gameSeq - 1)
                    GameSeqView.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                else
                    GameSeqView.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }


        void PopulateMistakeHearts(int netAvailable, int missHits)
        {
            for (int i = 0; i < MistakeCountView.transform.childCount; i++)
            {
                if (i < netAvailable)
                    MistakeCountView.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.8f, 0.1f);
                else
                    MistakeCountView.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.0f, 0.1f);

            }

            int availableMistakes = netAvailable - missHits;
            for (int i = 0; i < netAvailable; i++)
            {
                if (i < availableMistakes)
                    MistakeCountView.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.8f, 0.1f);
                else
                    MistakeCountView.transform.GetChild(i).gameObject.GetComponent<Image>().DOFade(0.0f, 0.1f);
            }
        }

        void PopulateGameTimer(float currTime)
        {
            float timerVal = currTime - gameStartTime;
            if (timerVal < netTime)
                TimerGO.transform.GetChild(1).GetComponent<Image>().fillAmount = timerVal / netTime;
            else
            {
                InGamePopUp("TIME OUT");
                // woodGameController.ReloadGameSeq();
            }
        }

        private void Update()
        {
            //if (gameState == WoodGameState.Running)
            //{
            //    PopulateGameTimer(Time.time); 
            //}
        }

        void OnFleeBtnPressed()
        {
            if (isFleeOpen)
            {
                continueBtn.transform.parent.GetComponent<RectTransform>().DOScaleY(0, 0.4f);
                isFleeOpen = false;
            }
            else
            {
                continueBtn.transform.parent.GetComponent<RectTransform>().DOScaleY(1, 0.4f);
                isFleeOpen = true;
            }


        }

        void OnOptionsBtnPressed()
        {
            if (isOptOpen)
            {
                fleeBtn.GetComponent<RectTransform>().DOScaleX(0, 0.4f);
                isOptOpen = false;
            }
            else
            {
                fleeBtn.GetComponent<RectTransform>().DOScaleX(1, 0.4f);
                isOptOpen = true;
            }
        }

        public void TogglePanel(bool active, string str)
        {
            startPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = " ";
            startPanel.SetActive(active);

        }
    }
}