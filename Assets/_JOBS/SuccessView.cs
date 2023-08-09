using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
namespace Town
{
    public class SuccessView : MonoBehaviour
    {
        WoodGameView1 woodGameView;
        [SerializeField] Button back2Town;
        [SerializeField] Button continueBtn;

        private void Awake()
        {
            back2Town.onClick.AddListener(OnBack2TownPressed);
            continueBtn.onClick.AddListener(OnContinuePressed);
        }
        public void InitSuccessView(WoodGameView1 woodGameView)
        {
            this.woodGameView = woodGameView;
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);
            woodGameView.LoadTheNextGameSeq();
        }
        void OnBack2TownPressed()
        {
            woodGameView.ExitGame();
            gameObject.SetActive(false);
        }
        void OnContinuePressed()
        {   
            woodGameView.OnContinuePressed();
            gameObject.SetActive(false);
        }
    }
}