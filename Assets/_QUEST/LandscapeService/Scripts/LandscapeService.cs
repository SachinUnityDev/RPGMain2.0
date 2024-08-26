using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common;

namespace Quest
{

    public class LandscapeService : MonoSingletonGeneric<LandscapeService>  
    {   
        public event Action<LandscapeNames> OnLandscapeEnter;
        public event Action<LandscapeNames> OnLandscapeExit;

        [Header(" All Land SO")]
        public AllLandscapeSO allLandSO; 

        [Header("global variable")]
        public LandscapeNames currLandscape;


        public LandscapeController landscapeController; // single controller
        public LandscapeFactory landFactory;
        private void OnEnable()
        {
            currLandscape = LandscapeNames.Sewers;
            landscapeController = GetComponent<LandscapeController>();
            landFactory = GetComponent<LandscapeFactory>();
        }

        public void InitLandscape()
        {   
            landFactory.LandscapesInit();
            landscapeController.InitLandController(allLandSO); 
        }

        public void On_LandscapeEnter(LandscapeNames landName)
        {
            currLandscape= landName;
            OnLandscapeEnter?.Invoke(landName);
        }
        public void On_LandscapeExit(LandscapeNames landName)
        {
            currLandscape = landName;
            OnLandscapeExit?.Invoke(landName);
        }
        public LandscapeNames GetLandscapeNames()
        {
            return currLandscape;
        }   
    }
}