using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Town
{
    public class StartGameView : MonoBehaviour
    {
        // get it from individual SO later
        [SerializeField] TextMeshProUGUI headingtxt; 
        [SerializeField] TextMeshProUGUI descTxt;
        [SerializeField] Button continueBtn;

        [Header("global Var")]
        WoodGameView1 woodGameView;


        private void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed); 
        }

        public void InitStartGame(WoodGameView1 woodGameView)
        {
            this.woodGameView= woodGameView;
            gameObject.SetActive(true);
        }

        void OnContinueBtnPressed()
        {
            woodGameView.OnContinuePressed();
            gameObject.SetActive(false);
        }
    }
}