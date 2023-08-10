using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Town
{
    public class RewardsView : MonoBehaviour
    {
        [Header(" Exp : TBR")]
        [SerializeField] Transform expBar;
        [SerializeField] TextMeshProUGUI expDesc;

        [Header(" Rank : TBR")]
        [SerializeField] Transform rankIcon;
        [SerializeField] TextMeshProUGUI rankDesc;

        [Header(" Btns : TBR")]
        [SerializeField] ContinueBtnPtrEvents continueBtn;
        [SerializeField] FlawlessBtnPtrEvents flawBtn;
        [SerializeField] RewardsSlotView rewardsSlotView;
        [SerializeField] ExpConvertPtrEvents expConvert;
        [SerializeField] SellOutPtrEvents sellOut;


        [Header(" Global Var")]
        public WoodGameData woodGameData;
        public WoodGameController1 woodController;
        public WoodGameView1 woodGameView;
        public float currExp; 

        public void RewardsInit(WoodGameData woodGameData
                        , WoodGameController1 woodController, WoodGameView1 woodGameView)
        {
            this.woodController= woodController;
            this.woodGameData = woodGameData; 
            this.woodGameView= woodGameView;
            flawBtn.Init(woodGameData);
            rewardsSlotView.Init(woodGameData, woodController, woodGameView);
            expConvert.Init(woodGameData, woodController, woodGameView);
            sellOut.Init(woodGameData, woodController, woodGameView);
            FillRank((int)woodGameData.woodGameRank);
            continueBtn.Init(woodGameData, woodController, woodGameView);

        }
        public void ShowRewardsView(float expGained)
        {
            gameObject.SetActive(true);
            FillExpBar(expGained);
        }
        void FillRank(int rank)
        {
            int count = rankIcon.childCount;

            for (int i = 0; i < count; i++)
            {
                if (i <= rank)
                    rankIcon.GetChild(i).GetComponent<Image>().DOFade(1.0f, 0.1f);
                else
                    rankIcon.GetChild(i).GetComponent<Image>().DOFade(0.0f, 0.1f);
            }
            switch (rank)
            {
                case 0:
                    rankDesc.text = "Rank: Novice"; break;
                case 1:
                    rankDesc.text = "Rank: Expert"; break;
                case 2:
                    rankDesc.text = "Rank: Master"; break;
                default:
                    rankDesc.text = ""; break;                   
            }
        }
        public void FillExpBar(float expGained)
        {
            float ratio = (expGained - woodGameData.minJobExpR) / (woodGameData.maxJobExpR - woodGameData.minJobExpR);

            // based on min and max R for a given Rank 
            expBar.GetChild(0).GetComponent<Image>().fillAmount = ratio;
            expDesc.text = "+" + $"{expGained} Exp. gained"; 

        }
        
        


    }
}