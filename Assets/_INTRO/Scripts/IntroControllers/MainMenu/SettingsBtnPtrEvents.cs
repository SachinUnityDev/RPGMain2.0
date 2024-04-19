using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Intro
{
    public class SettingsBtnPtrEvents : MonoBehaviour, IPointerClickHandler
    {
        MainMenuController mainMenuController; 


        public void OnPointerClick(PointerEventData eventData)
        {
            IntroServices.Instance.introViewController.settingViewController.GetComponent<IPanel>().Load();             
        }

        // Start is called before the first frame update
        public void Init(MainMenuController mainMenuController)
        {
            this.mainMenuController = mainMenuController; 
        }


    }
}