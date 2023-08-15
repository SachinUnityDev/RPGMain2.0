using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class WoodGameStartView : MonoBehaviour
    {

        WoodGameView1 woodGameView;
       
        [SerializeField] Button back2Town;
        [SerializeField] Button startGame;

        private void Start()
        {
            back2Town.onClick.AddListener(OnBack2TownPressed);
            startGame.onClick.AddListener(OnStartGamePressed);
        }
        public void InitStartView(WoodGameView1 woodGameView)
        {
            this.woodGameView = woodGameView;                
        }
       
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        void OnBack2TownPressed()
        {
            woodGameView.Back2Town();
            gameObject.SetActive(false);
        }
        void OnStartGamePressed()
        {
            woodGameView.OnContinuePressed();
            gameObject.SetActive(false);
        }


    }
}