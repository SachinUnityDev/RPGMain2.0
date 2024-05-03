using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Intro
{
    public class IntroViewController : MonoBehaviour
    {
        public SettingViewController settingViewController; 

        void Start()
        {
            settingViewController = transform.GetComponentInChildren<SettingViewController>(true); 
        }
    }
}