using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Town
{
    public class WelcomeService : MonoSingletonGeneric<WelcomeService>
    {
        
        public WelcomeController welcomeController;
        [Header("TBR")]
        public WelcomeView welcomeView;

        
        GameObject cornerBtns;

        void Start()
        {
            welcomeController= GetComponent<WelcomeController>();   
        }

        public void InitWelcome()
        {
            welcomeController = GetComponent<WelcomeController>();
            cornerBtns = GameObject.FindGameObjectWithTag("TownBtns");
            cornerBtns.SetActive(false);
            welcomeView.InitWelcomeView();
        }
    }
}