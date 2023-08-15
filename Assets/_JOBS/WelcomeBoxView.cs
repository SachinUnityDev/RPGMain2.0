using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class WelcomeBoxView : MonoBehaviour
    {
        // get it from individual SO later
        [SerializeField] TextMeshProUGUI headingtxt; 
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Button continueBtn;

        [Header("global Var")]
        WoodGameView1 woodGameView;
        WoodGameData woodGameData; 

        private void Awake()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed); 
        }

        public void InitWelcomeGame(WoodGameData woodGameData, WoodGameView1 woodGameView)
        {
            this.woodGameView= woodGameView;
            this.woodGameData= woodGameData;    
            //gameObject.SetActive(true);
        }

        void OnContinueBtnPressed()
        {
            woodGameData.isPlayedOnce = true; // once welcome view is shown this can be true... 
            woodGameView.ShowStartPanel(); 
            gameObject.SetActive(false);
        }
    }
}