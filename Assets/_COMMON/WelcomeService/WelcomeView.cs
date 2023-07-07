using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Common
{
    public class WelcomeView : MonoBehaviour
    {
        [SerializeField] Button continueBtn;
        GameObject canvas;
        GameObject cornerBtns; 
        void Start()
        {
            continueBtn.onClick.AddListener(OnContinueBtnPressed);
            canvas = GameObject.FindGameObjectWithTag("TownCanvas"); 

        }

        public void InitWelcomeView()
        {
            cornerBtns = GameObject.FindGameObjectWithTag("TownBtns");
            cornerBtns.SetActive(false);
        }
        void OnContinueBtnPressed()
        {
            gameObject.SetActive(false);            
        }
 
    }
}