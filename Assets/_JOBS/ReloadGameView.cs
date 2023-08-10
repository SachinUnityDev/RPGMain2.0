using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Town
{
    public class ReloadGameView : MonoBehaviour
    {

        WoodGameView1 woodGameView;
        [SerializeField] Button back2Town;
        [SerializeField] Button replay;

        private void Start()
        {
            back2Town.onClick.AddListener(OnBack2TownPressed);
            replay.onClick.AddListener(OnRePlayPressed);
        }
        public void InitReload(WoodGameView1 woodGameView)
        {
            this.woodGameView = woodGameView;
            gameObject.SetActive(false);
        }
        public void Show()
        {
            gameObject.SetActive(true);    
        }
        void OnBack2TownPressed()
        {
            woodGameView.Back2Town();
            gameObject.SetActive(false);    
        }
        void OnRePlayPressed()
        {
            woodGameView.OnContinuePressed();
            gameObject.SetActive(false);    
        }
    }
}