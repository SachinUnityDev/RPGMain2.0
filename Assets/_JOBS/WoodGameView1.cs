using Common;
using DG.Tweening;
using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class WoodGameView1 : MonoBehaviour
    {
        public Action OnRewardQuickSell; 

        [SerializeField] GameObject GameSeqView;
        [SerializeField] GameObject MistakeCountView;

        [Header("Hit Ticks")]
        [SerializeField] Transform hitsTrans;
        [SerializeField] Sprite hitsGreen;
        [SerializeField] Sprite hitsGreyed;


        [Header("Player Lvl")]
        [SerializeField] GameObject playerRankGO;

        [Header("TIMER")]
        [SerializeField] GameObject TimerGO;
        [SerializeField] float netTime;
        [SerializeField] float gameStartTime;

        


        [Header("Job Exp Bar")]
        [SerializeField] Transform expBar;
        public float expGained = 0f; 

        [SerializeField] WoodGameController1 woodController;
        [SerializeField] WoodAnimController woodAnimController;
       

        //[Header("Button UI Controlls")]
        //bool isOptOpen, isFleeOpen;
        //[SerializeField] Button optionsBtn;
        //[SerializeField] Button fleeBtn;


        //[Header("Exit options Buttons")]
        //[SerializeField] Button continueBtn;
        //[SerializeField] Button Back2town;

        [Header("START PANEL")]
        [SerializeField] StartGameView startGameView;

        [Header(" Game Reload Panel")]
        [SerializeField] ReloadGameView reloadGameView;

        [Header(" Success View")]
        [SerializeField] SuccessView successView; 

        [Header("Slider View")]
        [SerializeField] SliderView sliderView;

        [Header("Rewards View")]
        [SerializeField] RewardsView rewardsView; 

        [Header("Global Var")]
        public WoodGameState gameState;
        [SerializeField]WoodGameData currWoodGameData;



        void Start()
        {
            // woodAnimController = GetComponent<WoodAnimController>();
            //isOptOpen = false;
            //isFleeOpen = false;
            //continueBtn.transform.parent.GetComponent<RectTransform>().DOScaleY(0, 0.1f);
            //fleeBtn.GetComponent<RectTransform>().DOScaleX(0, 0.1f);

            //continueBtn.onClick.AddListener(OnFleeBtnPressed);
            //Back2town.onClick.AddListener(WoodGameService.Instance.OnBack2TownPressed);

            //popUp.transform.DOScale(0f, 0.1f);
            //LoadPopUp.transform.DOScale(0f, 0.1f);
            // woodGameController = GetComponent<WoodCuttingController>();
            //optionsBtn.onClick.AddListener(OnOptionsBtnPressed);
            //fleeBtn.onClick.AddListener(OnFleeBtnPressed);
         
        }
      

        public void NewGameInit(WoodGameData currWoodGameData, WoodGameController1 woodController)
        {
            this.woodController = woodController;
            startGameView.InitStartGame(this);
            reloadGameView.InitReload(this);
            woodAnimController.InitAnim(this);
            successView.InitSuccessView(this);
            PopulateJobExp(currWoodGameData);
            NewSeqInit(currWoodGameData);   
        }
        public void NewSeqInit(WoodGameData currWoodGameData)
        {
            this.currWoodGameData = currWoodGameData;            
            sliderView.Init(this, currWoodGameData);
        }
        public void ReloadGameSeq()
        {
            ControlGameState(WoodGameState.LoadPaused);
            sliderView.ChangeBarScale();           
            Sequence seq = DOTween.Sequence();
            seq
                 .AppendInterval(1f)
                 .AppendCallback(()=>  reloadGameView.Show())
                 ;
            seq.Play();
        }
        public void ShowSuccess()
        {
            successView.Show();     
        }
        public void OnExpConvert()
        {
            CharService.Instance.GetCharCtrlWithName(CharNames.Abbas).charModel.mainExp += (int)expGained;
            rewardsView.FillExpBar(0); 
        }
       
        public void OnQuickSell()
        {
           List<ItemDataWithQty> allItemQty = new List<ItemDataWithQty>();
            allItemQty =  woodController.woodGameSO.
                          GetRewardItems(currWoodGameData.gameSeq, currWoodGameData.woodGameRank);
            
            foreach (ItemDataWithQty itemQty in allItemQty)
            {
                int count = itemQty.quantity;                 
                CostData costData =
                    ItemService.Instance.GetCostData(itemQty.itemData.itemType, itemQty.itemData.itemName);
                // add to play Eco and dispose item
                int silver = (costData.baseCost.silver / 3) * count;
                int bronze = (costData.baseCost.bronze / 3) * count;
                Currency itemSaleVal = new Currency(silver, bronze).RationaliseCurrency();
                EcoServices.Instance.AddMoney2PlayerInv(itemSaleVal);
            }

            OnRewardQuickSell?.Invoke();
        }

        bool AddJobExpNChkRank()
        {
            expGained += UnityEngine.Random.Range(currWoodGameData.maxJobExpAdded, currWoodGameData.minJobExpAdded);
            if(currWoodGameData.netGameExp + expGained >= currWoodGameData.maxJobExpR)
            {
               if(woodController.currWoodGameRank != WoodGameRank.Master)
                {
                    woodController.currWoodGameRank++;
                    currWoodGameData.woodGameRank++;
                }
                Back2Town();
                return false; 
            }
            return true; 
        }
        public void ChgGameParam2Next()
        {
            if (currWoodGameData.gameSeq < 3)
            {
                currWoodGameData.gameSeq++;
            }
            else if (currWoodGameData.gameSeq >= 3 && currWoodGameData.woodGameRank != WoodGameRank.Master)
            {
                Debug.Log("you are DONE FOR THE DAY !");
                Back2Town();
                return;
            }
            else if (currWoodGameData.gameSeq >= 3 && currWoodGameData.woodGameRank == WoodGameRank.Master)
            {
                Debug.Log("Game play to be continued at master");
                Back2Town();
                return;
            }            
            currWoodGameData = woodController.woodGameSO
                        .GetWoodGameData(currWoodGameData.gameSeq, currWoodGameData.woodGameRank).DeepClone();
            NewSeqInit(currWoodGameData);
            OnContinuePressed();
        }

        public bool LoadTheNextGameSeq()
        {
            ChgGameParam2Next();            
            ControlGameState(WoodGameState.LoadPaused);
            return AddJobExpNChkRank(); // check if game check out on Rank 
        }
        public void OnContinuePressed()
        {
            ControlGameState(WoodGameState.Running);
            sliderView.StartMovement();
            PopulateGameSeq(currWoodGameData.gameSeq);
            PopulateMistakeHearts(currWoodGameData.totalMistakesAllowed, 0);
            PopulateHits(currWoodGameData.totalCorrectHits, 0);
            PopulateRank((int)currWoodGameData.woodGameRank);
            netTime = currWoodGameData.timeAvailable4Game;
            gameStartTime = Time.time;
            PopulateGameTimer(gameStartTime);

        }

        public void PopulateJobExp(WoodGameData woodGameData)
        {
            float ratio = (expGained - woodGameData.minJobExpR) / (woodGameData.maxJobExpR - woodGameData.minJobExpR);

            // based on min and max R for a given Rank 
            expBar.GetChild(1).GetComponent<Image>().fillAmount = ratio;

        }
        public void PopulateGameView(WoodGameData woodGameData, int hits, int missHits)
        {
            PopulateGameSeq(woodGameData.gameSeq);
            PopulateMistakeHearts(woodGameData.totalMistakesAllowed, missHits);
            PopulateHits(woodGameData.totalCorrectHits, hits);
            PopulateRank((int)woodGameData.woodGameRank);
        }
        void PopulateHits(int netHits, int currHits)
        {
            for (int i = 0; i < netHits; i++)
            {
                hitsTrans.GetChild(i).gameObject.SetActive(true);
                if (i < currHits)
                {
                    hitsTrans.GetChild(i).GetComponent<Image>().sprite = hitsGreen;
                }
                else
                {
                    hitsTrans.GetChild(i).GetComponent<Image>().sprite = hitsGreyed;
                }
            }
            for (int j = netHits; j < hitsTrans.childCount; j++)
            {
                hitsTrans.GetChild(j).gameObject.SetActive(false); 
            }
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
               Debug.Log("TIME OUT");
                ReloadGameSeq();
            }
        }     
        public void Back2Town()
        {  
            sliderView.SliderMovementStop();
           
            // show reward panel 
            rewardsView.RewardsInit(currWoodGameData, woodController, this); 
            rewardsView.ShowRewardsView(expGained);
        }
        private void Update()
        {
            if (gameState == WoodGameState.Running)
            {
                PopulateGameTimer(Time.time);
            }
        }
        public void ControlGameState(WoodGameState woodGameState)
        {
            switch (woodGameState)
            {
                case WoodGameState.Running:
                    gameState = WoodGameState.Running;
                    woodAnimController.StartAnim();
                    break;
                case WoodGameState.NewStartOptions:
                    break;
                case WoodGameState.HitPaused:
                    gameState = WoodGameState.HitPaused;
                    break;
                case WoodGameState.LoadPaused:
                    gameState = WoodGameState.LoadPaused;                  
                    break;
                case WoodGameState.ExitGame:
                    gameState = woodGameState;
                    Back2Town();
                    break;
                default:
                    break;
            }
        }

    }
}


//void OnFleeBtnPressed()
//{
//    if (isFleeOpen)
//    {
//        continueBtn.transform.parent.GetComponent<RectTransform>().DOScaleY(0, 0.4f);
//        isFleeOpen = false;
//    }
//    else
//    {
//        continueBtn.transform.parent.GetComponent<RectTransform>().DOScaleY(1, 0.4f);
//        isFleeOpen = true;
//    }
//}

//void OnOptionsBtnPressed()
//{
//    if (isOptOpen)
//    {
//        fleeBtn.GetComponent<RectTransform>().DOScaleX(0, 0.4f);
//        isOptOpen = false;
//    }
//    else
//    {
//        fleeBtn.GetComponent<RectTransform>().DOScaleX(1, 0.4f);
//        isOptOpen = true;
//    }
//}
//public void TogglePanel(bool active, string str)
//{
//    startPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = " ";
//    startPanel.SetActive(active);

//}