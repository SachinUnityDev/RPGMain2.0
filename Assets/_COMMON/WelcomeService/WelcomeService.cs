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
        void Start()
        {
            welcomeController= GetComponent<WelcomeController>();   
        }

        public void InitWelcome()
        {
            //welcomeController = GetComponent<WelcomeController>();
            //welcomeView.gameObject.SetActive(true); 
           // welcomeView.InitWelcomeView();
        }
    }
}