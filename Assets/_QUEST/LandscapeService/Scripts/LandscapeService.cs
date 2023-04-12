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


        [Header("global variable")   ]
        public LandscapeNames currLandscape;

        //[Header("AllLandscapeSOs")]


        [Header("Models")]
        public List<LandscapeModel> allLandScapeModels = new List<LandscapeModel>();         
        
        public LandscapeController landscapeControllers; // single controller
        public LandScapeViewController LandScapeViewController; 




    }
}